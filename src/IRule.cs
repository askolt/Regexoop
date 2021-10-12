using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    interface IRule
    {

        bool Step();

        bool Complete();

        bool HasErrors();

        List<string> GetResult();

    }
}
