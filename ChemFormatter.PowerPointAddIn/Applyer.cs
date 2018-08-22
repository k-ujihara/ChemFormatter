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
using Office = Microsoft.Office.Core;

namespace ChemFormatter.PowerPointAddIn
{
    public static class Applyer
    {
        public static void Apply(IEnumerable<PCommand> commands)
        {
            var save = KeepSelection();
            try
            {
                var app = Globals.ThisAddIn.Application;
                var aw = app.ActiveWindow;
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
                                throw new ApplicationException();
                            case TypeBackspaceCommand cmd:
                                throw new ApplicationException();
                            case FontResetCommand cmd:
                                throw new ApplicationException();
                            case SetItalicCommand cmd:
                                throw new ApplicationException();
                            case CopyAndPasteCommand cmd:
                                throw new ApplicationException();
                            case MoveToCommand cmd:
                                aw.Selection.TextRange.Characters(cmd.Position, 0).Select();
                                break;
                            case TypeTextCommand cmd:
                                aw.Selection.TextRange.Characters(aw.Selection.TextRange.Start, 0).Select();
                                aw.Selection.TextRange.Text = cmd.Text;
                                save.Length += cmd.Text.Length;
                                break;
                            case ReplaceStringCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) =>
                                    {
                                        save.Length += (cmd.Replacement.Length - cmd.Length);
                                        range.Text = cmd.Replacement;
                                    });
                                break;
                            case ItalicCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) => range.Font.Italic = Office.MsoTriState.msoTrue);
                                break;
                            case BoldCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) => range.Font.Bold = Office.MsoTriState.msoTrue);
                                break;
                            case SmallCapitalCommand cmd:
                                var normalFontSize = aw.Selection.TextRange.Characters(save.Start + cmd.Start + 1, 1).Font.Size;
                                SelectAndAction(save.Start, cmd, (range) =>
                                    {
                                        range.Font.Size = normalFontSize * 0.8f; 
                                    });
                                break;
                            case SubscriptCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) => range.Font.Subscript = Office.MsoTriState.msoTrue);
                                break;
                            case SuperscriptCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) => range.Font.Superscript = Office.MsoTriState.msoTrue);
                                break;
                            case ChangeScriptCommand cmd:
                                SelectAndAction(save.Start, cmd, (range) =>
                                {
                                    ScriptMode next;
                                    if (range.Font.Subscript == Office.MsoTriState.msoTrue)
                                        next = ScriptMode.Superscript;
                                    else if (range.Font.Superscript == Office.MsoTriState.msoTrue)
                                        next = ScriptMode.Normal;
                                    else
                                        next = ScriptMode.Subscript;

                                    switch (next)
                                    {
                                        case ScriptMode.Superscript:
                                            range.Font.Superscript = Office.MsoTriState.msoTrue;
                                            break;
                                        case ScriptMode.Normal:
                                            range.Font.Subscript = Office.MsoTriState.msoFalse;
                                            range.Font.Superscript = Office.MsoTriState.msoFalse;
                                            break;
                                        case ScriptMode.Subscript:
                                        default:
                                            range.Font.Subscript = Office.MsoTriState.msoTrue;
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

        private static void SelectAndAction(int start, RangeCommand command, Action<Microsoft.Office.Interop.PowerPoint.TextRange> action)
        {
            Microsoft.Office.Interop.PowerPoint.TextFrame parent = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Parent;
            var range = parent.TextRange.Characters(start + command.Start, command.Length);
            action(range);
        }

        public static Range KeepSelection()
        {
            var sel = Globals.ThisAddIn.Application.ActiveWindow.Selection;
            return new Range(sel.TextRange.Start, sel.TextRange.Length);
        }

        public static void RestoreSelection(Range selection)
        {
            Microsoft.Office.Interop.PowerPoint.TextFrame parent = Globals.ThisAddIn.Application.ActiveWindow.Selection.TextRange.Parent;
            parent.TextRange.Characters(selection.Start, selection.Length).Select();
        }
    }
}
