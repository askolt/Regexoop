using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class RedirectCommand : ICommand
    {

        private string _startCommand = "{";

        private string _middle;

        private string _endCommand = "}";

        public override string StartCommand { get => _startCommand; }
        public override string Middle { get => _middle; set => _middle = value; }
        public override string EndCommand { get => _endCommand; }

        public override Rule.Status Parse(ref InputText inputText, Rule rule)
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
