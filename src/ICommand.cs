using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    public abstract class ICommand
    {
        abstract public string StartCommand { get; }

        abstract public string Middle { get; set; }

        abstract public string EndCommand { get; } 

        public abstract Rule.Status Parse(ref InputText inputText, Rule rule);
    }
}
