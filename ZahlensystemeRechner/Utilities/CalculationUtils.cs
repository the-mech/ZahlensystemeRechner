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
            while (operators.Peek().Type != OperatorType.OpeningBracket)
            {
                PopCalculateAndPushOperand(operands, operators);
            }
            //lösche die Klammer auf vom stack
            operators.Pop();
        }

    }
}
