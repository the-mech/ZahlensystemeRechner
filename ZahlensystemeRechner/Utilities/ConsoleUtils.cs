using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    /// <summary>
    /// Diese Klasse enthält Funktionen für die Darstellung eines "Fensters" in der Konsole
    /// </summary>
    class ConsoleUtils
    {
        /// <summary>
        /// Enthält die Anzahl der zeichen der aktuellen Zeile
        /// </summary>
        public static int outputWidth;

        /// <summary>
        /// Die Funktion leert die Konsole und zeichnet den oberen Teil des Fensterrahmens
        /// </summary>
        public static void BeginWindow()
        {
            Console.Clear();
            Console.WriteLine("╔═[Zahlensysteme-Rechner]".PadRight(Console.WindowWidth-1, '═')+ "╗");
        }

        /// <summary>
        /// Die Funktion beendet das zeichnen des Fensters, indem die Konsole nach unten hin mit leeren Zeilen mit seitlichem
        /// Rahmen aufgefüllt wird. Abschließend wird der untere Teil des Rahmens gezeichnet
        /// </summary>
        public static void EndWindow()
        {
            int currentHeight = Console.CursorTop+1;
            for(int i = currentHeight; i < Console.WindowHeight-1; i++)
            {
                //zeichne leere Zeilen ACHTUNG! das ist NICHT die Console.WriteLine Funktion!!
                WriteLine("");
            }

            Console.WriteLine("╚".PadRight(Console.WindowWidth - 1, '═') + "╝");
        }

        /// <summary>
        /// Die Funktion muss aufgerufen werden wenn eine neue Zeile mit Rahmenlinien auf die Konsole geschrieben werden soll.
        /// </summary>
        public static void BeginLine()
        {
            string lineStart = "║ ";
            //merke die anzahl an geschriebenen zeichen, damit wir später die zeile nach rechts hin auffüllen können [siehe EndLine()]
            outputWidth += lineStart.Length;
            Console.Write(lineStart);
        }

        /// <summary>
        /// Diese Funktion mss verwendet werden wenn man inhalte mit Rahmenlinien auf die Konsole schreiben möchte.
        /// Vorher sollte die Funktion BeginLine() aufgerufen werden, welche die linke Rahmenlinie erzeugt.
        /// </summary>
        /// <param name="line">Zeileninhalte die geschrieben werden sollen</param>
        public static void WriteLineContent(string line)
        {
            //merke dir wieder die anzahl an geschriebenen zeichen
            outputWidth += line.Length;

            Console.Write(line);
        }

        /// <summary>
        /// Wie WriteLineContent, nur mit der Möglichkeit, bunte Zeilen auszugeben
        /// </summary>
        /// <param name="line">Zeile die geschrieben werden soll</param>
        /// <param name="color">Farbe in der die Zeile geschrieben werden soll</param>
        public static void WriteLineContentColored(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteLineContent(line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Schließt die Zeile durch auffüllen mit leerzeichen und rechter Rahmenlinie
        /// </summary>
        public static void EndLine()
        {
            Console.Write("║\n".PadLeft(Console.WindowWidth - outputWidth+1));
            //neue zeile, deshalb die output width auf 0 setzen
            outputWidth = 0;
        }

        /// <summary>
        /// Hilfsfunktion um eine Zeile mit linker und rechter Rahmenlinie auszugeben
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLine(string line)
        {
            BeginLine();
            WriteLineContent(line);
            EndLine();
        }

        /// <summary>
        /// Hilfsfunktion um einen fehler auf die Konsole zu schreiben
        /// </summary>
        /// <param name="err">Fehlernachricht</param>
        public static void WriteErrorLine(string err)
        {
            //schreibt den Fehler in Roter farbe auf die konsole
            BeginLine();
            WriteLineContentColored(err,ConsoleColor.Red);
            EndLine();
        }

        /// <summary>
        /// Hilfsfunktion um ein Ergebnis auf die Konsole zu schreiben
        /// </summary>
        /// <param name="result"></param>
        public static void WriteResultLine(string result)
        {
            //schreibt den string in grüner Farbe auf die Konsole
            BeginLine();
            WriteLineContentColored(result, ConsoleColor.Green);
            EndLine();
        }

    }
}
