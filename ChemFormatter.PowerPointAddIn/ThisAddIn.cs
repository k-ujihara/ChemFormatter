using System;
using System.Collections.Generic;

namespace ChemFormatter.PowerPointAddIn
{
    public partial class ThisAddIn
    {
        internal void Fire(Func<string, IEnumerable<PCommand>> makeCommand, bool normalize = true)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            if (normalize)
                text = Utility.Normalize(text);
            var commands = makeCommand(text);
            Applyer.Apply(commands);
        }

        internal void ButtonRDigitChanger_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(RDigitQuery.MakeCommand);
        }

        internal void ButtonChemFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(ChemFormulaQuery.MakeCommand);
        }

        internal void ButtonIonFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(IonFormulaQuery.MakeCommand);
        }

        internal void ButtonChemName_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(ChemNameQuery.MakeCommand);
        }

        internal void ButtonStyleCitation_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(JournalReferenceQuery.MakeCommand);
        }

        public void ButtonAlphaD_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(AlphaDQuery.MakeCommand);
        }

        internal void ButtonStyleAsChar_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Fire(StyleByCharQuery.MakeCommand, normalize: false);
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
