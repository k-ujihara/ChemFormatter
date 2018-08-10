using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public enum POperator
    {
        None = 0,
        Subscript,
        Superscript,
    }

    public class ChangeScriptCommand : ApplyFormatCommand
    {
        public ChangeScriptCommand(int start, int length) : base(start, length) { }
    }

    public class SubscriptCommand : ApplyFormatCommand
    {
        public SubscriptCommand(int start, int length) : base(start, length) { }
    }

    public class ApplyFormatCommand : PCommand
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public ApplyFormatCommand(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }
    }

    public abstract class PCommand
    {
    }
}
