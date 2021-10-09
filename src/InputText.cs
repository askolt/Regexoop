﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRegex.src
{
    public class InputText
    {
        protected int _cursor = 0;

        protected string _inputText;

        //Warning. Don't move cursor here. 
        public string GetSymbols(int range)
        {
//            Console.WriteLine("Cursor inputText: {0}", _cursor);
            List<char> chars = new List<char>();
            int EndRange = _cursor + range;
            if (EndRange > _inputText.Length)
            {
                EndRange = _inputText.Length;
            }
            for (int x = _cursor; x < EndRange; x++)
            {
                chars.Add(_inputText[x]);
            }
            return new string(chars.ToArray());
        }

        public void MoveCursor(int count)
        {
            //todo clear readed text
            if (count <= 0)
            {
                count = 1;
            }
            _cursor += count;
        }

        public bool IsComplete()
        {
            return _cursor > _inputText.Length;
        }

        public void Input(string input)
        {
            _inputText += input;
        }

        public void Clear()
        {
            _inputText = "";
        }
    }
}