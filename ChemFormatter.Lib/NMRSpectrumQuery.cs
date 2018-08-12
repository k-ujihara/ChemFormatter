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

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    public static class NMRSpectrumQuery
    {
        const string RepShift = @"(?<shift>[\-\+]?\d+(\.\d*)?(\-[\-\+]?\d+(\.\d*)?)?)";
        const string Rep3H = @"(?<counts>\d+(\.\d+)?[A-Z][a-z]?)";
        const string RepPattern = @"(?<pattern>(br )?[a-z]+)";
        const string RepJvalues = @"(?<JValue>\d+(\.\d+)?\s*(Hz)?)(\s*\,\s*(?<JValue>\d+(\.\d+)?\s*(Hz)?))*";
        const string RepComment = @"(?<comment>[^\t]+)";

        // ACS style
        public static Regex ReTabSpecSIPJ = new Regex($"^{RepShift}(\\t({Rep3H})?)?(\\t({RepPattern})?)?(\\t({RepJvalues})?)?(\\t{RepComment})?$", RegexOptions.Compiled);
        // Science style
        public static Regex ReTabSpecSPJI = new Regex($"^{RepShift}(\\t({RepPattern})?)?(\\t({RepJvalues})?)?(\\t({Rep3H})?)?(\\t{RepComment})?$", RegexOptions.Compiled);

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
            public string ChemicalShift { get; set; }
            public string Integration { get; set; }
            public string Pattern { get; set; }
            public IList<string> JValues { get; set; }
            public Range CommentRange { get; set; }

            public void ShiftRange(int shift)
            {
                if (CommentRange != null)
                    CommentRange.Start += shift;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append(ChemicalShift).Append(" (").Append(Integration);
                if (Pattern != null)
                    sb.Append(", ").Append(Pattern);
                if (JValues != null)
                    sb.Append(", J = ").Append(string.Join(", ", JValues)).Append("Hz");
                if (CommentRange != null)
                    sb.Append(", ").Append($"from {CommentRange.Start} to {CommentRange.Start + CommentRange.Length}");
                sb.Append(")");
                return sb.ToString();
            }
        }

        public static IList<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            commands.Add(new MoveToCommand(text.Length));

            foreach (var lineInfo in new LineReader(text))
            {
                Match match;
                match = MatchTabSepNMRSpec(lineInfo.Text);
                if (!match.Success)
                    return new List<PCommand>(0);
                PeakInfo info = ExtractPeakInfo(match);
                info.ShiftRange(lineInfo.Index);

                commands.Add(new TypeTextCommand(info.ChemicalShift));

            }

            return commands;
        }

        public static Match MatchTabSepNMRSpec(string text)
        {
            Match match;
            if (true)
                match = ReTabSpecSIPJ.Match(text);
            if (!match.Success || !match.Groups["counts"].Success)
                match = ReTabSpecSPJI.Match(text);
            if (!match.Success || !match.Groups["counts"].Success)
                match = ReTabSpecSIPJ.Match(text);
            if (!match.Success)
                match = ReTabSpecSPJI.Match(text);
            return match;
        }

        public static PeakInfo ExtractPeakInfo(Match match)
        {
            PeakInfo info = new PeakInfo();
            info.ChemicalShift = match.Groups["shift"].Value;
            {
                var g = match.Groups["counts"];
                if (g.Success)
                    info.Integration = g.Value;
            }
            {
                var g = match.Groups["pattern"];
                if (g.Success)
                    info.Pattern = g.Value;
            }
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
