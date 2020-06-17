using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZahlensystemeRechner
{
    public class OperandUtils
    {
        /// <summary>
        /// Die Funktion erstellt aus einer binären Eingabe einen Operanden.
        /// Der Algorithmus nutzt das Hornerschema
        /// </summary>
        /// <param name="input">string der die dezimale Eingabe enthält</param>
        /// <param name="isNegative">Enthält das Vorzeichen</param>
        /// <returns>Die Eingabe als Operand in Dezimaldarstellung</returns>
        public static Operand CreateFromBinaryInput(string input, bool isNegative)
        {
            long value = 0;

            //überprüfe, dass der input string nur aus für die zahlenbasis gültigen zeichen besteht
            // der Reguläre Ausdruck stimmt mit allen inputs überein
            // die nur aus 1en und 0en bestehen und mindestens ein zeichen Enthalten
            if (Regex.IsMatch(input, "^[10]+$"))
            {
                //wandle den string in eine dezimaldarstellung um
                // b steht für basis am anfang 2^0 = 1
                long b = 1;
                //iteriere von hinten über den string
                for (int i = input.Length-1; i >= 0; i--)
                {
                    //wenn die eingabe eine 1 is rechnen wir 1*2^0+ 1*2^1+ ... usw
                    if (input[i] == '1')
                    {
                        //addiere die basis
                        value += b;
                    }
                    //verdopple die basis 2^0 -> 2^1 ->2^2
                    b = b*2; 
                }
            }
            else
            {
                //eingabe ungültig
                throw new ArgumentException("Die binäre Eingabe " + input + " enthält ungültige Zeichen.");
            }
            //wenn die eingabe negativ ist müssen wir den dezimalwert negieren
            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        /// <summary>
        /// Die Funktion erstellt aus einer dezimalen Eingabe einen Operanden.
        /// Der Algorithmus nutzt das Hornerschema.
        /// Genauere Dokumentation siehe CreateFromBinaryInput
        /// </summary>
        /// <param name="input">string der die dezimale Eingabe enthält</param>
        /// <param name="isNegative">Enthält das Vorzeichen</param>
        /// <returns>Die Eingabe als Operand in Dezimaldarstellung</returns>
        public static Operand CreateFromDecimalInput(string input, bool isNegative)
        {
            long value = 0;
            // Dieser reguläre ausdruck entspricht allen kombinationen von zeichen von 0-9 mit mindestens einem zeichen
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                long b = 1;
                for(int i = input.Length - 1; i >= 0; i--)
                {
                    //konvertiere die ascii zeichen '0'-'9' zu den zahlenwerten 0-9
                    int digit = input[i] - '0';

                    value += digit * b;
                    b = b * 10;
                }
            }
            else
            {
                throw new ArgumentException("Die dezimale Eingabe "+input+" enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        /// <summary>
        /// Die Funktion erstellt aus einer hexadezimalen Eingabe einen Operanden.
        /// Der Algorithmus nutzt das Hornerschema.
        /// Genauere Dokumentation siehe CreateFromBinaryInput
        /// </summary>
        /// <param name="input">string der die dezimale Eingabe enthält</param>
        /// <param name="isNegative">Enthält das Vorzeichen</param>
        /// <returns>Die Eingabe als Operand in Dezimaldarstellung</returns>
        public static Operand CreateFromHexadecimalInput(string input, bool isNegative)
        {
            long value = 0;
            //dieser reguläre ausdruck entspricht allen kombinationen der zeichen 0-9 a-f und A-F mit mindestens einem zeichen
            if (Regex.IsMatch(input, "^[0-9A-Fa-f]+$"))
            {
                long b = 1;
                for(int i = input.Length-1; i >= 0; i--)
                {
                    long digit = input[i];

                    //unterscheide ob das zeichen bei 0-9 a-f oder A-F liegt

                    if(digit>='0' && digit <= '9')
                    {
                        //konvertiere wie gehabt ascii zeichen zu zahlen
                        digit = digit - '0';
                    }
                    else
                    if(digit >= 'a' && digit <= 'f')
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
                    b = b * 16; // basis 16
                }
            }
            else
            {
                throw new ArgumentException("Die hexadezimale Eingabe " + input + " enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

        /// <summary>
        /// Die Funktion erstellt aus einer oktalen Eingabe einen Operanden.
        /// Der Algorithmus nutzt das Hornerschema.
        /// Genauere Dokumentation siehe CreateFromBinaryInput
        /// </summary>
        /// <param name="input">string der die dezimale Eingabe enthält</param>
        /// <param name="isNegative">Enthält das Vorzeichen</param>
        /// <returns>Die Eingabe als Operand in Dezimaldarstellung</returns>
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
                    b = b * 8; // basis 8
                }
            }
            else
            {
                throw new ArgumentException("Die oktale Eingabe " + input + " enthält ungültige Zeichen.");
            }

            if (isNegative)
            {
                value = -value;
            }
            return new Operand(value);
        }

    }
}
