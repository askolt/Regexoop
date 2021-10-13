using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    class RangeCommand : ICommand
    {

        private string _startCommand = "[";

        private string _middle;

        private string _endCommand = "]";

        private List<string> _letters = new List<string>();

        public override string StartCommand { get => _startCommand; }
        public override string Middle { get => _middle; set => _middle = value; }
        public override string EndCommand { get => _endCommand; }

        public override Rule.Status Parse(ref InputText inputText, in Rule rule)
        {
            /*
             * 3 ways:
             * 1 - regex;
             * 2 - prepare letters before
             * 3 - prepare letters in process
             * First try the second way
             * */
            for (int x = 0; x < Middle.Length; x++)
            {
                int y = x + 1;
                int z = y + 1;
                if (z <= Middle.Length)
                {
                    int start;
                    int end;
                    if (int.TryParse(Middle[x].ToString(), out start) == true && Middle[y] == '-' && int.TryParse(Middle[z].ToString(), out end))
                    {
                        for(int index = start; index <= end; index++) //todo
                        {
                            if (_letters.Contains(index.ToString()) == false)
                            {
                                _letters.Add(index.ToString());
                            }
                        }
                        x = x + 2;
                    }
                    else
                    {
                        if (_letters.Contains(Middle[x].ToString()) == false)
                        {
                            _letters.Add(Middle[x].ToString());
                        }
                    }
                }
                else
                {
                    if (_letters.Contains(Middle[x].ToString()) == false)
                    {
                        _letters.Add(Middle[x].ToString());
                    }
                }
            }
            foreach (string symbol in _letters)
            {
                if (inputText.GetSymbols(1) == symbol)
                {
                    rule.SetResult(symbol);
                    return Rule.Status.Step;
                }
            }
            return Rule.Status.Wrong;
        }
    }
}
