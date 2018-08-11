using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public static class CommandFactory
    {
        static Regex ReRDigit = new Regex(@"[A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);
        static Regex ReSubscript { get; } = new Regex(@"[\)\}\]A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);
        static Regex ReIon { get; } = new Regex(@"[\)\}\]A-Za-z0-9](?<superscript>[2-7]?[\+\-])", RegexOptions.Compiled);
        static Regex ReTripleBond { get; } = new Regex(@"\#", RegexOptions.Compiled);

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
