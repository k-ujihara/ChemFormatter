using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class NMRSpectrumQueryTests
    {
        static void TestLine(Func<string, Match> f, string text, string shift, string integration, string pattern, string couplings, int commentLength)
        {
            var m = f(text);
            Assert.IsTrue(m.Success);
            var info = NMRSpectrumQuery.CreatePeakInfo(m, 0);
            Assert.AreEqual(shift, info.ChemicalShift);
            Assert.AreEqual(integration, info.Integration);
            Assert.AreEqual(pattern, info.Pattern);
            if (couplings == null)
                Assert.IsNull(info.JValues);
            else
                Assert.AreEqual(couplings, string.Join(", ", info.JValues));
            Assert.AreEqual(commentLength, info.CommentRange == null ? 0 : info.CommentRange.Length);
        }

        static void TestTab(string text, string shift, string integration, string pattern, string couplings, int commentLength)
        {
            TestLine(NMRSpectrumQuery.MatchTabSeparatedSpec, text, shift, integration, pattern, couplings, commentLength);
        }

        static void TestList(string text, string shift, string integration, string pattern, string couplings, int commentLength)
        {
            TestLine(NMRSpectrumQuery.MatchNMRSpec, text, shift, integration, pattern, couplings, commentLength);
        }

        [TestMethod()]
        public void MakeCommandTest()
        {
            TestTab("1.23\t\t\t", "1.23", null, null, null, 0);
            TestTab("1.23\t1H\ts", "1.23", "1H", "s", null, 0);
            TestTab("1.23\t1H\ts\t", "1.23", "1H", "s", null, 0);
            TestTab("1.23\t1H\ts\t\t", "1.23", "1H", "s", null, 0);
            // ACS style
            TestTab("1.23", "1.23", null, null, null, 0);
            TestTab("1.23\t1H", "1.23", "1H", null, null, 0);
            TestTab("1.23\ts", "1.23", null, "s", null, 0);
            TestTab("1.23\t1H\ts", "1.23", "1H", "s", null, 0);
            TestTab("1.23\t1H\tbr s", "1.23", "1H", "br s", null, 0);
            TestTab("1.23\t1H\td\t1.3", "1.23", "1H", "d", "1.3", 0);
            TestTab("1.23\t1H\tdd\t4.5, 1.3", "1.23", "1H", "dd", "4.5, 1.3", 0);
            TestTab("1.23\t1H\tdd\t4.5, 1.3\tCH", "1.23", "1H", "dd", "4.5, 1.3", 2);
            TestTab("1.23-1.55\t1H\tm\t\tCH", "1.23-1.55", "1H", "m", null, 2);
            TestTab("1.23\t1H\ts\tCH", "1.23", "1H", "s", null, 2);

            // Science style
            TestTab("1.23\ts\t1H", "1.23", "1H", "s", null, 0);
            TestTab("1.23\tbr s\t1H", "1.23", "1H", "br s", null, 0);
            TestTab("1.23\td\t1.3\t1H", "1.23", "1H", "d", "1.3", 0);
            TestTab("1.23\tdd\t4.5, 1.3\t1H", "1.23", "1H", "dd", "4.5, 1.3", 0);
            TestTab("1.23\tdd\t4.5, 1.3\t1H\tCH", "1.23", "1H", "dd", "4.5, 1.3", 2);
            TestTab("1.23-1.55\tm\t\t1H\tCH", "1.23-1.55", "1H", "m", null, 2);
            TestTab("1.23\ts\t1H\tCH", "1.23", "1H", "s", null, 2);
        }

        [TestMethod()]
        public void MakeRCommandTest()
        {
            TestList("1.00", "1.00", null, null, null, 0);
            TestList("1.00 (comment)", "1.00", null, null, null, 7);
            TestList("1.00 (1H)", "1.00", "1H", null, null, 0);
            TestList("1.00 (1H, s)", "1.00", "1H", "s", null, 0);
            TestList("1.00 (1H, d, J = 1.0)", "1.00", "1H", "d", "1.0", 0);
            TestList("1.00 (1H, d, J = 7.3, 1.0)", "1.00", "1H", "d", "7.3, 1.0", 0);
            TestList("1.00 (1H, d, J = 7.3, 1.0 Hz)", "1.00", "1H", "d", "7.3, 1.0", 0);
            TestList("1.00 (1H, comment)", "1.00", "1H", null, null, 7);
            TestList("1.00 (1H, s, comment)", "1.00", "1H", "s", null, 7);
            TestList("1.00 (1H, d, J = 1.0, comment)", "1.00", "1H", "d", "1.0", 7);
            TestList("1.00 (1H, d, J = 7.3, 1.0, comment)", "1.00", "1H", "d", "7.3, 1.0", 7);
        }

        [TestMethod()]
        public void MakeRCommandTest2()
        {
            TestList("1.00 (s, 1H)", "1.00", "1H", "s", null, 0);
            TestList("1.00 (d, J = 1.0, 1H)", "1.00", "1H", "d", "1.0", 0);
            TestList("1.00 (d, J = 7.3, 1.0, 1H)", "1.00", "1H", "d", "7.3, 1.0", 0);
            TestList("1.00 (d, J = 7.3, 1.0 Hz, 1H)", "1.00", "1H", "d", "7.3, 1.0", 0);
            TestList("1.00 (s, 1H, comment)", "1.00", "1H", "s", null, 7);
            TestList("1.00 (d, J = 1.0, 1H, comment)", "1.00", "1H", "d", "1.0", 7);
            TestList("1.00 (d, J = 7.3, 1.0, 1H, comment)", "1.00", "1H", "d", "7.3, 1.0", 7);
        }

        [TestMethod()]
        public void MakeCommandsTest()
        {
            var o = new NMRSpectrumQuery();
            var cmds = o.MakeCommand("1.45 (s, 1H), 1.55 (d, J = 1.3 Hz, 3H), 1.95 (dd, J = 7.3, 2.3 Hz, 1.4H, CH3), 2.2-3.2 (m, 15H)");
            Assert.AreNotEqual(0, cmds.Count());
        }
    }
}
