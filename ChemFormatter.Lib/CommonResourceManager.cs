using System;

namespace ChemFormatter
{
    public static class CommonResourceManager
    {
        public const string RDigitChangerImage = "RDigitChanger.png";
        public const string ChemFormulaImage = "ChemFormula.png";
        public const string IonFormulaImage = "IonFormula.png";
        public const string ChemNameImage = "ChemName.png";

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
