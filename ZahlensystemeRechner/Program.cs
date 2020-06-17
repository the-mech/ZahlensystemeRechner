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
                Console.Clear();
                Console.Write("> ");
                //lese benutzereingabe ein
                string input = Console.ReadLine();
                try
                {
                    // Zerlege die Benutzereingabe in Tokens
                    InputToken[] tokens = InputUtils.CreateTokensFromInput(input);
                    // wenn die länge 1 entspricht gehen wir davon aus, dass der benutzer eine zahl eingegeben hat
                    // und versuchen diese zu konvertieren
                    if (tokens.Length == 1)
                    {
                        Operand toConvert = InputUtils.CreateOperandFromInputToken(tokens[0]);
                        ConvertNumber(toConvert);
                    }
                    else
                    {
                        //an sonsten sollte dies ein term sein(länge 0 ist kein problem)
                        //versuche den Term zu evaluieren
                        Operand result = CalculateTerm(tokens);
                        //gib das Ergebnis aus
                        ConsoleUtils.WriteResultLine("= "+result.DecimalValue.ToString());
                    }

                }
                //fange ungültige Operationen auf, diese entstehen wenn Leere stacks gepoppt werden
                //passiert wenn die eingabe syntax fehler enthielt
                catch (InvalidOperationException)
                {
                    ConsoleUtils.WriteErrorLine("Syntax Error");
                }
                //fange sonstige Exceptions auf und gib die nachricht auf der konsole aus
                catch (Exception e)
                {
                    ConsoleUtils.WriteErrorLine(e.Message);
                }

                Console.WriteLine("Erneut rechnen? (j/n)");

                //wiederhole solange der benutzer kein kleines n eintippt
            } while (Console.ReadKey().KeyChar != 'n');
        }

        /// <summary>
        /// Konvertiert den übergegenen Operanden ins binäre, oktale und hexadezimale Zahlensystem
        /// und gibt anschließend diesen in den 4 Darstellungen aus ( Binär, Dezimal, Oktal, Hexadezimal)
        /// </summary>
        /// <param name="number"> Operand, der konvertiert und ausgegeben werden soll</param>
        public static void ConvertNumber(Operand number)
        {
            Console.WriteLine("Bin: "+ ConversionUtils.ConvertToBinary(number));
            Console.WriteLine("Dez: " + number.DecimalValue);
            Console.WriteLine("Okt: " + ConversionUtils.ConvertToOctal(number));
            Console.WriteLine("Hex: "+ ConversionUtils.ConvertToHexadecimal(number));
        }
        /// <summary>
        /// Diese Funktion nimmt den vom User eingegebenen Term, welcher vorher zu einem Array von InputToken zerlegt wurde und berechnet
        /// diesen unter beachtung von Klammersetzung und Punkt-vor-Strich-Rechnung
        /// </summary>
        /// <param name="tokens">Array mit der Usereingabe die zu InputToken zerlegt wurde.</param>
        /// <returns></returns>
        public static Operand CalculateTerm(InputToken[] tokens)
        {
            //die Stacks halten operanden und operatoren für die auswertung des Terms
            Stack<Operand> operandStack = new Stack<Operand>();
            Stack<Operator> operatorStack = new Stack<Operator>();

            //iteriere über den term
            for (int i = 0; i < tokens.Length; i++)
            {
                InputToken currentToken = tokens[i];

                switch (currentToken.Type)
                {
                    //wenn der token ein operand ist, konvertiere ihn zu einem Operand und pushe ihn auf den Operanden Stack
                    case InputTokenType.Operand:
                        operandStack.Push(InputUtils.CreateOperandFromInputToken(currentToken));
                        break;
                    //wenn der token ein operator ist, konvertiere den InputToken zu einem Operator
                    case InputTokenType.Operator:
                        Operator op = InputUtils.CreateOperatorFromInputToken(currentToken);

                        //überprüfe ob der Operator eine schließende Klammer ist
                        if (op.Type == OperatorType.ClosingBracket)
                        {
                            //schließende klammer zeigt an, dass ein subterm vorher von einer öffnenden Klammer eingeleitet wurde
                            //berechne nun den subterm, da die Klammerrechnung Vorrang hat.
                            CalculationUtils.CalculateInsideBrackets(operandStack, operatorStack);
                        }
                        else
                        {
                            //Bei allen anderen Operatoren schau dir an was auf dem Operator Stack oben drauf liegt
                            Operator top;
                            if (operatorStack.TryPeek(out top))
                            {
                                //Ignoriere Klammern, diese haben als OperatorPrecedence den wert Ignore
                                if (op.Precedence != OperatorPrecedence.Ignore && top.Precedence != OperatorPrecedence.Ignore)
                                {
                                    // Vergleiche den aktuellen operator und den auf dem stack
                                    // hat der Operator auf dem stack Vorrang? * und / haben Vorrang vor + und - 
                                    if (top.Precedence > op.Precedence)
                                    {
                                        CalculationUtils.PopCalculateAndPushOperand(operandStack, operatorStack);
                                    }
                                }
                            }
                            operatorStack.Push(op);
                        }
                        break;
                }
            }
            // alle terme in klammern sind evaluiert, Operatoren und Operanden sind in der richtigen Reihenfolge auf dem Stack
            // Evaluiere nun die Stacks
            while (operatorStack.Count > 0)
            {
                // falls wir noch eine öffnende Klammer auf dem Stack haben, bedeutet dies das es keine schließende klammer in der eingabe gab
                // somit ist dies ein Syntax Fehler
                if (operatorStack.Peek().Type == OperatorType.OpeningBracket)
                {
                    throw new ArgumentException("Syntax Fehler: Schließende Klammer fehlt.");
                }
                //rufe die auswertungsfunktion auf
                CalculationUtils.PopCalculateAndPushOperand(operandStack, operatorStack);
            }
            //nach dem evaluieren der stacks befindet sich nur noch ein operand auf dem Operanden Stack
            //dieser entspricht dem Ergebnis des Terms
            //gib dieses zurück
            return operandStack.Pop();

        }


    }
}
