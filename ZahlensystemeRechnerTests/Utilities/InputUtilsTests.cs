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
        public void CreateOperandFromInputTest()
        {
            string input = "b_101010";
            InputToken tok = new InputToken(input, InputTokenType.Operand);
            Operand op = InputUtils.CreateOperandFromInputToken(tok);

            Assert.AreEqual(42, op.decimalValue);
        }

        [TestMethod]
        public void ExtractOperandFromNegativeInputTest()
        {
            string input = "-h_AFFE";
            InputToken tok = new InputToken(input, InputTokenType.Operand);
            Operand op = InputUtils.CreateOperandFromInputToken(tok);

            Assert.AreEqual(-45054, op.decimalValue);
        }

        [TestMethod]
        public void ExtractOperandFromInvalidPrefixTest()
        {
            string input = "hh_AFFE";
            InputToken tok = new InputToken(input, InputTokenType.Operand);
            Assert.ThrowsException<ArgumentException>(() => InputUtils.CreateOperandFromInputToken(tok));
        }

        [TestMethod]
        public void ExtractOperandFromInvalidInput()
        {
            string input = "kekew";
            InputToken tok = new InputToken(input, InputTokenType.Operand);
            Assert.ThrowsException<ArgumentException>(() => InputUtils.CreateOperandFromInputToken(tok));
        }


        [TestMethod]
        public void ExtractOperandFromDoublePrefixInput()
        {
            string input = "h_b_FFFW";
            InputToken tok = new InputToken(input, InputTokenType.Operand);

            Assert.ThrowsException<ArgumentException>(() => InputUtils.CreateOperandFromInputToken(tok));
        }

        [TestMethod]
        public void ExtractOperandFromInputOnlyMinusSign()
        {
            string input = "-";
            InputToken tok = new InputToken(input, InputTokenType.Operand);

            Assert.ThrowsException<ArgumentException>(() => InputUtils.CreateOperandFromInputToken(tok));
        }

        [TestMethod()]
        public void CreateTokensFromInputTest()
        {
            string input = "2+4-4";

            InputToken[] result = InputUtils.CreateTokensFromInput(input);
            CollectionAssert.AreEquivalent(new InputToken[] { 
                new InputToken("2",InputTokenType.Operand), 
                new InputToken("+", InputTokenType.Operator), 
                new InputToken("4", InputTokenType.Operand), 
                new InputToken("+", InputTokenType.Operator), 
                new InputToken("-4", InputTokenType.Operand) }, result);
        }

        [TestMethod()]
        public void CreateTokensWithPrefixFromInputTest()
        {
            string input = "-(d_2+o_4-h_Affe)";

            InputToken[] result = InputUtils.CreateTokensFromInput(input);
            CollectionAssert.AreEquivalent(new InputToken[] {
                new InputToken("-1",InputTokenType.Operand),
                new InputToken("*", InputTokenType.Operator),
                new InputToken("(", InputTokenType.Operator),
                new InputToken("d_2", InputTokenType.Operand),
                new InputToken("+", InputTokenType.Operator),
                new InputToken("o_4", InputTokenType.Operand),
                new InputToken("-", InputTokenType.Operator),
                new InputToken("h_Affe", InputTokenType.Operand),
                new InputToken(")", InputTokenType.Operator) }, result);
        }
    }
}