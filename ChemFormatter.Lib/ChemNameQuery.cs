using System.Collections.Generic;

namespace ChemFormatter
{
    public static class ChemNameQuery
    {        
        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            CommandFactory.AddChemPrefixCommands(commands, text);

            return commands;
        }
    }
}
