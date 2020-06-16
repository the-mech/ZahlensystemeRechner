using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZahlensystemeRechner;
using System;
using System.Collections.Generic;
using System.Text;
namespace ZahlensystemeRechner.Tests
{
    [TestClass()]
    public class OperandUtilsTests
    {
        [TestMethod()]
        public void CreateFromBinaryInputTest()
        {
            string input = "10101010";
        
            Operand op = OperandUtils.CreateFromBinaryInput(input, false);

            Assert.AreEqual(Convert.ToInt32(input,2), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromBinaryInputZeroTest()
        {
            string input = "0";
            Operand op = OperandUtils.CreateFromBinaryInput(input, false);

            Assert.AreEqual(Convert.ToInt32(input, 2), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromBinaryInputNegativeTest()
        {
            string input = "10101010";

            Operand op = OperandUtils.CreateFromBinaryInput(input, true);

            Assert.AreEqual(-Convert.ToInt32(input, 2), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromBinaryInputIllegalCharactersThrowsTest()
        {
            string input = "A0101";
            Assert.ThrowsException<ArgumentException>(() =>OperandUtils.CreateFromBinaryInput(input, false));
        }

        [TestMethod()]
        public void CreateFromBinaryInputEmptyStringThrowsTest()
        {
            string input = "";
            Assert.ThrowsException<ArgumentException>(() => OperandUtils.CreateFromBinaryInput(input, false));
        }



        [TestMethod()]
        public void CreateFromDecimalInputTest()
        {
            string input = "42";

            Operand op = OperandUtils.CreateFromDecimalInput(input, false);

            Assert.AreEqual(Convert.ToInt32(input), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromDecimalInputZeroTest()
        {
            string input = "0";
            Operand op = OperandUtils.CreateFromDecimalInput(input, false);

            Assert.AreEqual(Convert.ToInt32(input), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromDecimalInputNegativeTest()
        {
            string input = "42";

            Operand op = OperandUtils.CreateFromDecimalInput(input, true);

            Assert.AreEqual(-Convert.ToInt32(input), op.decimalValue);
        }

        [TestMethod()]
        public void CreateFromDecimalInputIllegalCharactersThrowsTest()
        {
            string input = "A0101";
            Assert.ThrowsException<ArgumentException>(() => OperandUtils.CreateFromDecimalInput(input, false));
        }

        [TestMethod()]
        public void CreateFromDecimalInputEmptyStringThrowsTest()
        {
            string input = "";
            Assert.ThrowsException<ArgumentException>(() => OperandUtils.CreateFromDecimalInput(input, false));
        }
    }
}