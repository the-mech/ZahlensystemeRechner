using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public enum OperatorType
    {
        OpeningBracket,
        ClosingBracket,
        Multiply,
        Divide,
        Add,
        Subtract
    }

    public enum OperatorPrecedence
    {
        Ignore,
        Low,
        High
    }
    public class Operator
    {
        public OperatorType Type { get; }
        public OperatorPrecedence Precedence { get; }

        public Operator(OperatorType type, OperatorPrecedence precedence)
        {
            Type = type;
            Precedence = precedence;
        }
    }
}
