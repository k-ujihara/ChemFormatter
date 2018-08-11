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
            var ac = app.AutoCorrect;
			this.CorrectInitialCaps = ac.CorrectInitialCaps;
			this.CorrectSentenceCaps = ac.CorrectSentenceCaps;
			this.CorrectTableCells = ac.CorrectTableCells;
			this.CorrectDays = ac.CorrectDays;
			this.CorrectCapsLock = ac.CorrectCapsLock;
			this.ReplaceText = ac.ReplaceText;
			this.ReplaceTextFromSpellingChecker = ac.ReplaceTextFromSpellingChecker;
			this.DisplayAutoCorrectOptions = ac.DisplayAutoCorrectOptions;
        }

        public void DisableAll()
		{
            var app = Globals.ThisAddIn.Application;
            var ac = app.AutoCorrect;
			ac.CorrectInitialCaps = false;
			ac.CorrectSentenceCaps = false;
			ac.CorrectTableCells = false;
			ac.CorrectDays = false;
			ac.CorrectCapsLock = false;
			ac.ReplaceText = false;
			ac.ReplaceTextFromSpellingChecker = false;
			ac.DisplayAutoCorrectOptions = false;
		}

        public void Dispose()
        {
            var app = Globals.ThisAddIn.Application;
            var ac = app.AutoCorrect;
			ac.CorrectInitialCaps = this.CorrectInitialCaps;
			ac.CorrectSentenceCaps = this.CorrectSentenceCaps;
			ac.CorrectTableCells = this.CorrectTableCells;
			ac.CorrectDays = this.CorrectDays;
			ac.CorrectCapsLock = this.CorrectCapsLock;
			ac.ReplaceText = this.ReplaceText;
			ac.ReplaceTextFromSpellingChecker = this.ReplaceTextFromSpellingChecker;
			ac.DisplayAutoCorrectOptions = this.DisplayAutoCorrectOptions;
        }
    }
}
