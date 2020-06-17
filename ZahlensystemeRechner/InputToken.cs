using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    /// <summary>
    /// Wir haben zwei Arten von InputToken
    /// Operanden und Operatoren
    /// Die Typen vereinfachen das identifizieren von Operanden und Operatoren
    /// </summary>
    public enum InputTokenType
    {
        Operator,
        Operand
    };

    /// <summary>
    /// Ein InputToken entspricht einem Teil der Benutzereingabe der sinngemäß von anderen getrennt ist.
    /// Jedes Rechenzeichen und jede Zahl inkl Präfix entspricht einem Token
    /// </summary>
    public class InputToken
    {
        public InputTokenType Type { get; }

        public string Token { get; }

        public InputToken(string token, InputTokenType type)
        {
            this.Token = token;
            Type = type;
        }

        public override string ToString()
        {
            return Token;
        }
    }
}
