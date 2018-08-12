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
            var m = NMRSpectrumQuery.ReTabSpec.Match(text);
            Assert.IsTrue(m.Success);
            var info = NMRSpectrumQuery.ExtractPeakInfo(m);
            Assert.AreEqual(shift, info.Shift);
            Assert.AreEqual(integration, info.Integration);
            Assert.AreEqual(pattern, info.Pattern);
            if (couplings == null)
                Assert.IsNull(info.JValues);
            else
                Assert.AreEqual(couplings, string.Join(", ", info.JValues));
            Assert.AreEqual(commentLength, info.CommentRange == null ? 0 : info.CommentRange.Length);
        }

        [TestMethod()]
        public void MakeCommandTest()
        {
            TestLine("1.23\t1H\ts", "1.23", "1H", "s", null, 0);
            TestLine("1.23\t1H\tbr s", "1.23", "1H", "br s", null, 0);
            TestLine("1.23\t1H\td\t1.3", "1.23", "1H", "d", "1.3", 0);
            TestLine("1.23\t1H\tdd\t4.5, 1.3", "1.23", "1H", "dd", "4.5, 1.3", 0);
            TestLine("1.23\t1H\tdd\t4.5, 1.3\tCH", "1.23", "1H", "dd", "4.5, 1.3", 2);
            TestLine("1.23\t1H\ts\tCH", "1.23", "1H", "s", null, 2);
        }
    }
}