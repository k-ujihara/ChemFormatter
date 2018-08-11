using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public static class IonFormulaQuery
    {        
        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            CommandFactory.AddSubscriptCommands(commands, text);
            CommandFactory.AddIonCommands(commands, text);
            CommandFactory.AddTripleBondCommands(commands, text);

            return commands;
        }
    }
}
