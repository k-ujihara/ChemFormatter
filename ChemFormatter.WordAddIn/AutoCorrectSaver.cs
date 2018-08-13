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


namespace ChemFormatter.WordAddIn
{
    public class AutoCorrectSaver : System.IDisposable
    {
        public bool Overtype { get; set; }
        public bool CorrectInitialCaps { get; set; }
        public bool CorrectSentenceCaps { get; set; }
        public bool CorrectTableCells { get; set; }
        public bool CorrectDays { get; set; }
        public bool CorrectCapsLock { get; set; }
        public bool ReplaceText { get; set; }
        public bool ReplaceTextFromSpellingChecker { get; set; }
        public bool DisplayAutoCorrectOptions { get; set; }
        public AutoCorrectSaver()
        {
            var app = Globals.ThisAddIn.Application;
            this.Overtype = app.Options.Overtype;
            this.CorrectInitialCaps = app.AutoCorrect.CorrectInitialCaps;
            this.CorrectSentenceCaps = app.AutoCorrect.CorrectSentenceCaps;
            this.CorrectTableCells = app.AutoCorrect.CorrectTableCells;
            this.CorrectDays = app.AutoCorrect.CorrectDays;
            this.CorrectCapsLock = app.AutoCorrect.CorrectCapsLock;
            this.ReplaceText = app.AutoCorrect.ReplaceText;
            this.ReplaceTextFromSpellingChecker = app.AutoCorrect.ReplaceTextFromSpellingChecker;
            this.DisplayAutoCorrectOptions = app.AutoCorrect.DisplayAutoCorrectOptions;
        }

        public void DisableAll()
        {
            var app = Globals.ThisAddIn.Application;
            app.Options.Overtype = false;
            app.AutoCorrect.CorrectInitialCaps = false;
            app.AutoCorrect.CorrectSentenceCaps = false;
            app.AutoCorrect.CorrectTableCells = false;
            app.AutoCorrect.CorrectDays = false;
            app.AutoCorrect.CorrectCapsLock = false;
            app.AutoCorrect.ReplaceText = false;
            app.AutoCorrect.ReplaceTextFromSpellingChecker = false;
            app.AutoCorrect.DisplayAutoCorrectOptions = false;
        }

        public void Dispose()
        {
            var app = Globals.ThisAddIn.Application;
            app.Options.Overtype = this.Overtype;
            app.AutoCorrect.CorrectInitialCaps = this.CorrectInitialCaps;
            app.AutoCorrect.CorrectSentenceCaps = this.CorrectSentenceCaps;
            app.AutoCorrect.CorrectTableCells = this.CorrectTableCells;
            app.AutoCorrect.CorrectDays = this.CorrectDays;
            app.AutoCorrect.CorrectCapsLock = this.CorrectCapsLock;
            app.AutoCorrect.ReplaceText = this.ReplaceText;
            app.AutoCorrect.ReplaceTextFromSpellingChecker = this.ReplaceTextFromSpellingChecker;
            app.AutoCorrect.DisplayAutoCorrectOptions = this.DisplayAutoCorrectOptions;
        }
    }
}

