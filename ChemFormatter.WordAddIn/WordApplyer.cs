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

using System.Collections.Generic;
using System.Globalization;
using Office = Microsoft.Office.Core;

namespace ChemFormatter.WordAddIn
{
    public static class WordApplyer
    {
        public static void Apply(IEnumerable<PCommand> commands)
        {
            var save = KeepSelection();
            try
            {
                var app = Globals.ThisAddIn.Application;
                using (var saver = new AutoCorrectSaver())
                {
                    foreach (var command in commands)
                    {
                        switch (command)
                        {
                            case ChangeSelectionCommand cmd:
                                save.Start = save.Start + cmd.Start;
                                save.Length = cmd.Length;
                                break;
                            case TypeParagraphCommand cmd:
                                app.Selection.TypeParagraph();
                                save.Length += 1;
                                break;
                            case TypeBackspaceCommand cmd:
                                app.Selection.TypeBackspace();
                                if (app.Selection.Start < save.Start)
                                    save.Start--;
                                else
                                    save.Length--;
                                break;
                            case FontResetCommand cmd:
                                app.Selection.Font.Reset();
                                break;
                            case EnableItalicCommand cmd:
                                app.Selection.Font.Italic = (int)Office.MsoTriState.msoTrue;
                                break;
                            case CopyAndPasteCommand cmd:
                                var keepSelection = KeepSelection();
                                app.Selection.SetRange(save.Start + cmd.Start, save.Start + cmd.Start + cmd.Length);
                                app.Selection.Copy();
                                RestoreSelection(keepSelection);
                                app.Selection.Paste();
                                save.Length += cmd.Length;
                                break;
                            case MoveToCommand cmd:
                                app.Selection.SetRange(save.Start + cmd.Position, save.Start + cmd.Position);
                                break;
                            case TypeTextCommand cmd:
                                app.Selection.End = app.Selection.Start;
                                app.Selection.TypeText(cmd.Text);
                                save.Length += cmd.Text.Length;
                                break;
                            case ReplaceStringCommand cmd:
                                SelectAndAction(save.Start, cmd, () =>
                                    {
                                        save.Length += (cmd.Replacement.Length - cmd.Length);
                                        if (cmd.Replacement.Length == 0)
                                            app.Selection.Delete();
                                        else
                                            app.Selection.TypeText(cmd.Replacement);
                                    });
                                break;
                            case ItalicCommand cmd:
                                SelectAndAction(save.Start, cmd, () => app.Selection.Font.Italic = (int)Office.MsoTriState.msoTrue);
                                break;
                            case BoldCommand cmd:
                                SelectAndAction(save.Start, cmd, () => app.Selection.Font.Bold = (int)Office.MsoTriState.msoTrue);
                                break;
                            case SmallCapitalCommand cmd:
                                SelectAndAction(save.Start, cmd, () =>
                                    {
                                        app.Selection.Font.SmallCaps = (int)Office.MsoTriState.msoTrue;
                                        var lower = app.Selection.Text.ToLower(CultureInfo.InvariantCulture);
                                        app.Selection.TypeText(lower);
                                    });
                                break;
                            case SubscriptCommand cmd:
                                SelectAndAction(save.Start, cmd, () => app.Selection.Font.Subscript = (int)Office.MsoTriState.msoTrue);
                                break;
                            case SuperscriptCommand cmd:
                                SelectAndAction(save.Start, cmd, () => app.Selection.Font.Superscript = (int)Office.MsoTriState.msoTrue);
                                break;
                            case ChangeScriptCommand cmd:
                                SelectAndAction(save.Start, cmd, () =>
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

        private static void SelectAndAction(int start, RangeCommand command, System.Action action)
        {
            Globals.ThisAddIn.Application.Selection.SetRange(start + command.Start, start + command.Start + command.Length);
            action();
        }

        public static Range KeepSelection()
        {
            var sel = Globals.ThisAddIn.Application.Selection;
            var start = sel.Start;
            var end = sel.End;
            return new Range(start, end - start);
        }

        public static void RestoreSelection(Range selection)
        {
            Globals.ThisAddIn.Application.Selection.SetRange(selection.Start, selection.End);
        }
    }
}
