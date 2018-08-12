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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    public static class NMRSpectrumQuery
    {
        const string RepShift = @"(?<shift>[\-\+]?\d+(\.\d*)?)";
        const string Rep3H = @"(?<counts>\d+(\.\d+)?[A-Z][a-z]?)";
        const string RepPattern = @"(?<pattern>(br )?[a-z]+)";
        const string RepJvalues = @"(?<JValue>\d+(\.\d+)?\s*(Hz)?)(\s*\,\s*(?<JValue>\d+(\.\d+)?\s*(Hz)?))*";
        const string RepComment = @"(?<comment>.+)";
        public static Regex ReTabSpec = new Regex($"^{RepShift}\\t{Rep3H}(\\t{RepPattern}(\\t{RepJvalues})?)?(\\t{RepComment})?$", RegexOptions.Compiled);

        public class Range
        {
            public int Start { get; set; }
            public int Length { get; set; }

            public Range(int start, int length)
            {
                this.Start = start;
                this.Length = length;
            }
        }

        public class PeakInfo
        {
            public string Shift { get; set; }
            public string Integration { get; set; }
            public string Pattern { get; set; }
            public IList<string> JValues { get; set; }
            public Range CommentRange { get; set; }
        }

        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            // add sentinel
            text = text + "\xffff";
            int lineStart = 0;
            while (true)
            {
                string line = null;
                int i = lineStart;
                while (true)
                {
                    var c = text[i++];
                    switch (c)
                    {
                        case '\r':
                        case '\n':
                            line = text.Substring(lineStart, i - lineStart);
                            if (c == '\r' && text[i] == '\n')
                                i++;
                            goto L_LineFound;
                        case '\xffff':
                            goto L_EndOfSelection;
                    }
                    i++;
                }
                L_LineFound:
                var match = ReTabSpec.Match(line);
                if (!match.Success)
                    return new List<PCommand>(0);
                PeakInfo info = ExtractPeakInfo(match);
            }
            L_EndOfSelection:


            return commands;
        }

        public static PeakInfo ExtractPeakInfo(Match match)
        {
            PeakInfo info = new PeakInfo();
            info.Shift = match.Groups["shift"].Value;
            info.Integration = match.Groups["counts"].Value;
            info.Pattern = match.Groups["pattern"].Value;
            {
                var jinfo = new List<string>();
                foreach (Capture c in match.Groups["JValue"].Captures)
                {
                    jinfo.Add(c.Value);
                }
                info.JValues = jinfo.Count == 0 ? null : jinfo;
            }
            {
                var g = match.Groups["comment"];
                if (g.Captures.Count > 0)
                    info.CommentRange = new Range(g.Index, g.Length);
            }

            return info;
        }
    }
}
