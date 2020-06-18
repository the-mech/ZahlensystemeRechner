using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    public class ConversionUtils
    {
        /// <summary>
        /// Die Funktion Konvertiert einen Operanden zur Ausgabe ins Binäre Zahlensystem
        /// </summary>
        /// <param name="operand"> Operand welcher konvertiert werden soll</param>
        /// <returns>string welcher die binäre Repräsentation des Operanden enthält</returns>
        public static string ConvertToBinary(Operand operand)
        {
            long value = operand.DecimalValue;
            StringBuilder builder = new StringBuilder();
            bool isNegative = false;

            //beachte,dass der algorithmus keine negativen zahlen umwandeln kann
            //falls die zahl negativ ist negiere sie vorher und merke dir dass sie negativ war
            if(value < 0)
            {
                isNegative = true;
                value = -value;
            }

            //klassischer algorithmus zum umwandeln von dezimal nach binär
            do
            {
                // Rest der division durch 2 speichern
                builder.Append(value % 2);
                // wert durch 2 teilen
                value = value / 2;
            } while ( value > 0);

            //füge ein - hinzu falls die zahl negativ war
            if (isNegative)
            {
                builder.Append('-');
            }
            //die string repräsentation der zahl muss noch umgekehrt werden
            //dies geht am einfachsten mit Array.Reverse
            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }
        /// <summary>
        /// Die Funktion Konvertiert einen Operanden zur Ausgabe ins oktale Zahlensystem
        /// Für genauere Dokumentation des Algorithmus siehe ConvertToBinary
        /// </summary>
        /// <param name="operand"> Operand welcher konvertiert werden soll</param>
        /// <returns>string welcher die oktale Repräsentation des Operanden enthält</returns>
        public static string ConvertToOctal(Operand operand)
        {
            long value = operand.DecimalValue;
            StringBuilder builder = new StringBuilder();
            bool isNegative = false;

            if (value < 0)
            {
                isNegative = true;
                value = -value;
            }

            do
            {
                //wie beim binären, nur mit einer 8 statt mit einer 2 oder allgemein gesagt durch die Zahlenbasis des zielsystems
                builder.Append(value % 8);
                value = value / 8;
            } while (value > 0);

            if (isNegative)
            {
                builder.Append('-');
            }

            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }

        /// <summary>
        /// Die Funktion Konvertiert einen Operanden zur Ausgabe ins hexadezimale Zahlensystem
        /// Für genauere Dokumentation des Algorithmus siehe ConvertToBinary
        /// </summary>
        /// <param name="operand"> Operand welcher konvertiert werden soll</param>
        /// <returns>string welcher die hexadezimale Repräsentation des Operanden enthält</returns>
        public static string ConvertToHexadecimal(Operand operand)
        {
            char[] digitToCharMap = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            long value = operand.DecimalValue;
            StringBuilder builder = new StringBuilder();
            bool isNegative = false;

            if (value < 0)
            {
                isNegative = true;
                value = -value;
            }

            do
            {
                // wie beim binären nur dividieren wir durch 16 oder allgemein gesagt durch die Zahlenbasis des zielsystems
                // hier müssen wir jedoch aufpassen, da die werte zw 0 und 15 liegen und ab 10 die werte mit buchstaben kodiert werden
                // das array digitToCharMap sorgt für eine einfache möglichkeit der konvertierung von zahlen ->zeichen
                // im array sind die Zeichendarstellungen gespeichert, durch interpretieren des ergebnisses der restwertrechnung
                // als index im array kann somit einfach konvertiert werden
                builder.Append(digitToCharMap[value % 16]);
                value = value / 16;
            } while (value > 0);

            if (isNegative)
            {
                builder.Append('-');
            }

            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }

        /// <summary>
        /// Konvertiert den übergegenen Operanden ins binäre, oktale und hexadezimale Zahlensystem
        /// und gibt anschließend diesen in den 4 Darstellungen aus ( Binär, Dezimal, Oktal, Hexadezimal)
        /// </summary>
        /// <param name="number"> Operand, der konvertiert und ausgegeben werden soll</param>
        public static void ConvertNumber(Operand number)
        {
            ProtocolUtils.AddToProtocol("Bin: " + ConversionUtils.ConvertToBinary(number), ProtocolMessageType.Result);
            ProtocolUtils.AddToProtocol("Dez: " + number.DecimalValue, ProtocolMessageType.Result);
            ProtocolUtils.AddToProtocol("Okt: " + ConversionUtils.ConvertToOctal(number), ProtocolMessageType.Result);
            ProtocolUtils.AddToProtocol("Hex: " + ConversionUtils.ConvertToHexadecimal(number), ProtocolMessageType.Result);
        }

    }


}
