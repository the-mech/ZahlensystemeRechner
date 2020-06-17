using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public enum InputTokenType
    {
        Operator,
        Operand
    };
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
