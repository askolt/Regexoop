using System;
using System.Collections.Generic;
using EasyRegex.src;



namespace EasyRegex
{
    public class EasyRegex
    {

        protected string _input;

        protected List<Rule> Rules = new List<Rule>();

        public EasyRegex(Rule rule)
        {
            AddRules(rule);
        }

        public EasyRegex Input(string input)
        {
            _input += input;
            return this;
        }

        public EasyRegex AddRules(Rule rule)
        {
            Rules.Add(rule);
            return this;
        }

        public List<string> Find()
        {

            List<string> result = new List<string>();
            foreach (Rule rule in Rules)
            {
                Router charge = new Router(rule);
                charge.Input(_input);
                charge.Step();
                charge.Complete();
                result.AddRange(charge.GetResult());
            }
            return result;
        }

        public bool Check()
        {
            Console.WriteLine("Check");
            return true;
        }

        public object Each()
        {

            return "fdsf";
        }
    }
}
