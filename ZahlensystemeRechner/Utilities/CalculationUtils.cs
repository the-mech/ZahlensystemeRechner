using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    class CalculationUtils
    {

        /// <summary>
        /// Diese Funktion bekommt zwei Operanden und einen Operator übergeben
        /// und entscheidet anhand des Operators wie die beiden Operanden verrechnet werden sollen
        /// Sollte ein nicht unterstützter Operand übergeben werden (Klammer auf, Klammer zu) wirft diese Funktion eine
        /// Exception.
        /// </summary>
        /// <param name="left"> Linker Operand</param>
        /// <param name="right"> Rechter Operand</param>
        /// <param name="op"> Operator der die Operanden verrechnet</param>
        /// <returns>Gibt das Ergebnis der Rechnung als Operand zurück</returns>
        public static Operand Calculate(Operand left, Operand right, Operator op)
        {
            switch (op.Type)
            {
                case OperatorType.Add:
                    return new Operand(left.DecimalValue + right.DecimalValue);
                case OperatorType.Subtract:
                    return new Operand(left.DecimalValue - right.DecimalValue);
                case OperatorType.Multiply:
                    return new Operand(left.DecimalValue * right.DecimalValue);
                case OperatorType.Divide:
                    return new Operand(left.DecimalValue / right.DecimalValue);

                default:
                    throw new ArgumentException("Unsupported Operator in calculation");
            }
        }

        /// <summary>
        /// Diese Funktion bekommt zwei Stacks übergeben. Einen mit Operanden und einen mit Operatoren.
        /// Sie Popt zwei operanden und einen operator vom jeweiligen Stack. Berechnet das Ergebnis der Rechnung
        /// und Pusht das Ergebnis als Operand auf den Operandenstack
        /// ACHTUNG! Die Pop Methode wirft einen fehler falls versucht wird von einem leeren Stack zu pop'en
        /// ACHTUNG! dies geschieht nur wenn der Term Syntax Fehler enthält
        /// </summary>
        /// <param name="operands"> Stack mit Operanden</param>
        /// <param name="operators"> Stack mit Operatoren</param>
        public static void PopCalculateAndPushOperand(Stack<Operand> operands, Stack<Operator> operators)
        {
            try
            {
                Operator op = operators.Pop();
                Operand right = operands.Pop();
                Operand left = operands.Pop();
                operands.Push(Calculate(left, right, op));
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Syntax Fehler");
            }


        }

        /// <summary>
        /// Diese Funktion berechnet einen Subterm der von zwei Klammern umschlossen wird,
        /// aufgerufen wird diese, wenn eine schließende Klammer vom stack genommen wird.
        /// Die Funktion berechnet Teilausdrücke im Term solange, bis diese auf eine öffnende Klammer stößt und somit
        /// auf das Ende des Subterms. Die Funktion nimmt an, dass Punkt vor Strich rechnung im Vorfeld behandelt wurde.
        /// </summary>
        /// <param name="operands">Stack mit Operanden</param>
        /// <param name="operators">Stack mit Operatoren</param>
        public static void CalculateInsideBrackets(Stack<Operand> operands, Stack<Operator> operators)
        {
            try
            {
                while (operators.Peek().Type != OperatorType.OpeningBracket)
                {
                    PopCalculateAndPushOperand(operands, operators);
                }

            }
            //notwendig, da wenn keine öffnende klammer auf dem stack ist
            //die .Peek methode eine exception wirft. diese interpretieren wir als syntax fehler
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Syntax Fehler. Öffnende Klammer fehlt.");
            }
            //lösche die Klammer auf vom stack
            operators.Pop();
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
