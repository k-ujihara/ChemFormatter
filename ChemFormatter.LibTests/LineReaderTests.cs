using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class LineReaderTests
    {
        [TestMethod()]
        public void ReadLineTest()
        {
            Test("line 1\r\nline 2\r\n", "line 1", "line 2");
            Test("line 1\nline 2\n", "line 1", "line 2");
            Test("line 1\rline 2\r", "line 1", "line 2");
            try
            {
                Test("line 1\rline 2", "line 1", "line 2");
                Assert.Fail();
            }
            catch (Exception)
            { }
        }

        static void Test(string text, params string[] expectedLines)
        {
            var lines = new LineReader(text) { ThrowExceptionOnNoEol = true };
            Assert.IsTrue(expectedLines.SequenceEqual(lines.Select(n => n.Text)));
        }
    }
}
