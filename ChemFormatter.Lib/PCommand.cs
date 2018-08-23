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
    public class ChangeSelectionCommand : RangeCommand
    {
        public ChangeSelectionCommand(int start, int length) : base(start, length) { }
    }

    public class FontResetCommand : PCommand
    {
    }

    public class MoveToCommand : PositionCommand
    {
        public MoveToCommand(int position) : base(position) { }
    }

    public class TypeParagraphCommand : PCommand
    {
    }

    public class TypeBackspaceCommand : PCommand
    {
    }

    public class TypeTextCommand : PCommand
    {
        public string Text { get; set; }

        public TypeTextCommand(string text)
        {
            this.Text = text;
        }
    }

    public class CopyAndPasteCommand : RangeCommand
    {
        public CopyAndPasteCommand(int start, int length) : base(start, length) { }
    }

    public class ReplaceStringCommand : RangeCommand
    {
        public string Replacement { get; set; }

        public ReplaceStringCommand(int start, int length, string replacement) : base(start, length)
        {
            this.Replacement = replacement;
        }
    }

    public class EnableItalicCommand : PCommand
    {
    }

    public class ItalicCommand : RangeCommand
    {
        public ItalicCommand(int start, int length) : base(start, length) { }
    }

    public class BoldCommand : RangeCommand
    {
        public BoldCommand(int start, int length) : base(start, length) { }
    }

    public class SmallCapitalCommand : RangeCommand
    {
        public SmallCapitalCommand(int start, int length) : base(start, length) { }
    }    

    public class ChangeScriptCommand : RangeCommand
    {
        public ChangeScriptCommand(int start, int length) : base(start, length) { }
    }

    public class SubscriptCommand : RangeCommand
    {
        public SubscriptCommand(int start, int length) : base(start, length) { }
    }

    public class SuperscriptCommand : RangeCommand
    {
        public SuperscriptCommand(int start, int length) : base(start, length) { }
    }

    public abstract class RangeCommand : PCommand
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public RangeCommand(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }
    }

    public abstract class PositionCommand : PCommand
    {
        public int Position { get; set; }

        public PositionCommand(int position)
        {
            this.Position = position;
        }
    }

    public abstract class PCommand
    {
    }
}
