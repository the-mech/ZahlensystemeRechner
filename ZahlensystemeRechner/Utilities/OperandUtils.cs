using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZahlensystemeRechner
{
    public class OperandUtils
    {
        
        public static Operand CreateFromBinaryInput(string input, bool isNegative)
        {
            //überprüfe, dass der input string nur aus für die zahlenbasis gültigen zeichen besteht
            long value = 0;
            if (Regex.IsMatch(input, "^[10]+$"))
            {
                //wandle den string in eine dezimaldarstellung um
                long b = 1;
                for (int i = input.Length-1; i >= 0; i--)
                {
                    if (input[i] == '1')
                    {
                        value += b;
                    } 
                    b = b*2;
                }
            }
            else
            {
                throw new ArgumentException("Die binäre Eingabe" + input + "enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        public static Operand CreateFromDecimalInput(string input, bool isNegative)
        {
            long value = 0;
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                long b = 1;
                for(int i = input.Length - 1; i >= 0; i--)
                {
                    int digit = input[i] - '0';

                    value += digit * b;
                    b = b * 10;
                }
            }
            else
            {
                throw new ArgumentException("Die dezimale Eingabe"+input+"enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        public static Operand CreateFromHexadecimalInput(string input, bool isNegative)
        {
            long value = 0;

            if (Regex.IsMatch(input, "^[0-9A-Fa-f]+$"))
            {
                long b = 1;
                for(int i = input.Length-1; i >= 0; i--)
                {
                    long digit = input[i];

                    //unterscheide ob das zeichen bei 0-9 a-f oder A-F liegt

                    if(digit>='0' && digit <= '9')
                    {
                        digit = digit - '0';
                    }
                    else
                    if(digit>='a'&&digit <= 'f')
                    {
                        digit = digit - 'a';
                        //jetzt noch 10 draufrechnen, da a ja den dezimalen wert 10 hat
                        digit = digit + 10;
                    }
                    else
                    if(digit>='A'&& digit <='F')
                    {
                        //durch das subtrahieren von 'A' wird zb 'B' zu 1 und 'A' zu 0
                        digit = digit - 'A';
                        //jetzt noch 10 draufrechnen, da a ja den dezimalen wert 10 hat
                        digit = digit + 10;
                    }

                    value += digit * b;
                    b = b * 16;
                }
            }
            else
            {
                throw new ArgumentException("Die hexadezimale Eingabe" + input + "enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        public static Operand CreateFromOctalInput(string input, bool isNegative)
        {
            long value = 0;
            if (Regex.IsMatch(input, "^[0-7]+$"))
            {
                long b = 1;
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    int digit = input[i] - '0';

                    value += digit * b;
                    b = b * 8;
                }
            }
            else
            {
                throw new ArgumentException("Die oktale Eingabe" + input + "enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

    }
}
