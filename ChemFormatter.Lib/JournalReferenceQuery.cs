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
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    public static class JournalReferenceQuery
    {
        const string RepName = @"(?<name>(\w+\.?)( \w+\.?)*)";
        const string RepYear = @"(?<year>\d\d\d\d)";
        const string RepVolumeNo = @"(?<volume>\d+)(?<no>\(\d+\))?";
        const string RepPages = @"(?<pages>\d+(\-\d+)?)";

        static Regex ReACSStyle { get; } = new Regex(
            $"\\b{RepName} {RepYear}\\, {RepVolumeNo}\\, {RepPages}\\b", RegexOptions.Compiled);

        static Regex ReNatureStyle { get; } = new Regex(
            $"\\b{RepName} {RepVolumeNo}\\, {RepPages} \\({RepYear}\\)", RegexOptions.Compiled);

        public static IList<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            foreach (Match match in ReACSStyle.Matches(text))
            {
                Group g;
                g = match.Groups["name"];
                commands.Add(new ItalicCommand(g.Index, g.Length));
                g = match.Groups["year"];
                commands.Add(new BoldCommand(g.Index, g.Length));
                g = match.Groups["volume"];
                commands.Add(new ItalicCommand(g.Index, g.Length));
            }

            foreach (Match match in ReNatureStyle.Matches(text))
            {
                Group g;
                g = match.Groups["name"];
                commands.Add(new ItalicCommand(g.Index, g.Length));
                g = match.Groups["volume"];
                commands.Add(new BoldCommand(g.Index, g.Length));
            }

            return commands;
        }
    }
}
