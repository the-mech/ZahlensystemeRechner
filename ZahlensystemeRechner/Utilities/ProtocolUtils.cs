using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    /// <summary>
    /// Stellt Funktionen bereit um Nachrichten zu protokollieren und auf Festplatte zu schreiben.
    /// </summary>
    class ProtocolUtils
    {
        /// <summary>
        /// Liste die die gesamten Protokollnachrichten enthält
        /// </summary>
        static List<ProtocolMessage> protocol = new List<ProtocolMessage>();


        /// <summary>
        /// Fügt eine neue Nachricht der Protokollliste hinzu
        /// </summary>
        /// <param name="message"> Nachricht die hinzugefügt werden soll</param>
        /// <param name="type"> Nachrichtentyp</param>
        public static void AddToProtocol(string message, ProtocolMessageType type)
        {
            protocol.Add(new ProtocolMessage(message, type));
        }

        /// <summary>
        /// Löscht das Protokoll
        /// </summary>
        public static void ClearProtocol()
        {
            protocol.Clear();
        }

        /// <summary>
        /// Schreibt den aktuellen Inhalt der Protokollliste in eine Textdatei auf der Festplatte
        /// </summary>
        /// <param name="filename">Dateiname für die Textdatei</param>
        public static void WriteProtocolToFile(string filename)
        {
            //erstelle eine neue datei mit dem dateinamen filename
            StreamWriter file = new StreamWriter(filename);
            //iteriere über alle nachrichten in der liste
            foreach(ProtocolMessage msg in protocol)
            {
                //schreibe den nachrichtentext in die datei
                file.WriteLine(msg.Message);
            }
            //schließe die datei zum schluss
            file.Close();
        }

        /// <summary>
        /// Diese Funktion schreibt den gesamten Inhalt des Protokolls auf die Konsole
        /// unter Beachtung von drei Nachrichtentypen. Infonachrichten werden weiss ausgegeben,
        /// Fehlernachrichten Rot und Ergebnisnachrichten Grün
        /// </summary>
        public static void RefreshWindowFromProtocol()
        {
            foreach(ProtocolMessage msg in protocol)
            {
                switch (msg.MessageType){
                    case ProtocolMessageType.Info:
                        ConsoleUtils.WriteLine(msg.Message);
                        break;
                    case ProtocolMessageType.Error:
                        ConsoleUtils.WriteErrorLine(msg.Message);
                        break;
                    case ProtocolMessageType.Result:
                        ConsoleUtils.WriteResultLine(msg.Message);
                        break;

                }
            }
        }
    }
}
