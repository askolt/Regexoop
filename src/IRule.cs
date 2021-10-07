using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRegex.src
{
    interface IRule
    {

        bool Step();

        bool Complete();

        bool HasErrors();

        string[] Result();

    }
}
