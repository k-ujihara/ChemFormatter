using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public static class RDigitQuery
    {
        static Regex ReRDigit = new Regex(@"[A-Za-z](?<subdigit>\d+(\-\d+)?)", RegexOptions.Compiled);

        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            var matches = ReRDigit.Matches(text);
            foreach (Match match in matches)
            {
                var g = match.Groups["subdigit"];
                commands.Add(new ChangeScriptCommand(g.Index, g.Length));
            }

            return commands;
        }
    }
}
