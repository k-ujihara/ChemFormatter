using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChemFormatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemFormatter
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void NormalizeTest()
        {
            char c;
            c = Utility.Normalize('０');
            Assert.AreEqual('0', c);
        }

        [TestMethod()]
        public void NormalizeStringTest()
        {
            string s;
            s = Utility.Normalize("０１２３４５６７８９，．");
            Assert.IsTrue(string.Equals("0123456789,.", s, StringComparison.Ordinal));
        }
    }
}
