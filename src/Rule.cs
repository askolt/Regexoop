using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regexoop.src;

namespace Regexoop.src
{

    public abstract class Rule
    {
        public enum Direction { start = 1, stop = 2 }

        public enum Status { Skip = 0, Wrong, Step, Complete }

        public string Name;

        public string Pattern;

        public int Length;

        public int MinLength;

        public int MaxLength;

        public int Repeat;

        public int MinRepeat;

        public int MaxRepeat;

        public bool Trim;

        public Direction Start;

        public bool Result;

        protected string _result;

        public bool CaseInsensitive;

        public bool Loop;

        public int LoopVariable;

        public string Lazy;

        public bool Chunk;

        public bool AsArray;

        private int _cursorPattern = 0;

        protected Status _status = Status.Skip;

        public delegate void IfBefore();

        public delegate void IfAfter();

        public List<Rule> Variables;

        protected Rule _redirectRule;

        protected bool _needRedirect;

        List<ICommand> _commands = new List<ICommand>();

        public Rule()
        {
            _commands.Add(new RedirectCommand());
            _commands.Add(new RangeCommand());
        }

        public virtual bool CheckRequires()
        {
            if (Name.Length == 0)
            {
                return false;
            }

            return true;
        }

        public Status ParseSymbol(InputText inputChars)
        {
            if (_status != Status.Step)
            {
                _status = Status.Skip;
                ResetCursorPattern();
                _result = "";
            }
            ICommand command = ParsePattern();
            if (command == null)
            {
                return _status;
            }
            Status resStatus = command.Parse(ref inputChars, this);
            /*        if (resStatus == Status.Complete) //stop recursion 
                    {
                        _status = Status.Complete;
                        return Status.Complete;
                    }*/
            if (resStatus == Status.Step && _result.Length == Pattern.Length) //normal way
            {
                _status = Status.Complete;
                return Status.Complete;
            }
            if (resStatus == Status.Wrong && _status == Status.Step)
            {
                _status = Status.Wrong;
                return Status.Wrong;
            }
            if (resStatus == Status.Step)
            {
                _status = Status.Step;
            }
            return _status;
        }

        protected ICommand ParsePattern()
        {
            if (IsCompletePattern())
            {
                ResetCursorPattern();
                _status = Status.Complete; //todo check it
                return null;
            }
            foreach (ICommand command in _commands)
            {
                if (command.StartCommand == Pattern[_cursorPattern].ToString())
                {
                    int tempCursor = GetCursorPattern() + 1;
                    string body = "";
                    while (tempCursor <= Pattern.Length)
                    {
                        if (command.EndCommand == Pattern[tempCursor].ToString())
                        {
                            command.Middle = body;
                            _cursorPattern = tempCursor + 1;
                            //tempCursor = tempCursor + 1;
                            //MoveCursorPattern(tempCursor+2);
                            return command;
                        }
                        else
                        {
                            body += Pattern[tempCursor];
                        }
                        tempCursor++;
                    }
                }
            }
            ICommand text = new TextCommand();
            text.Middle = Pattern[_cursorPattern].ToString();
            MoveCursorPattern(1);
            return text;
        }

        protected bool IsCommandSymbol(char c)
        {
            return false;
        }

        protected bool IsCompletePattern()
        {
            if (Start == Direction.start)
            {
                return _cursorPattern >= Pattern.Length;
            }
            return _cursorPattern <= 0;
        }

        protected void ResetCursorPattern()
        {
            _cursorPattern = 0;
        }

        protected void MoveCursorPattern(int value)
        {
            if (Start == Direction.start)
            {
                _cursorPattern += value;
            } 
            else
            {
                _cursorPattern -= value;
            }
        }

        protected int GetCursorPattern(int value = 0)
        {
            if (value > 0)
            {
                if (Start == Direction.start)
                {
                    return _cursorPattern + value;
                }
                return _cursorPattern - value;
            }
            return _cursorPattern;
        }

        public string GetResult()
        {
            return _result;
        }

        public void SetResult(string text)
        {
            _result += text;
        }

        public Rule GetRedirectRule()
        {
            _needRedirect = false;
            return _redirectRule;
        }

        public bool NeedRedirect()
        {
            return _needRedirect;
        }

        protected void SetNeedRedirect()
        {
            _needRedirect = true;
        }

        public bool SetRedirectRule(string name, in Rule rule)
        {
            //int x = 0;
            if (name == Name)
            {
                _needRedirect = true;
                Rule redirectRule = new BasicRule().CopyRule(rule);
                redirectRule.LoopVariable -= 1;
                _redirectRule = redirectRule;
                return true;
            }
            if (rule.Variables == null)
            {
                return false;
            }
            foreach (Rule variable in rule.Variables)
            {
                if (variable.Name == name)
                {
                    _needRedirect = true;
                    Rule redirectRule = new BasicRule().CopyRule(variable);
                    redirectRule.Start = Start;
                    _redirectRule = redirectRule;
                    return true;
                }
                //x++;
            }
            return false;
        }

        abstract protected Rule CopyRule(Rule rule);
    }
}
