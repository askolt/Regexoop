using System;
using System.Collections.Generic;
using Regexoop.src;



namespace Regexoop
{
    public class Regexoop
    {

        protected string _input;

        protected List<Rule> Rules = new List<Rule>();

        public Regexoop(Rule rule)
        {
            AddRules(rule);
        }

        public Regexoop Input(string input)
        {
            _input += input;
            return this;
        }

        public Regexoop AddRules(Rule rule)
        {
            Rules.Add(rule);
            return this;
        }

        public List<string> Find()
        {

            List<string> result = new List<string>();
            foreach (Rule rootRule in Rules)
            {
                // each root rules have own input text;
                InputText copyInput = new InputText(rootRule);
                copyInput.Input(_input);
                Router charge = new Router(rootRule, copyInput);
                charge.Step();
                charge.Complete();
                result.AddRange(charge.GetResult());
                copyInput.Clear();
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
