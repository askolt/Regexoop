using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    interface ICommand
    {
        Rule.Status Parse(ref InputText inputText, ref Rule rule)
        {
            return Rule.Status.Complete;
        }
    }
}
