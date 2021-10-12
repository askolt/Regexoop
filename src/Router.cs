using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class Router : IRule
    {

        protected List<string> _result = new List<string>();

        protected InputText _input;

        protected Rule _rule;

        protected int _cursor = 0;

        public Router(Rule rule, InputText input)
        {
            _rule = rule;
            _input = input;
        }

        public bool Step()
        {
            if (_input.IsComplete())
            {
                return true;
            }

            if (!_rule.CheckRequires())
            {
                return false;
            }

            while(_input.IsComplete() == false)
            {
                Rule.Status stepResult = _rule.ParseSymbol(_input);
                _input.MoveCursor(1); //todo 
                if (stepResult == Rule.Status.Complete)
                {
                    _result.Add(_rule.GetResult());
                }
                //Console.WriteLine("Char: {0}  Step: {1}", _input.GetSymbols(1), stepResult);
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
