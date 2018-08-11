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
using System.Globalization;

namespace ChemFormatter.WordAddIn
{
    public static class Applyer
    {
        public static void ButtonRDigitChanger_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = RDigitQuery.MakeCommand(text);
            Apply(commands);
        }

        public static void ButtonChemFormular_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = ChemFormulaQuery.MakeCommand(text);
            Apply(commands);
        }

        public static void ButtonIonFormular_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = IonFormulaQuery.MakeCommand(text);
            Apply(commands);
        }

        public static void ButtonChemName_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = ChemNameQuery.MakeCommand(text);
            Apply(commands);
        }

        public static void ButtonJournalReference_Click(object sender, RibbonControlEventArgs e)
        {
            var text = Globals.ThisAddIn.Application.Selection.Text;
            text = Utility.Normalize(text);
            var commands = JournalReferenceQuery.MakeCommand(text);
            Apply(commands);
        }
        
        public static void Apply(List<PCommand> commands)
        {
            var save = KeepSelection();
            try
            {
                var app = Globals.ThisAddIn.Application;
                var start = app.Selection.Start;
                using (var saver = new AutoCorrectSaver())
                {
                    foreach (var command in commands)
                    {
                        switch (command)
                        {
                            case ReplaceStringCommand rsc:
                                SelectAndAction(start, rsc, () => ReplaceText(save, rsc.Replacement));
                                break;
                            case ItalicCommand ic:
                                SelectAndAction(start, ic, () => app.Selection.Font.Italic = (int)Office.MsoTriState.msoTrue);
                                break;
                            case BoldCommand ic:
                                SelectAndAction(start, ic, () => app.Selection.Font.Bold = (int)Office.MsoTriState.msoTrue);
                                break;
                            case SmallCapitalCommand ic:
                                SelectAndAction(start, ic, () =>
                                {
                                    app.Selection.Font.SmallCaps = (int)Office.MsoTriState.msoTrue;
                                    ReplaceText(save, app.Selection.Text.ToLower(CultureInfo.InvariantCulture));
                                });
                                break;
                            case SubscriptCommand sbsc:
                                SelectAndAction(start, sbsc, () => app.Selection.Font.Subscript = (int)Office.MsoTriState.msoTrue);
                                break;
                            case SuperscriptCommand spsc:
                                SelectAndAction(start, spsc, () => app.Selection.Font.Superscript = (int)Office.MsoTriState.msoTrue);
                                break;
                            case ChangeScriptCommand ssc:
                                SelectAndAction(start, ssc, () =>
                                {
                                    ScriptMode next;
                                    if (app.Selection.Font.Subscript == (int)Office.MsoTriState.msoTrue)
                                        next = ScriptMode.Superscript;
                                    else if (app.Selection.Font.Superscript == (int)Office.MsoTriState.msoTrue)
                                        next = ScriptMode.Normal;
                                    else
                                        next = ScriptMode.Subscript;

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
                                });
                                break;
                        }
                    }
                }
            }
            finally
            {
                RestoreSelection(save);
            }
        }

        private static SelectionKeeper ReplaceText(SelectionKeeper save, string replacement)
        {
            var orginalLength = Globals.ThisAddIn.Application.Selection.Text.Length;
            // adjust saved selection range 
            save.End += (replacement.Length - orginalLength);
            Globals.ThisAddIn.Application.Selection.TypeText(replacement);
            return save;
        }

        private static void SelectAndAction(int start, ApplyFormatCommand command, System.Action action)
        {
            Globals.ThisAddIn.Application.Selection.SetRange(start + command.Start, start + command.Start + command.Length);
            action();
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
