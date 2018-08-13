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

using System;
using System.Collections;
using System.Collections.Generic;

namespace ChemFormatter
{
    public class IndexAndString
    {
        public int Index { get; set; }
        public string Text { get; set; }

        public IndexAndString(int index, string text)
        {
            this.Index = index;
            this.Text = text;
        }

        public override string ToString()
        {
            return $"{Index}: {Text}";
        }
    }

    public class LineReader : IEnumerable<IndexAndString>
    {
        string text;
        int currPosition;
        public bool ThrowExceptionOnNoEol { get; set; } = false;

        public LineReader(string text)
        {
            // add sentinel
            this.text = text + '\uffff';
            currPosition = 0;
        }

        public IEnumerator<IndexAndString> GetEnumerator()
        {
            while (true)
            {
                if (text[currPosition] == '\uFFFF')
                    yield break;

                int i = currPosition;
                while (true)
                {
                    var c = text[i++];
                    switch (c)
                    {
                        case '\r':
                        case '\n':
                        case '\uFFFF':
                            var endPosition = i - 1;
                            var newPosition = i;
                            if (c == '\uFFFF')
                            {
                                if (ThrowExceptionOnNoEol)
                                    throw new Exception("No end of line.");
                                newPosition -= 1;
                            }
                            else if (c == '\r' && text[i] == '\n')
                            {
                                newPosition += 1;
                            }
                            var line = text.Substring(currPosition, endPosition - currPosition);
                            var ret = new IndexAndString(currPosition, line);
                            currPosition = newPosition;
                            yield return ret;
                            goto L_Next;
                        default:
                            break;
                    }
                }
            L_Next:
                ;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
