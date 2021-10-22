using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    public class InputText
    {
        protected int _cursor = 0;

        protected string _inputText;

        protected bool _useInputText;

        protected Rule.Direction _direction;

        public InputText(Rule rule)
        {
            _direction = rule.Start;
        }

        //Warning. Don't move cursor here. 
        public string GetSymbols(int range)
        {
            _useInputText = true;
            List<char> chars = new List<char>();
            if (_direction == Rule.Direction.start)
            {
                int EndRange = _cursor + range;
                if (EndRange > _inputText.Length)
                {
                    EndRange = _inputText.Length;
                }
                for (int x = _cursor; x < EndRange; x++)
                {
                    chars.Add(_inputText[x]);
                }
            }
            if (_direction == Rule.Direction.end)
            {
                int EndRange = _cursor - range;
                if (EndRange < 0)
                {
                    EndRange = -1;
                }
                for (int x = _cursor; x > EndRange; x--)
                {
                    chars.Add(_inputText[x]);
                }
            }
            return new string(chars.ToArray());
        }

        public void MoveCursor(int count)
        {
            if (_useInputText == true)
            {
                _useInputText = false;
                if (_direction == Rule.Direction.end)
                {
                    if (count <= 0)
                    {
                        count = 1;
                    }
                    _cursor -= count;
                }
                else
                {
                    if (count <= 0)
                    {
                        count = 1;
                    }
                    _cursor += count;
                }
                //todo clear readed text
            }
        }

        public bool IsComplete()
        {
            return _direction == Rule.Direction.end ? _cursor < 0 : _cursor > _inputText.Length;
        }

        public void Input(string input)
        {
            if (_direction == Rule.Direction.end)
            {
                _inputText = input + _inputText;
                _cursor = _inputText.Length - 1;
            }
            else
            {
                _inputText += input;
            }
        }

        public void Clear()
        {
            _inputText = "";
        }
    }
}
