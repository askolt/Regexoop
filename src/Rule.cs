using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRegex.src;

namespace EasyRegex.src
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

        public virtual bool CheckRequires()
        {
            if (Name.Length == 0)
            {
                return false;
            }

            return true;
        }

        public Status ParseSymbol(char c)
        {
            if (_status != Status.Step)
            {
                _status = Status.Skip;
                _cursor = 0;
                _result = "";
            }
            List<char> FoundChars = ParsePattern();
            //Console.WriteLine("Cursor: {0}", _cursor);
            foreach (char TryFindChar in FoundChars)
            {
                Console.WriteLine("Try find :{0}: in :{1}:", TryFindChar, c);
                if (c == TryFindChar)
                {
                    _result += TryFindChar;
                    if (_result.Length == Pattern.Length)
                    {
                        _status = Status.Complete;
                        return Status.Complete;
                    } 
                    else
                    {
                        _status = Status.Step;
                        return Status.Step;
                    }

                    /*if (_status == Status.Skip)
                    {
                        _status = Status.Step;
                        return Status.Step; //????
                    }
                    if (_status == Status.Step && IsCompletePattern())
                    {
                        return Status.Complete;
                    }
                    return Status.Step;*/
                }
            }
            if (_status == Status.Step)
            {
                _status = Status.Wrong;
                return Status.Wrong;
            }
            return Status.Skip;
        }

        protected List<char> ParsePattern()
        {
            List<char> FoundChars = new List<char>();
            if (IsCompletePattern())
            {
                _cursor = 0;
                _status = Status.Skip;
            }
            if (IsCommandSymbol(Pattern[_cursor]) == false)
            {
                FoundChars.Add(Pattern[_cursor]);
            }
            _cursor++;
            return FoundChars;
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


    }
}
