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

        public static List<PCommand> MakeCommand(string text)
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
