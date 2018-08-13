using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChemFormatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class NMRSpectrumQueryTests
    {
        static void TestLine(string text, string shift, string integration, string pattern, string couplings, int commentLength)
        {
            var m = NMRSpectrumQuery.MatchTabSepNMRSpec(text);
            Assert.IsTrue(m.Success);
            var info = NMRSpectrumQuery.ExtractPeakInfo(m);
            Assert.AreEqual(shift, info.ChemicalShift);
            Assert.AreEqual(integration, info.Integration);
            Assert.AreEqual(pattern, info.Pattern);
            if (couplings == null)
                Assert.IsNull(info.JValues);
            else
                Assert.AreEqual(couplings, string.Join(", ", info.JValues));
            Assert.AreEqual(commentLength, info.CommentRange == null ? 0 : info.CommentRange.Length);

            text = text + "\n";
            var cmd = NMRSpectrumQuery.MakeCommand(text);
        }

        [TestMethod()]
        public void MakeCommandTest()
        {
            TestLine("1.23\t1H\ts", "1.23", "1H", "s", null, 0);
            // ACS style
            TestLine("1.23", "1.23", null, null, null, 0);
            TestLine("1.23\t1H", "1.23", "1H", null, null, 0);
            TestLine("1.23\ts", "1.23", null, "s", null, 0);
            TestLine("1.23\t1H\ts", "1.23", "1H", "s", null, 0);
            TestLine("1.23\t1H\tbr s", "1.23", "1H", "br s", null, 0);
            TestLine("1.23\t1H\td\t1.3", "1.23", "1H", "d", "1.3", 0);
            TestLine("1.23\t1H\tdd\t4.5, 1.3", "1.23", "1H", "dd", "4.5, 1.3", 0);
            TestLine("1.23\t1H\tdd\t4.5, 1.3\tCH", "1.23", "1H", "dd", "4.5, 1.3", 2);
            TestLine("1.23-1.55\t1H\tm\t\tCH", "1.23-1.55", "1H", "m", null, 2);
            TestLine("1.23\t1H\ts\tCH", "1.23", "1H", "s", null, 2);

            // Science style
            TestLine("1.23\ts\t1H", "1.23", "1H", "s", null, 0);
            TestLine("1.23\tbr s\t1H", "1.23", "1H", "br s", null, 0);
            TestLine("1.23\td\t1.3\t1H", "1.23", "1H", "d", "1.3", 0);
            TestLine("1.23\tdd\t4.5, 1.3\t1H", "1.23", "1H", "dd", "4.5, 1.3", 0);
            TestLine("1.23\tdd\t4.5, 1.3\t1H\tCH", "1.23", "1H", "dd", "4.5, 1.3", 2);
            TestLine("1.23-1.55\tm\t\t1H\tCH", "1.23-1.55", "1H", "m", null, 2);
            TestLine("1.23\ts\t1H\tCH", "1.23", "1H", "s", null, 2);
        }
    }
}