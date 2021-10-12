using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class RedirectCommand : ICommand
    {

        public string StartCommand = "{";

        public string Middle;

        public string EndCommand = "}";

        public Rule.Status Parse(ref InputText inputText, ref Rule rule)
        {
            bool res = rule.SetRedirectRule(Middle);
            if (res == false)
            {
                throw new Exception($"Variable {Middle} not found.");
            }
            return Rule.Status.Step;
        }
    }
}
