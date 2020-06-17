using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public class Operand
    {
        public long DecimalValue {get; set;}
        
        public Operand(long decimalValue)
        {
            this.DecimalValue = decimalValue;
        }

    }
}
