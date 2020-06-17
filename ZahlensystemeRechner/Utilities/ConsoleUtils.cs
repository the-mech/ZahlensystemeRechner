using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    class ConsoleUtils
    {

        /// <summary>
        /// Hilfsmethode um einen fehler auf die Konsole zu schreiben
        /// </summary>
        /// <param name="err">Fehlernachricht</param>
        public static void WriteErrorLine(string err)
        {
            //schreibt den Fehler in Roter farbe auf die konsole
            WriteColoredLine(err, ConsoleColor.Red);
        }

        /// <summary>
        /// Hilfsmethode um ein Ergebnis auf die Konsole zu schreiben
        /// </summary>
        /// <param name="result"></param>
        public static void WriteResultLine(string result)
        {
            //schreibt den string in grüner Farbe auf die Konsole
            WriteColoredLine(result, ConsoleColor.Green);
        }

        /// <summary>
        /// Schreibt eine Zeile in einer wählbaren Farbe auf die Konsole
        /// </summary>
        /// <param name="message"> String der auf der Konsole ausgegeben werden soll</param>
        /// <param name="color">Farbe, in der der String ausgegeben werden soll</param>
        static void WriteColoredLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
