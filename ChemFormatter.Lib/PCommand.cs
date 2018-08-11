namespace ChemFormatter
{
    public class ReplaceStringCommand : ApplyFormatCommand
    {
        public string Replacement { get; set; }

        public ReplaceStringCommand(int start, int length, string replacement) : base(start, length)
        {
            this.Replacement = replacement;
        }
    }

    public class ItalicCommand : ApplyFormatCommand
    {
        public ItalicCommand(int start, int length) : base(start, length) { }
    }

    public class BoldCommand : ApplyFormatCommand
    {
        public BoldCommand(int start, int length) : base(start, length) { }
    }

    public class SmallCapitalCommand : ApplyFormatCommand
    {
        public SmallCapitalCommand(int start, int length) : base(start, length) { }
    }    

    public class ChangeScriptCommand : ApplyFormatCommand
    {
        public ChangeScriptCommand(int start, int length) : base(start, length) { }
    }

    public class SubscriptCommand : ApplyFormatCommand
    {
        public SubscriptCommand(int start, int length) : base(start, length) { }
    }

    public class SuperscriptCommand : ApplyFormatCommand
    {
        public SuperscriptCommand(int start, int length) : base(start, length) { }
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
