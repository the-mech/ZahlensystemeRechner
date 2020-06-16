using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public class Operand
    {
        public long decimalValue {get; set;}
        
        public Operand(long decimalValue)
        {
            this.decimalValue = decimalValue;
        }

    }
}
