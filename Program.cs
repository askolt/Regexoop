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

            foreach(Rule obj in Rules) {
                foreach (Rule rul in obj.Variables)
                {
                    Console.WriteLine(rul.Name);
                }
            }
            return this;
        }

        public object Find()
        {

            Console.WriteLine("Find");
            return "fdsfds";
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
