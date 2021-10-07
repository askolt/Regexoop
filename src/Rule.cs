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

        public bool CaseInsensitive;

        public bool Loop;

        public int LoopVariable;

        public string Lazy;

        public bool Chunk;

        public bool AsArray;

        private int _cursor;

        protected string _inputText;

        public delegate void IfBefore();

        public delegate void IfAfter();

        public List<Rule> Variables;

        protected bool CheckRequires()
        {
            if (Name.Length == 0)
            {
                return false;
            }

            return true;
        }

    }
}
