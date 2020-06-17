using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    /// <summary>
    /// Stellt einen Operanden dar
    /// Ein Operand besteht aus einem dezimalen, ganzzahligen Wert
    /// </summary>
    public class Operand
    {
        public long DecimalValue {get; set;}
        
        public Operand(long decimalValue)
        {
            this.DecimalValue = decimalValue;
        }

    }
}
