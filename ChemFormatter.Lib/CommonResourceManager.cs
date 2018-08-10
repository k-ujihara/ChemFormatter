using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemFormatter
{
    public static class CommonResourceManager
    {
        public const string RDigitChangerImage = "RDigitChanger.png";

        public static System.Drawing.Image GetImage(string name)
        {
            using (var stream = typeof(CommonResourceManager).Assembly.GetManifestResourceStream($"{nameof(ChemFormatter)}.Resources.{name}"))
            {
                if (stream == null)
                    throw new Exception($"Missing resource: {name}");
                return System.Drawing.Image.FromStream(stream);
            }
        }
    }
}
