using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public class InputToken
    {
        public enum TokenType
        {
            Operator,
            Operand
        };

        private TokenType Type { get; }

        private string Token { get; }

        public InputToken(string token, TokenType type)
        {
            this.Token = token;
            Type = type;
        }
    }
}
