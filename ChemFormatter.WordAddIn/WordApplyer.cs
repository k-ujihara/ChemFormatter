using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Microsoft.Office.Tools.Ribbon;
using System.Text.RegularExpressions;

namespace ChemFormatter.WordAddIn
{
    public static class Applyer
    {
        public static void ButtonRDigitChanger_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = RDigitQuery.MakeRDigitCommand(text);
            if (commands == null)
                return;

            var save = KeepSelection();
            try
            {
                Apply(commands);
            }
            finally
            {
                RestoreSelection(save);
            }
        }

        public static void Apply(List<PCommand> commands)
        {
            var app = Globals.ThisAddIn.Application;
            var start = app.Selection.Start;
            using (var saver = new AutoCorrectSaver())
            {
                foreach (var command in commands)
                {
                    switch (command)
                    {
                        case ChangeScriptCommand ssc:
                            if (ssc.Length == 0)
                                break;

                            app.Selection.SetRange(start + ssc.Start, start + ssc.Start + 1);
                            ScriptMode next;
                            if (app.Selection.Font.Subscript == (int)Office.MsoTriState.msoTrue)
                                next = ScriptMode.Superscript;
                            else if (app.Selection.Font.Superscript == (int)Office.MsoTriState.msoTrue)
                                next = ScriptMode.Normal;
                            else
                                next = ScriptMode.Subscript;

                            app.Selection.SetRange(start + ssc.Start, start + ssc.Start + ssc.Length);
                            switch (next)
                            {
                                case ScriptMode.Superscript:
                                    app.Selection.Font.Superscript = (int)Office.MsoTriState.msoTrue;
                                    break;
                                case ScriptMode.Normal:
                                    app.Selection.Font.Subscript = (int)Office.MsoTriState.msoFalse;
                                    app.Selection.Font.Superscript = (int)Office.MsoTriState.msoFalse;
                                    break;
                                case ScriptMode.Subscript:
                                default:
                                    app.Selection.Font.Subscript = (int)Office.MsoTriState.msoTrue;
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        public static SelectionKeeper KeepSelection()
        {
            var sel = Globals.ThisAddIn.Application.Selection;
            var start = sel.Start;
            var end = sel.End;
            return new SelectionKeeper(start, end);
        }

        public static void RestoreSelection(SelectionKeeper selection)
        {
            Globals.ThisAddIn.Application.Selection.SetRange(selection.Start, selection.End);
        }
    }
}
