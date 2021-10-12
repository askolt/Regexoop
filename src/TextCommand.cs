using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class TextCommand : ICommand
    {

        public string StartCommand = "";

        public string Middle;

        public string EndCommand = "";

        public Rule.Status Parse(ref InputText inputText, ref Rule rule)
        {
            if (inputText.GetSymbols(1) == Middle)
            {
                rule.SetResult(Middle);
                return Rule.Status.Step;
            /*    if (_result.Length == Pattern.Length)
                {
                    _status = Status.Complete;
                    return Status.Complete;
                }
                else
                {
                    _status = Status.Step;
                    return Status.Step;
                }*/

            }
            return Rule.Status.Wrong;
        }
    }
}
