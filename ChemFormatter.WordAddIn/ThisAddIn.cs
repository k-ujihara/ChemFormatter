// MIT License
// 
// Copyright (c) 2018 Kazuya Ujihara
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Diagnostics;
using System.Linq;
using static ChemFormatter.DocumentProperties;

namespace ChemFormatter.WordAddIn
{
    public partial class ThisAddIn
    {
        public NMRFormat CurrentNMRFormat { get; set; } = null;

        private static string GetNMRFormatProperty(Microsoft.Office.Interop.Word.Document doc)
        {
            string prop = null;
            try
            {
                prop = doc.CustomDocumentProperties.Item(NMRFormatKey).Value;
            }
            catch (Exception)
            {
            }

            return prop;
        }

        private void SetNMRFormat(string format)
        {
            var ribbon = Globals.Ribbons.GetRibbon<Ribbon>();

            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem find(string label) 
                => ribbon.dropDownNMRFormat.Items.Where(n => n.Label == format).FirstOrDefault();

            var item = find(format);
            if (item == null)
            {
                format = NMRFormat.Default.Label;
                item = find(format);
            }
            if (item == null)
                Trace.TraceError($"Default {nameof(NMRFormat)} should be contained in {ribbon.dropDownNMRFormat}.");

            Globals.Ribbons.GetRibbon<Ribbon>().dropDownNMRFormat.SelectedItem = item;

            CurrentNMRFormat = NMRFormat.FromLabel(format);
        }

        internal void OnDocumentOpen(Microsoft.Office.Interop.Word.Document doc)
        {
            SetNMRFormat(GetNMRFormatProperty(doc));
        }

        private void OnDocumentBeforeSave(Microsoft.Office.Interop.Word.Document doc, ref bool saveAsUI, ref bool cancel)
        {
            var prop = GetNMRFormatProperty(doc);

            if (prop == null && CurrentNMRFormat == NMRFormat.Default)
            {
                // do not save
            }
            else
            {
                try
                {
                    doc.CustomDocumentProperties.Item(NMRFormatKey).Delete();
                }
                catch (Exception)
                {
                }
                try
                { 
                    doc.CustomDocumentProperties.Add(NMRFormatKey, false, Microsoft.Office.Core.MsoDocProperties.msoPropertyTypeString, CurrentNMRFormat.Label, missing);
                }
                catch (Exception e)
                {
                    Trace.TraceError($"Failed to set {nameof(doc.CustomDocumentProperties)}: {e.Message}");
                }
            }
        }

        internal void ButtonRDigitChanger_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = RDigitQuery.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        internal void ButtonChemFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = ChemFormulaQuery.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        internal void ButtonIonFormular_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = IonFormulaQuery.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        internal void ButtonChemName_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = ChemNameQuery.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        internal void ButtonStyleCitation_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = JournalReferenceQuery.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        public void ButtonNMRSpec_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var app = Globals.ThisAddIn.Application;
            var text = app.Selection.Text;
            text = Utility.Normalize(text);
            var query = new NMRSpectrumQuery() { Format = CurrentNMRFormat };
            var commands = query.MakeCommand(text);
            WordApplyer.Apply(commands);
        }

        internal void DropDownNMRFormat_SelectionChanged(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            var ribbon = Globals.Ribbons.GetRibbon<Ribbon>();
            var format = ribbon.dropDownNMRFormat.SelectedItem.Label;
            SetNMRFormat(format);
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Globals.ThisAddIn.Application.DocumentOpen += OnDocumentOpen;
            Globals.ThisAddIn.Application.DocumentBeforeSave += OnDocumentBeforeSave;
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
