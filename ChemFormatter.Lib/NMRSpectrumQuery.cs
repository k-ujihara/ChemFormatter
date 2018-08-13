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
using System.Text;
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    public static class NMRSpectrumQuery
    {
        const string RepShift = @"(?<shift>[\-\+]?\d+(?:\.\d*)?(?:\-[\-\+]?\d+(?:\.\d*)?)?)";
        const string Rep3H = @"(?<integration>\d+(?:\.\d+)?[A-Z][a-z]?)";
        const string RepPattern = @"(?<pattern>(br )?(?:m|s|d|t|q|quin|sext|sep|oct|dd|dt|dq|td|tt|tq|qd|qt|qq))";
        const string RepJvalues = @"(?<JValue>\d+(?:\.\d+)?)(?:\s+Hz)?(?:\s*\,\s*(?<JValue>\d+(?:\.\d+)?)(?:\s+Hz)?)*";
        const string RepComment = @"(?<comment>[^\s]*)";

        // ACS style
        public static Regex ReTabSpecSIPJ = new Regex("^" + RepShift + @"(\t(?:" + Rep3H + ")?)?(?:\t(?:" + RepPattern + @")?)?(?:\t(?:" + RepJvalues + @")?)?(?:\t" + RepComment + ")?$", RegexOptions.Compiled);
        // Science style
        public static Regex ReTabSpecSPJI = new Regex("^" + RepShift + "(\t(" + RepPattern + ")?)?(\t(" + RepJvalues + @")?)?(\t(" + Rep3H + ")?)?(\t" + RepComment + ")?$", RegexOptions.Compiled);

        public static Regex ReRsSC = new Regex("^" + RepShift + @"( \(" + RepComment + @"\))?$", RegexOptions.Compiled);
        // ACS style
        public static Regex ReRsSIPJC = new Regex("^" + RepShift + @"( \(" + Rep3H + @"(\, " + RepPattern + @"(\, J \= " + RepJvalues + @")?)?(\, " + RepComment + @")?\))?$", RegexOptions.Compiled);

        public class PeakInfo
        {
            public string ChemicalShift { get; set; }
            public string Integration { get; set; }
            public string Pattern { get; set; }
            public IList<string> JValues { get; set; }
            public Range CommentRange { get; set; }

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

        public static List<PCommand> TryMakeCommandTabToSpec(string text)
        {
            var commands = new List<PCommand>();

            int removeStart = 0;
            UnselectDelta(ref removeStart, ref text);
            if (text.StartsWith("\r"))
            {
                text = text.Substring(1);
                removeStart += 1;
            }
            {
                var trim = text.TrimEnd();
                if (trim.EndsWith("\r."))
                    text = trim.Substring(0, trim.Length - 1);
            }
            commands.Add(new ChangeSelectionCommand(removeStart, text.Length));

            commands.Add(new MoveToCommand(text.Length));
            commands.Add(new FontResetCommand());

            bool isFirst = true;
            foreach (var lineInfo in new LineReader(text))
            {
                if (isFirst)
                    isFirst = false;
                else
                    commands.Add(new TypeTextCommand(", "));

                Match match;
                match = MatchTabSeparatedSpec(lineInfo.Text);
                if (!match.Success)
                    return null;
                PeakInfo info = CreatePeakInfo(match, lineInfo.Index);

                commands.Add(new TypeTextCommand(info.ChemicalShift));
                bool needComma = false;
                if (info.Integration != null || info.Pattern != null || info.JValues != null || info.CommentRange != null)
                {
                    commands.Add(new TypeTextCommand(" ("));
                    if (info.Integration != null)
                    {
                        if (needComma)
                            commands.Add(new TypeTextCommand(", "));
                        else
                            needComma = true;
                        commands.Add(new TypeTextCommand(info.Integration));
                    }
                    if (info.Pattern != null)
                    {
                        if (needComma)
                            commands.Add(new TypeTextCommand(", "));
                        else
                            needComma = true;
                        commands.Add(new TypeTextCommand(info.Pattern));
                    }
                    if (info.JValues != null)
                    {
                        if (needComma)
                            commands.Add(new TypeTextCommand(", "));
                        else
                            needComma = true;
                        commands.Add(new SetItalicCommand());
                        commands.Add(new TypeTextCommand("J"));
                        commands.Add(new FontResetCommand());
                        commands.Add(new TypeTextCommand($" = {string.Join(", ", info.JValues)} Hz"));
                    }
                    if (info.CommentRange != null)
                    {
                        if (needComma)
                            commands.Add(new TypeTextCommand(", "));
                        else
                            needComma = true;
                        commands.Add(new CopyAndPasteCommand(info.CommentRange.Start, info.CommentRange.Length));
                        commands.Add(new FontResetCommand());
                    }
                    commands.Add(new TypeTextCommand(")"));
                }
            }
            commands.Add(new ReplaceStringCommand(0, text.Length, ""));
            commands.Add(new MoveToCommand(0));
            commands.Add(new TypeBackspaceCommand());

            return commands;
        }

        public static List<PCommand> TryMakeCommandSpecToTab(string text)
        {
            var commands = new List<PCommand>();

            int removeStart = 0;
            UnselectDelta(ref removeStart, ref text);
            text = text.TrimEnd();
            if (text.EndsWith("."))
                text = text.Substring(0, text.Length - 1);

            commands.Add(new ChangeSelectionCommand(removeStart, text.Length));
            commands.Add(new MoveToCommand(text.Length));

            foreach (var lineInfo in new ListReader(text))
            {
                Match match;
                match = MatchNMRSpec(lineInfo.Text);
                if (!match.Success)
                    return null;
                PeakInfo info = CreatePeakInfo(match, lineInfo.Index);

                commands.Add(new TypeParagraphCommand());
                commands.Add(new FontResetCommand());
                commands.Add(new TypeTextCommand(info.ChemicalShift));
                commands.Add(new TypeTextCommand("\t"));
                if (info.Integration != null)
                    commands.Add(new TypeTextCommand(info.Integration));
                commands.Add(new TypeTextCommand("\t"));
                if (info.Pattern != null)
                    commands.Add(new TypeTextCommand(info.Pattern));
                commands.Add(new TypeTextCommand("\t"));
                if (info.JValues != null)
                    commands.Add(new TypeTextCommand(string.Join(", ", info.JValues)));
                commands.Add(new TypeTextCommand("\t"));
                if (info.CommentRange != null)
                    commands.Add(new CopyAndPasteCommand(info.CommentRange.Start, info.CommentRange.Length));
            }
            commands.Add(new TypeParagraphCommand());
            commands.Add(new ReplaceStringCommand(0, text.Length, ""));

            return commands;
        }

        private static void UnselectDelta(ref int removeStart, ref string text)
        {
            if (text.StartsWith("δ"))
            {
                text = text.Substring(1);
                removeStart++;
            }
            {
                var trim = text.TrimStart();
                removeStart += (text.Length - trim.Length);
                text = trim;
            }
        }

        public static IEnumerable<PCommand> MakeCommand(string text)
        {
            List<PCommand> commands;

            commands = TryMakeCommandTabToSpec(text);
            if (commands != null)
                return commands;

            commands = TryMakeCommandSpecToTab(text);
            if (commands != null)
                return commands;

            return Array.Empty<PCommand>();
        }

        public static Match MatchNMRSpec(string text)
        {
            Match match;
            match = ReRsSIPJC.Match(text);
            if (match.Success) goto L_Return;
            match = ReRsSC.Match(text);
            L_Return:
            return match;
        }

        public static Match MatchTabSeparatedSpec(string text)
        {
            Match match;
            match = ReTabSpecSIPJ.Match(text);
            if (match.Success && match.Groups["integration"].Success)
                goto L_Return;
            match = ReTabSpecSPJI.Match(text);
            if (match.Success && match.Groups["integration"].Success)
                goto L_Return;
            match = ReTabSpecSIPJ.Match(text);
            if (match.Success)
                goto L_Return;
            match = ReTabSpecSPJI.Match(text);
            L_Return:
            return match;
        }

        public static PeakInfo CreatePeakInfo(Match match, int rangeShift)
        {
            PeakInfo info = new PeakInfo
            {
                ChemicalShift = match.Groups["shift"].Value
            };

            {
                var g = match.Groups["integration"];
                if (g.Success)
                {
                    var s = g.Value.Trim();
                    if (s.Length > 0)
                        info.Integration = g.Value;
                }
            }
            {
                var g = match.Groups["pattern"];
                if (g.Success)
                {
                    var s = g.Value.Trim();
                    if (s.Length > 0)
                        info.Pattern = g.Value;
                }
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
                {
                    var s = g.Value.Trim();
                    if (s.Length > 0)
                        info.CommentRange = new Range(g.Index + rangeShift, g.Length);
                }
            }

            return info;
        }
    }
}
