using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regexoop.src
{
    public class BasicRule : Rule
    {
        public override Rule CopyRule(Rule rule)
        {
            Name = rule.Name;
            Pattern = rule.Pattern;
            /*Length = rule.Length;
            MinLength = rule.MinLength;
            MaxLength = rule.MaxLength;*/
            Repeat = rule.Repeat;
            MinRepeat = rule.MinRepeat;
            MaxRepeat = rule.MaxRepeat;
            Trim = rule.Trim;
            Start = rule.Start;
            Result = rule.Result;
            CaseInsensitive = rule.CaseInsensitive;
            Loop = rule.Loop;
            LoopVariable = rule.LoopVariable;
            Lazy = rule.Lazy;
            Chunk = rule.Chunk;
            AsArray = rule.AsArray;
            Variables = rule.Variables;
            if (rule.IsNeedRedirect() && rule.Name == Name)
            {
                _preparedRule = true;
            }
            return this;
        }
    }
}
