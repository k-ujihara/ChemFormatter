using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    public static class CommandFactory
    {
        static Regex ReRDigit = new Regex(@"[A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);
        static Regex ReSubscript { get; } = new Regex(@"[\)\}\]A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);
        static Regex ReIon { get; } = new Regex(@"[\)\}\]A-Za-z0-9](?<superscript>[2-7]?[\+\-])", RegexOptions.Compiled);
        static Regex ReTripleBond { get; } = new Regex(@"\#", RegexOptions.Compiled);
        const string DefaultItalicPrefixes = "syn|anti|meso|racemi|cis|trans|rel|l|d|dl|i|endo|exo|sec|tert|n|s|t|o|m|p|vic|gem|cisoid|transoid|r|t|c|ent|gache|erythro|threo";
        static Regex ReItalicPrefix = new Regex(@"(?<prefix>" + DefaultItalicPrefixes + @")\-", RegexOptions.Compiled);

        public static void AddChemNameCommands(List<PCommand> commands, string text)
        {
            foreach (Match match in ReItalicPrefix.Matches(text))
            {
                var g = match.Groups["prefix"];
                commands.Add(new ItalicCommand(g.Index, g.Length));
            }
        }

        public static void AddRDigitCommands(List<PCommand> commands, string text)
        {
            foreach (Match match in ReRDigit.Matches(text))
            {
                var g = match.Groups["subdigit"];
                commands.Add(new ChangeScriptCommand(g.Index, g.Length));
            }
        }

        public static void AddSubscriptCommands(List<PCommand> commands, string text)
        {
            foreach (Match match in ReSubscript.Matches(text))
            {
                var g = match.Groups["subdigit"];
                commands.Add(new SubscriptCommand(g.Index, g.Length));
            }
        }

        public static void AddIonCommands(List<PCommand> commands, string text)
        {
            foreach (Match match in ReIon.Matches(text))
            {
                var g = match.Groups["superscript"];
                commands.Add(new SuperscriptCommand(g.Index, g.Length));
            }
        }

        public static void AddTripleBondCommands(List<PCommand> commands, string text)
        {
            foreach (Match match in ReTripleBond.Matches(text))
            {
                commands.Add(new ReplaceStringCommand(match.Index, match.Length, "≡"));
            }
        }
    }
}
