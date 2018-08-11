using System.Collections.Generic;

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
