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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace ChemFormatter.ExcelAddIn
{
    public partial class ThisAddIn
    {
        private void FormatThem(Excel.Range cells, Func<string, IEnumerable<PCommand>> commandMaker)
        {
            switch (cells.Count)
            {
                case 0:
                    return;
                case 1:
                    string text = cells.Text;
                    text = Utility.Normalize(text);
                    var commands = commandMaker(text);
                    cells.Select();
                    Applyer.Apply(commands);
                    break;
                default:
                    foreach (Excel.Range cell in cells)
                        FormatThem(cell, commandMaker);
                    break;
            }
        }

        internal void Fire(Func<string, IEnumerable<PCommand>> makeCommand)
        {
            Excel.Range keep = null;
            try
            {
                keep = Globals.ThisAddIn.Application.Selection;
                FormatThem(keep, text => makeCommand(text));
            }
            catch (Exception)
            {
            }
            finally
            {
                if (keep != null)
                    keep.Select();
            }
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
