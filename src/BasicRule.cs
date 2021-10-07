using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRegex.src
{
    public class BasicRule : Rule, IRule
    {

        protected string[] _result;

        public bool Step()
        {

            return true;
        }

        public bool Complete()
        {
            return true;
        }

        public bool HasErrors()
        {
            return false;
        }

        public string[] Result()
        {
            return _result;
        }
    }
}
