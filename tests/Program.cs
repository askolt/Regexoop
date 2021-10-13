using System;
using Regexoop;
using Regexoop.src;
using System.Collections.Generic;



namespace ConsoleRegexoop
{
    public class Program
    {
        public void Main(string[] args)
        {
            
            Rule test = new BasicRule() { Name="root", 
                                          Pattern = "Hello {world}", 
                                          Start = Rule.Direction.start, 
                                          Variables = new List<Rule> { 
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              } 
                                          } };

            List<string> p = new Regexoop.Regexoop(test).Input("Hello World").Find();
            Console.WriteLine("Input: Hello World");
            Console.WriteLine("Found:");
            foreach (string item in p)
            {
                Console.WriteLine(item);
            }
        }
    }
}
