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

        private int _cursor = 0;

        protected Status _status = Status.Skip;

        protected string _inputText;

        public delegate void IfBefore();

        public delegate void IfAfter();

        public List<Rule> Variables;

        protected int _redirectRule;

        List<ICommand> _commands = new List<ICommand>();

        public Rule()
        {
            _commands.Add(new RedirectCommand());
            _commands.Add(new TextCommand());
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
                _cursor = 0;
                _result = "";
            }
            //List<char> FoundChars = ParsePattern();
            ICommand command = ParsePattern();
            Status resStatus = command.Parse(ref inputChars, this);
            //inputChars.MoveCursor(1);
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
                _cursor = 0;
                _status = Status.Skip; //todo check it
            }
            ICommand text = new TextCommand();
            text.Middle = Pattern[_cursor].ToString();
            _cursor++;
            return text;
            
            List<char> FoundChars = new List<char>();
            
            if (IsCommandSymbol(Pattern[_cursor]) == false)
            {
                FoundChars.Add(Pattern[_cursor]);
            }
            
            //return FoundChars;
        }

        protected bool IsCommandSymbol(char c)
        {

            return false;
        }

        protected bool IsCompletePattern()
        {
            return _cursor >= Pattern.Length;
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
            return _redirectRule;
        }

        public bool SetRedirectRule(string name)
        {
            int x = 0; //todo has list the index?
            foreach (Rule variable in Variables)
            {
                if (variable.Name == name)
                {
                    _redirectRule = x;
                    return true;
                }
                x++;
            }
            return false;
        }


    }
}
