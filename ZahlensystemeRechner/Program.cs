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
                    // wenn die länge 1 entspricht gehen wir davon aus, dass der benutzer eine zahl eingegeben hat
                    // und versuchen diese zu konvertieren
                    if (tokens.Length == 1)
                    {
                        Operand toConvert = InputUtils.CreateOperandFromInputToken(tokens[0]);
                        ConversionUtils.ConvertNumber(toConvert);
                    }
                    else
                    {
                        //an sonsten sollte dies ein term sein(länge 0 ist kein problem)
                        //versuche den Term zu evaluieren
                        Operand result = CalculationUtils.CalculateTerm(tokens);
                        //gib das Ergebnis aus
                        ProtocolUtils.AddToProtocol("= " + result.DecimalValue.ToString(), ProtocolMessageType.Result);
                    }

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
                



                ProtocolUtils.AddToProtocol("Erneut rechnen? (j/n)",ProtocolMessageType.Info);

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
