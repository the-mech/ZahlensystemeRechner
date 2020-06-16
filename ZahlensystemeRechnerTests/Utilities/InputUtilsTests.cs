using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZahlensystemeRechner.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZahlensystemeRechner.Utilities.Tests
{
    [TestClass()]
    public class InputUtilsTests
    {
        [TestMethod()]
        public void ExtractOperandFromInputTest()
        {
            string input = "b_101010";

            Operand op = InputUtils.ExtractOperandFromInput(input);

            Assert.AreEqual(42, op.decimalValue);
        }

        [TestMethod]
        public void ExtractOperandFromNegativeInputTest()
        {
            string input = "-h_AFFE";

            Operand op = InputUtils.ExtractOperandFromInput(input);

            Assert.AreEqual(-45054, op.decimalValue);
        }

        [TestMethod]
        public void ExtractOperandFromInvalidPrefixTest()
        {
            string input = "hh_AFFE";

            Assert.ThrowsException<ArgumentException>(() => InputUtils.ExtractOperandFromInput(input));
        }

        [TestMethod]
        public void ExtractOperandFromInvalidInput()
        {
            string input = "kekew";

            Assert.ThrowsException<ArgumentException>(() => InputUtils.ExtractOperandFromInput(input));
        }


        [TestMethod]
        public void ExtractOperandFromDoublePrefixInput()
        {
            string input = "h_b_FFFW";


            Assert.ThrowsException<ArgumentException>(() => InputUtils.ExtractOperandFromInput(input));
        }

        [TestMethod]
        public void ExtractOperandFromInputOnlyMinusSign()
        {
            string input = "-";


            Assert.ThrowsException<ArgumentException>(() => InputUtils.ExtractOperandFromInput(input));
        }

        [TestMethod()]
        public void CreateTokensFromInputTest()
        {
            string input = "2+4-4";

            string[] result = InputUtils.CreateTokensFromInput(input);
            Console.WriteLine(String.Join(' ', result));
            CollectionAssert.AreEquivalent(new string[] { "2", "+", "4", "+", "-4" }, result);
        }

        [TestMethod()]
        public void CreateTokensWithPrefixFromInputTest()
        {
            string input = "-(d_2+o_4-h_Affe)";

            string[] result = InputUtils.CreateTokensFromInput(input);
            Console.WriteLine(String.Join(' ', result));
            CollectionAssert.AreEquivalent(new string[] { "-1","*", "(","d_2","+", "o_4", "-", "h_Affe",")" }, result);
        }
    }
}