using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    class ConsoleUtils
    {

        public static void WriteError(string err)
        {
            WriteColoredLine(err, ConsoleColor.Red);
        }

        public static void WriteResult(string result)
        {
            WriteColoredLine(result, ConsoleColor.Green);
        }


        static void WriteColoredLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
