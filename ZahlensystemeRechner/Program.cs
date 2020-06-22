using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using ZahlensystemeRechner.Utilities;
namespace ZahlensystemeRechner
{
    class Program
    {
        static void Main(string[] args)
        {
            ProtocolUtils.AddToProtocol("Herzlich Willkommen bei dem Zahlensysteme-Rechner!", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("Um ihre Berechnung zu starten geben sie bitte einen Term ein.", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("Die Grundrechenarten + - * / und Klammersetzung werden unterstüzt.", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("Bei Eingabe einer Zahl wird diese in alle unterstützten Zahlensysteme umgewandelt.", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("Der Taschenrechner unterstützt folgende Zahlensysteme, um diese zu verwenden verwenden sie folgende Präfixe:", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("binär: b_(zahl),", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("oktal: o_(zahl),", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("dezimal: d_(zahl),", ProtocolMessageType.Info);
            ProtocolUtils.AddToProtocol("hexadezimal: h_(zahl)", ProtocolMessageType.Info);
            do
            {
                ConsoleUtils.BeginWindow();
                ProtocolUtils.RefreshWindowFromProtocol();
                ConsoleUtils.BeginLine();
                ConsoleUtils.WriteLineContent("> ");
                //save coordinates
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;
                ConsoleUtils.EndLine();
                ConsoleUtils.EndWindow();
                Console.SetCursorPosition(cursorX, cursorY);


                //lese benutzereingabe ein
                string input = Console.ReadLine();

                string displayInput = "> " + input;
                ProtocolUtils.AddToProtocol(displayInput, ProtocolMessageType.Info);
                try
                {
                    // Zerlege die Benutzereingabe in Tokens
                    InputToken[] tokens = InputUtils.CreateTokensFromInput(input);

                    //an sonsten sollte dies ein term sein(länge 0 ist kein problem)
                    //versuche den Term zu evaluieren
                    Operand result = CalculationUtils.CalculateTerm(tokens);
                    //gib das Ergebnis aus
                    ConversionUtils.ConvertNumber(result);


                }
                //fange ungültige Operationen auf, diese entstehen wenn Leere stacks gepoppt werden
                //passiert wenn die eingabe syntax fehler enthielt
                catch (InvalidOperationException)
                {
                    ProtocolUtils.AddToProtocol("Syntax Fehler", ProtocolMessageType.Error);
                }// Durch null teilen 
                catch (DivideByZeroException)
                {
                    ProtocolUtils.AddToProtocol("Durch 0 teilen nicht möglich.", ProtocolMessageType.Error);
                }
                //fange sonstige Exceptions auf und gib die nachricht auf der konsole aus
                catch (Exception e)
                {
                    ProtocolUtils.AddToProtocol(e.Message, ProtocolMessageType.Error);
                }
                



                ProtocolUtils.AddToProtocol("Möchten Sie noch eine Berechnung durchführen? (j/n)", ProtocolMessageType.Info);

                ConsoleUtils.BeginWindow();
                ProtocolUtils.RefreshWindowFromProtocol();
                ConsoleUtils.EndWindow();
                //wiederhole solange der benutzer kein kleines n eintippt
            } while (Console.ReadKey().KeyChar != 'n');

            Console.Clear();

            string filename = "rechenprotokoll.txt";
            ProtocolUtils.WriteProtocolToFile(filename);
            Console.WriteLine("Protokoll gespeichert. Dateiname: "+ filename);
        }




    }
}
