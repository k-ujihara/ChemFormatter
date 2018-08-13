using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class ListReaderTests
    {
        [TestMethod()]
        public void ReadLineTest()
        {
            Test("1,2", "1", "2");
            Test("1(c),2", "1(c)", "2");
            Test("1(c,d),2", "1(c,d)", "2");
            try
            {
                Test("1(c,d,2", "1(c,d", "2");
                Assert.Fail();
            }
            catch (Exception)
            { }
        }

        static void Test(string text, params string[] expectedLines)
        {
            var lines = new ListReader(text) { ThrowExceptionOnNoEol = true };
            Assert.IsTrue(expectedLines.SequenceEqual(lines.Select(n => n.Text)));
        }
    }
}
