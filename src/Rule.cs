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

        protected int _redirectRule;

        protected bool _needRedirect;

        List<ICommand> _commands = new List<ICommand>();

        public Rule()
        {
            _commands.Add(new RedirectCommand());
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
                _cursorPattern = 0;
                _result = "";
            }
            ICommand command = ParsePattern();
            if (command == null)
            {
                return _status;
            }
            Status resStatus = command.Parse(ref inputChars, this);
            if (resStatus == Status.Step && _result.Length == Pattern.Length)
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
                _cursorPattern = 0;
                _status = Status.Complete; //todo check it
                return null;
            }
            foreach (ICommand command in _commands)
            {
                if (command.StartCommand == Pattern[_cursorPattern].ToString())
                {
                    int tempCursor = _cursorPattern + 1;
                    string body = "";
                    while (tempCursor <= Pattern.Length)
                    {
                        if (command.EndCommand == Pattern[tempCursor].ToString())
                        {
                            command.Middle = body;
                            _cursorPattern = tempCursor + 1;
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
            _cursorPattern++;
            return text;
        }

        protected bool IsCommandSymbol(char c)
        {
            return false;
        }

        protected bool IsCompletePattern()
        {
            return _cursorPattern >= Pattern.Length;
        }
        
        public string GetResult()
        {
            return _result;
        }

        public void SetResult(string text)
        {
            _result += text;
        }

        public int GetRedirectRule()
        {
            _needRedirect = false;
            return _redirectRule;
        }

        public bool NeedRedirect()
        {
            return _needRedirect;
        }

        public bool SetRedirectRule(string name)
        {
            int x = 0;
            foreach (Rule variable in Variables)
            {
                if (variable.Name == name)
                {
                    _needRedirect = true;
                    _redirectRule = x;
                    return true;
                }
                x++;
            }
            return false;
        }
    }
}
