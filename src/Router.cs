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

        protected Stack<Rule> _rule = new Stack<Rule>();

        protected Rule _rootRule;

        protected int _cursor = 0;

        public Router(Rule rule, InputText input)
        {
            _rule.Push(rule);
            _rootRule = rule;
            _input = input;
        }

        public bool Step()
        {
            if (_input.IsComplete())
            {
                return true;
            }

            if (!_rule.Peek().CheckRequires())
            {
                return false;
            }

            while(_input.IsComplete() == false)
            {
                if (_rule.Count == 0)
                {
                    _rule.Push(_rootRule);
                }

                Rule.Status stepResult = _rule.Peek().ParseSymbol(_input);
                _input.MoveCursor(1); //todo 
                if (stepResult == Rule.Status.Complete)
                {
                    _result.Add(_rule.Peek().GetResult());
                    _rule.Pop();
                }
                //Console.WriteLine("Char: {0}  Step: {1}", _input.GetSymbols(1), stepResult);
                _cursor += 1;

                if (_rule.Count == 0) //todo
                {
                    _rule.Push(_rootRule);
                }
                // todo maybe let call above yourself??
                if (_rule.Peek().GetRedirectRule() != 0)
                {
                    _rule.Push(_rule.Peek().Variables[_rule.Peek().GetRedirectRule()]);
                }
            }
            //Console.WriteLine(_rule.Pattern);

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
