using System.Collections.Generic;

namespace ChemFormatter
{
    public static class RDigitQuery
    {
        
        public static List<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            CommandFactory.AddRDigitCommands(commands, text);

            return commands;
        }
    }
}
