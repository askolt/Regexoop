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
        public enum Direction { start = 1, end = 2 }

        public enum Status { Skip = 0, Wrong, Step, Complete }

        public string Name;

        public string Pattern;

        /*public int Length;

        public int MinLength;

        public int MaxLength;*/

        public int Repeat;

        protected int _repeat;

        public int MinRepeat;

        public int MaxRepeat;

        public bool Trim;

        public Direction Start = Direction.start;

        public bool Result = true;

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

        public List<Rule> Variables = new List<Rule>();

        protected Rule _redirectRule;

        protected bool _needRedirect;

        //protected bool _moveInputCursor;

        List<ICommand> _commands = new List<ICommand>();

        protected bool _preparedRule;

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

        public virtual bool PrepareRule()
        {
            if (_preparedRule)
            {
                return true;
            }
            if (Start == Direction.end)
            {
                List<string> patternRevers = new List<string>();
                while(IsCompletePattern() == false)
                {
                    ICommand command = ParsePattern();
                    if (command == null)
                    {
                        break;
                    }
                    patternRevers.Add(command.StartCommand + command.Middle + command.EndCommand);
                }
                Pattern = "";
                for (int i = patternRevers.Count - 1; i >= 0; i--)
                {
                    Pattern += patternRevers[i];
                }
                _preparedRule = true;
                _status = Status.Skip;
                ResetCursorPattern();
            }
            if (MaxRepeat > 0 || MinRepeat > 0)
            {
                Repeat = 0;
            }
            if (Repeat < 0 || MaxRepeat < 0 || MinRepeat < 0)
            {
                throw new Exception("Parameter Repeat is wrong!");
            }
            foreach (Rule variable in Variables)
            {
                variable.Start = Start;
            }
            return true;
        }

        public Status ParseSymbol(InputText inputChars)
        {
            PrepareRule();
            //_moveInputCursor = false;
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
            //_moveInputCursor = true;
            Status resStatus = command.Parse(ref inputChars, this);
            /*if (resStatus == Status.Complete) //stop recursion 
            {
                _status = Status.Complete;
            }*/
            if (IsCompletePattern() && (Repeat > 0 || MinRepeat > 0 || MaxRepeat > 0))
            {
                _repeat += 1;
                if (Repeat > 0 && _repeat <= Repeat)
                {
                    ResetCursorPattern();
                }
                if (MinRepeat > 0)
                {
                    ResetCursorPattern();
                }
                if (MaxRepeat > 0 && _repeat <= MaxRepeat)
                {
                    ResetCursorPattern();
                }
            }

            //if (resStatus == Status.Step && _result.Length == Pattern.Length) //normal way
            if (resStatus == Status.Step && IsCompletePattern() && IsNeedRedirect() == false) //normal way
            {
                //_moveInputCursor = false;
                _status = Status.Complete;
                return Status.Complete;
            }
            if (resStatus == Status.Wrong && _status == Status.Step)
            {
                if (MinRepeat > 0 && _repeat >= MinRepeat)
                {
                    _status = Status.Complete;
                    return Status.Complete;
                }
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
            //_moveInputCursor = true;
            foreach (ICommand command in _commands)
            {
                if (command.StartCommand == Pattern[_cursorPattern].ToString())
                {
                    int tempCursor = 1;
                    string body = "";
                    while (GetCursorPattern(tempCursor) <= Pattern.Length)
                    {
                        if (command.EndCommand == Pattern[GetCursorPattern(tempCursor)].ToString())
                        {
                            command.Middle = body;
                            //_cursorPattern = tempCursor + 1;
                            //tempCursor = tempCursor + 1;
                            MoveCursorPattern(tempCursor + 1);
                            return command;
                        }
                        else
                        {
                            body += Pattern[GetCursorPattern(tempCursor)];
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
            //if (Start == Direction.start)
            //{
                return _cursorPattern >= Pattern.Length;
            //}
            //return _cursorPattern <= 0;
        }

        protected void ResetCursorPattern()
        {
            _cursorPattern = 0;
        }

        protected void MoveCursorPattern(int value)
        {
            //if (Start == Direction.start)
            //{
                _cursorPattern += value;
            //} 
            //else
            //{
            //    _cursorPattern -= value;
            //}
        }

        protected int GetCursorPattern(int value = 0)
        {
            if (value == 0)
            {
                return _cursorPattern;
            }
            return value > 0 ? _cursorPattern + value : _cursorPattern - value;
        }

        //public bool IsMoveInputCursor()
        //{
        //    return _moveInputCursor;
        //}

        public string GetResult()
        {
            return Result ? _result : "";
        }

        public void SetResult(string text)
        {
            if (Start == Direction.end)
            {
                _result = text + _result;
            }
            else
            {
                _result += text;
            }
        }

        public Rule GetRedirectRule()
        {
            return _redirectRule;
        }

        public bool IsNeedRedirect()
        {
            return _needRedirect;
        }

        public void ResetRedirect()
        {
            _needRedirect = false;
            _redirectRule = null;
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
                if (redirectRule.LoopVariable < 0)
                {
                    //_redirectRule = null;
                    _needRedirect = false;
                }
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

        abstract public Rule CopyRule(Rule rule);
    }
}
