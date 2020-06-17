using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    /// <summary>
    /// Enthält alle Typen von unterstützten Operatoren
    /// </summary>
    public enum OperatorType
    {
        OpeningBracket,
        ClosingBracket,
        Multiply,
        Divide,
        Add,
        Subtract
    }

    /// <summary>
    /// Enthält mögliche Arten mathematischer Prioritäten
    /// Die Ignore Precedence ist für Klammerrechnung notwendig
    /// Eine Precedence von High drückt aus, dass dieser Operator gegenüber Operatoren
    /// von einer Precedence von Low vorrangig evaluiert werden sollen.
    /// </summary>
    public enum OperatorPrecedence
    {
        Ignore,
        Low,
        High
    }

    /// <summary>
    /// Ein Operator besteht aus einem Typen und einer mathematischen Priorität
    /// </summary>
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
