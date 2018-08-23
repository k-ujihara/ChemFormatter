using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class ChemNameQueryTests
    {
        [TestMethod()]
        public void MakeCommandTest()
        {
            Assert.AreEqual(0, ChemNameQuery.MakeCommand("(II)").Count());
        }
    }
}
