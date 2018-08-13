namespace ChemFormatter
{
    public class Range
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public Range(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }

        public int End => Start + Length;
    }
}
