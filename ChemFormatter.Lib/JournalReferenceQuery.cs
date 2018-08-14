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
        const string RepMonth = @"(?<month>(January|February|March|April|May|June|July|August|September|October|November|December|Jan.|Feb.|Mar.|Apr.|May.|Jun.|Jul.|Aug.|Sept.|Oct.|Nov.|Dec.))";
        const string RepVolume = @"(?<volume>(ED\-)?\d+)";
        const string RepNo = @"(?<no>\d+)";
        const string RepPages = @"(?<pages>(A|e)?\d+(\-(A|e)?\d+)?)";

        // <i>J. Am. Chem. Soc.</i> <b>2001</b>, <i>123</i>, 4567-4569.
        static Regex ReACSStyle { get; } = new Regex(
            @"\b" + RepName + " " + RepYear + @"\, " + RepVolume + @"(\(" + RepNo + @"\))?" + @"\, " + RepPages + @"\.", RegexOptions.Compiled);

        // <i>J. Am. Chem. Soc.</i> <b>123</b>, 4567-4569, (2001).
        static Regex ReNatureStyle { get; } = new Regex(
            @"\b" + RepName+ " " + RepVolume + @"(\(" + RepNo + @"\))?" + @"\, " + RepPages + @" \(" + RepYear + @"\)\.", RegexOptions.Compiled);

        // IEEE: http://libguides.murdoch.edu.au/IEEE/journal
        // <i>IEEE Transactions on Image Processing</i>, vol. 19, no. 9, pp. 2265-77, 2010.
        static Regex ReIEEEStyle { get; } = new Regex(
            @"\b" + RepName + @"\, " + "(" + @"[Vv]ol\. " + RepVolume + @"\, " + @"([Nn]o\. " + RepNo + @"\, )?" + @"pp\. " + RepPages + @"\, " + @"(" + RepMonth + @" )?" + RepYear + "|to be published)" + @"\.", RegexOptions.Compiled); 

        public static IEnumerable<PCommand> MakeCommand(string text)
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

            foreach (Match match in ReIEEEStyle.Matches(text))
            {
                Group g;
                g = match.Groups["name"];
                commands.Add(new ItalicCommand(g.Index, g.Length));
            }

            return commands;
        }
    }
}
