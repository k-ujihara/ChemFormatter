

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
