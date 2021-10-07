using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRegex.src
{
    public abstract class Rule
    {
        public string Name;

        public string Pattern;

        public int Length;

        public int MinLength;

        public int MaxLength;

        public int Repeat;

        public int MinRepeat;

        public int MaxRepeat;

        public bool Trim;

        public string Start;

        public bool Result;

        public bool CaseInsensitive;

        public bool Loop;

        public int LoopVariable;

        public string Lazy;

        public bool Chunk;

        public bool AsArray;

        private int _cursor;

        public delegate void IfBefore();

        public delegate void IfAfter();

        public List<Rule> Variables;

    }
}
