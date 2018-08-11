// MIT License
// 
// Copyright (c) 2018 Kazuya Ujihara
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
