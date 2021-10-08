using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRegex.src
{
    class Router : IRule
    {

        protected List<string> _result = new List<string>();

        protected string _input;

        protected Rule _rule;

        protected int _cursor = 0;

        public Router(Rule rule)
        {
            _rule = rule;
        }

        public void Input(string input)
        {
            _input += input;
        }

        public bool Step()
        {
            if (_input.Length == 0)
            {
                return true;
            }

            if (!_rule.CheckRequires())
            {
                return false;
            }

            foreach (char c in _input)
            {
                Rule.Status stepResult = _rule.ParseSymbol(c);
                if (stepResult == Rule.Status.Complete)
                {
                    _result.Add(_rule.GetResult());
                }
                Console.WriteLine("Char: {0}  Step: {1}", c, stepResult);

                _cursor += 1;
            }


            Console.WriteLine(_rule.Pattern);

            return true;
        }

        public bool Complete()
        {
            return true;
        }

        public bool HasErrors()
        {
            return false;
        }

        public List<string> GetResult()
        {
            return _result;
        }
    }
}
