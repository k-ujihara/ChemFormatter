namespace ChemFormatter.PowerPointAddIn
{
    public partial class ThisAddIn
    {
        internal void ButtonRDigitChanger_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = RDigitQuery.MakeCommand(text);
            Applyer.Apply(commands);
        }

        internal void ButtonChemFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = ChemFormulaQuery.MakeCommand(text);
            Applyer.Apply(commands);
        }

        internal void ButtonIonFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = IonFormulaQuery.MakeCommand(text);
            Applyer.Apply(commands);
        }

        internal void ButtonChemName_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = ChemNameQuery.MakeCommand(text);
            Applyer.Apply(commands);
        }

        internal void ButtonStyleCitation_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = JournalReferenceQuery.MakeCommand(text);
            Applyer.Apply(commands);
        }

        public void ButtonAlphaD_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Text;
            text = Utility.Normalize(text);
            var commands = AlphaDQuery.MakeCommand(text);
            Applyer.Apply(commands);
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
