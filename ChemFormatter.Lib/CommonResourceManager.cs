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

namespace ChemFormatter
{
    public static class CommonResourceManager
    {
        public const string RDigitChangerImage = "RDigitChanger.png";
        public const string ChemFormulaImage = "ChemFormula.png";
        public const string IonFormulaImage = "IonFormula.png";
        public const string ChemNameImage = "ChemName.png";
        public const string JournalReferenceImage = "Journal.png";
        public const string NMRSpecImage = "NMRSpectrum.png";
        public const string NMRFormat_IPJC = "NMR_IPJC.png";
        public const string NMRFormat_PJIC = "NMR_PJIC.png";
        public const string NMRFormat_PlainIPJC = "NMR_PlainIPJC.png";
        public const string NMRFormat_PlainPJIC = "NMR_PlainPJIC.png";

        public static System.Drawing.Image GetImage(string name)
        {
            using (var stream = typeof(CommonResourceManager).Assembly.GetManifestResourceStream($"{nameof(ChemFormatter)}.Resources.{name}"))
            {
                if (stream == null)
                {
                    System.Diagnostics.Trace.TraceError($"Missing resource: {name}");
                    return null;
                }
                return System.Drawing.Image.FromStream(stream);
            }
        }
    }
}
