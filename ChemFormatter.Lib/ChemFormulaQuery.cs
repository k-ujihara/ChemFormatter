using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public static class ChemFormulaQuery
    {
        static Regex ReRDigit { get; } = new Regex(@"[\)\}\]A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);
        static Regex ReTripleBond { get; } = new Regex(@"\#", RegexOptions.Compiled);

        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            foreach (Match match in ReRDigit.Matches(text))
            {
                var g = match.Groups["subdigit"];
                commands.Add(new SubscriptCommand(g.Index, g.Length));
            }

            foreach (Match match in ReTripleBond.Matches(text))
            {
                commands.Add(new ReplaceStringCommand(match.Index, match.Length, "≡"));
            }

            return commands;
        }
    }
}
