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
using System.Globalization;
using Office = Microsoft.Office.Core;

namespace ChemFormatter.ExcelAddIn
{
    public static class Applyer
    {
        public static void Apply(IEnumerable<PCommand> commands)
        {
            var save = KeepSelection();
            try
            {
                var app = Globals.ThisAddIn.Application;
                int pos = 0;
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
                            pos = cmd.Position;
                            break;
                        case TypeTextCommand cmd:
                            app.ActiveCell.Characters[pos, 0].Insert(cmd.Text);
                            pos += cmd.Text.Length;
                            save.Length += cmd.Text.Length;
                            break;
                        case ReplaceStringCommand cmd:
                            app.ActiveCell.Characters[save.Start + cmd.Start, cmd.Length].Insert(cmd.Replacement);
                            save.Length += (cmd.Replacement.Length - cmd.Length);
                            break;
                        case ItalicCommand cmd:
                            SelectAndAction(save.Start, cmd, (chars) => chars.Font.Italic = true);
                            break;
                        case BoldCommand cmd:
                            SelectAndAction(save.Start, cmd, (chars) => chars.Font.Bold = true);
                            break;
                        case SmallCapitalCommand cmd:
                            try
                            {
                                double size = app.ActiveCell.Font.Size;
                                SelectAndAction(save.Start, cmd, (chars) => chars.Font.Size = size * 0.8);
                            }
                            catch (Exception)
                            {  
                            }
                            break;
                        case SubscriptCommand cmd:
                            SelectAndAction(save.Start, cmd, (chars) => chars.Font.Subscript = true);
                            break;
                        case SuperscriptCommand cmd:
                            SelectAndAction(save.Start, cmd, (chars) => chars.Font.Superscript = true);
                            break;
                        case ChangeScriptCommand cmd:
                            SelectAndAction(save.Start, cmd, (chars) =>
                            {
                                ScriptMode next = ScriptMode.Subscript;
                                try
                                {
                                    if (chars.Font.Subscript != false)
                                        next = ScriptMode.Superscript;
                                    else if (chars.Font.Superscript != false)
                                        next = ScriptMode.Normal;
                                    else
                                        next = ScriptMode.Subscript;
                                }
                                catch (Exception)
                                {
                                }

                                switch (next)
                                {
                                    case ScriptMode.Superscript:
                                        chars.Font.Superscript = true;
                                        break;
                                    case ScriptMode.Normal:
                                        chars.Font.Subscript = false;
                                        chars.Font.Superscript = false;
                                        break;
                                    case ScriptMode.Subscript:
                                    default:
                                        chars.Font.Subscript = true;
                                        break;
                                }
                            });
                            break;
                    }
                }
            }
            finally
            {
                RestoreSelection(save);
            }
        }

        private static void SelectAndAction(int start, RangeCommand command, Action<Microsoft.Office.Interop.Excel.Characters> action)
        {
            var chars = Globals.ThisAddIn.Application.ActiveCell.Characters[start + command.Start, command.Length];
            action(chars);
        }

        public static Range KeepSelection()
        {
            var sel = Globals.ThisAddIn.Application.Selection;
            var text = sel.Text;
            return new Range(1, text.Length);
        }

        public static void RestoreSelection(Range selection)
        {
        }
    }
}
