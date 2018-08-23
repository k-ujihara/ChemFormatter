using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChemFormatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemFormatter.Tests
{
    [TestClass()]
    public class ChemFormulaQueryTests
    {
        [TestMethod()]
        public void DontItalicR1()
        {
            Assert.IsFalse(ChemFormulaQuery.MakeCommand("R1").Any(n => n is ItalicCommand));
        }

        [TestMethod()]
        public void ElementIsNotItalic()
        {
            Assert.IsFalse(ChemFormulaQuery.MakeCommand("F-F").Any(n => n is ItalicCommand));
        }
    }
}