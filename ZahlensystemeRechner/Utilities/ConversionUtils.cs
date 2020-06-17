using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    public class ConversionUtils
    {
        public static string ConvertToBinary(Operand operand)
        {
            long value = operand.DecimalValue;
            StringBuilder builder = new StringBuilder();
            bool isNegative = false;

            if(value < 0)
            {
                isNegative = true;
                value = -value;
            }

            do
            {
                builder.Append(value % 2);
            } while ((value /= 2) > 0);

            if (isNegative)
            {
                builder.Append('-');
            }

            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }
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
                builder.Append(value % 8);
            } while ((value /= 8) > 0);

            if (isNegative)
            {
                builder.Append('-');
            }

            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }


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
                builder.Append(digitToCharMap[value % 16]);
            } while ((value /= 16) > 0);

            if (isNegative)
            {
                builder.Append('-');
            }

            char[] reversed = builder.ToString().ToCharArray();
            Array.Reverse(reversed);
            return new string(reversed);
        }
    }
}
