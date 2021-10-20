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

        protected List<string> _rawResult = new List<string>();

        protected InputText _input;

        protected Stack<Rule> _rule = new Stack<Rule>();

        protected Rule _rootRule;

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
            // todo check requires for all of rules
            if (!_rule.Peek().CheckRequires())
            {
                return false;
            }

            while(_input.IsComplete() == false)
            {
                _input.MoveCursor(1);
                Rule.Status stepResult = _rule.Peek().ParseSymbol(_input);
                if (stepResult == Rule.Status.Complete)
                {
                    _rawResult.Add(_rule.Peek().GetResult());
                    _rule.Pop();
                    
                    if (_rule.Count == 0) //root rule has been complete
                    {
                        _result.Add(_rawResult[0]);
                        _rawResult.Clear();
                        _rule.Push(_rootRule);
                    }
                    else
                    {
                        _rule.Peek().SetResult(_rawResult[0]);
                        _rawResult.Clear();
                    }
                }
                if (stepResult == Rule.Status.Wrong)
                {
                    _rule.Pop();
                }
                if (_rule.Count == 0) //todo
                {
                    _rule.Push(_rootRule);
                }
                // todo recursive call
                if (_rule.Peek().IsNeedRedirect())
                {
                    Rule redirectRule = _rule.Peek().GetRedirectRule();
                    _rule.Peek().ResetRedirect();
                    _rule.Push(redirectRule);
                }
               /* else
                {
                    if (_rule.Peek().IsMoveInputCursor())
                    {
                        
                    }
                }*/
            }
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
