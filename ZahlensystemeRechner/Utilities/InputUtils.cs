using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    public class InputUtils
    {

        /// <summary>
        /// Diese Funktion erstellt aus einem InputToken einen Operanden
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Operand CreateOperandFromInputToken(InputToken token)
        {
            string input = token.Token;

            bool isNegative = false;

            //wenn das erste zeichen im token ein minus ist, haben wir eine negative zahl
            if (input[0] == '-')
            {
                isNegative = true;
                //merke dir dass die zahl negativ ist, entferne das minuszeichen, das braucht man nicht fürs konvertieren
                input = input.Remove(0, 1);
            }

            // teile den input string am _ , dies ist der separator der präfix und wert trennt
            // b_10101 -> b, 10101
            string[] splittedToken = input.Split('_');
            // haben wir nun ein array mit mehr als 3 teilen
            // hat der user irgendwas falsches eingegeben: zb. h_b_h_12
            // dies würde durch die split methode zu h , b , h, 12 und somit ungültig
            if (splittedToken.Length > 2)
            {
                throw new ArgumentException("Operand "+input+" ist ungültig.");
            }
            //eine länge von 2 bedeutet, dass unser token einen präfix und wert enthält
            else if (splittedToken.Length == 2)
            {
                //nach anforderung ist der erste part im string ist das prefix zweiter part ist der wert
                string prefix = splittedToken[0];
                string value = splittedToken[1];

                //wenn der präfix aus mehr als einem zeichen besteht, ist dieser fehlerhaft
                if (prefix.Length > 1)
                {
                    throw new ArgumentException("Präfix <"+prefix+"> ist zu lang.");
                }
                else
                {
                    //entscheide anhand des präfixes, welche konvertierungsfunktion verwendet werden soll
                    //und gib nach dem konvertieren einen operanden zurück
                    switch(prefix){
                        case "b":
                            return OperandUtils.CreateFromBinaryInput(value, isNegative);
                        case "o":
                            return OperandUtils.CreateFromOctalInput(value, isNegative);
                        case "d":
                            return OperandUtils.CreateFromDecimalInput(value, isNegative);
                        case "h":
                            return OperandUtils.CreateFromHexadecimalInput(value, isNegative);

                        default:
                            // sonstige Präfixe werden nicht unterstützt
                            throw new ArgumentException("Präfix <" + prefix + ">ist ungültig.");
                    }
                }
                
            }
            else // bei einer länge von 1 gehen wir davon aus, dass der input eine Dezimalzahl ist
            {
                // Achtung! Länge von 0 wäre hier auch möglich, gibt aber natürlich einen fehler
                // Eine solche fehlerhafte eingabe wird in der unteren funktion beachtet
                return OperandUtils.CreateFromDecimalInput(splittedToken[0], isNegative);
            }

        }

        /// <summary>
        /// Diese Funktion erstellt aus einem InputToken einen Operator
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Operator CreateOperatorFromInputToken(InputToken token)
        {
            //passiert nicht
            if (token.Type == InputTokenType.Operand)
            {
                throw new ArgumentException("Expected Operator, got Operand");
            }

            //entsprechend dem inhalt des tokens muss der operator richtig bestimmt werden
            //Precedence ist notwendig für Punkt-vor-Strich Rechnung
            switch (token.Token)
            {

                case "*":
                    return new Operator(OperatorType.Multiply,OperatorPrecedence.High);
                case "/":
                    return new Operator(OperatorType.Divide, OperatorPrecedence.High);
                case "+":
                    return new Operator(OperatorType.Add, OperatorPrecedence.Low);
                case "-":
                    return new Operator(OperatorType.Subtract, OperatorPrecedence.Low);
                case "(":
                    return new Operator(OperatorType.OpeningBracket, OperatorPrecedence.Ignore);
                case ")":
                    return new Operator(OperatorType.ClosingBracket, OperatorPrecedence.Ignore);

                default:
                    throw new ArgumentException("Invalid Operator!");
            }


        }

        /// <summary>
        /// Diese Funktion bekommt die User eingabe als string übergeben und Teilt diese in InputToken auf.
        /// Die Eingabe ist ein beliebiger mathematischer term mit grundrechenarten und klammern
        /// </summary>
        /// <param name="input">Term der aufgeteilt werden soll</param>
        /// <returns>Array mit InputToken die den eingegebenen Term repräsentieren</returns>
        public static InputToken[] CreateTokensFromInput(string input)
        {
            //entferne leerzeichen und tabs
            string[] inputWithoutWhitespace = input.Split(new char[] {' ','\t'}, StringSplitOptions.RemoveEmptyEntries);
            input = string.Join("", inputWithoutWhitespace);
            //ersetze negierungen von klammern durch multiplikation mit -1
            input = input.Replace("-(", "-1*(");

            //erstelle eine dynamische liste, da kommen die token rein
            List<InputToken> tokenList = new List<InputToken>();
            bool parsingNumber = false;
            //eine zahl besteht oft aus mehreren zeichen, diese werden im stringbuilder zusammengeführt
            StringBuilder numberBuilder = new StringBuilder();
            //iteriere über jedes zeichen in der eingabe
            for (int i = 0; i < input.Length; i++){
                //wenn das zeichen eine Klammer oder Rechenzeichen ist
                //man könnte auch einzeln prüfen ob das aktuelle zeichen eins der Operatoren ist
                //oder man prüft ob das aktuelle zeichen in der menge aller Operatoren enthalten ist
                if ("()+-*/".Contains(input[i]) )
                {
                    // die vorangegangenden zeichen gehörten zu einer zahl
                    // und das aktuelle zeichen ist ein operand
                    if (parsingNumber)
                    {
                        //somit ist die zahl komplett und wir können diese aus dem Stringbuilder in die liste packen
                        AddOperandTokenToList(tokenList, numberBuilder.ToString());
                        //leere den String builder für die nächste zahl
                        numberBuilder.Clear();
                        //zahl ist fertig, somit kann parsingNumber false werden
                        parsingNumber = false;

                        //das aktuelle zeichen ist ein operator, füge ihn auch zur liste hinzu
                        AddOperatorTokenToList(tokenList, input[i]);
                    }
                    //das zeichen ist ein minus zeichen und parsing number ist false
                    //somit muss das minuszeichen zu einer negativen zahl gehören
                    else
                    if (input[i] == '-')
                    {
                        //füge das zeichen zum stringbuilder hinzu und
                        numberBuilder.Append(input[i]);
                        //merke dir dass wir in eine zahl gefunden haben
                        parsingNumber = true;
                    }
                    else
                    {
                        //sonst ist das ein öder operator, füg ihn zur liste hinzu
                        AddOperatorTokenToList(tokenList, input[i]);
                    }



                }
                //wenn das aktuelle zeichen kein operator ist muss es ein zeichen sein, dass zum operanden gehört
                else
                {
                    //füge das zeichen zum stringbuilder hinzu
                    numberBuilder.Append(input[i]);
                    //merke dir dass wir eine zahl gefunden haben
                    //die folgenden zeichen könnten auch zur zahl gehören
                    parsingNumber = true;
                }
            }

            //am ende noch die letzte zahl zur liste hinzufügen
            if (parsingNumber)
            {
                AddOperandTokenToList(tokenList, numberBuilder.ToString());
                numberBuilder.Clear();
            }

            return tokenList.ToArray();
        }

        /// <summary>
        /// Hilfsfunktion zum hinzufügen eines neuen Operanden als InputToken zu einer liste
        /// </summary>
        /// <param name="list">Liste zu der der Operand hinzugefügt werden soll</param>
        /// <param name="operand">Operand welcher hinzugefügt werden soll</param>
        public static void AddOperandTokenToList(List<InputToken> list, string operand)
        {
            list.Add(new InputToken(operand, InputTokenType.Operand));
        }
        /// <summary>
        /// Hilfsfunktion zum hinzufügen eines neuen Operanden als InputToken zu einer liste
        /// </summary>
        /// <param name="list">Liste zu der der Operand hinzugefügt werden soll</param>
        /// <param name="operand">Operand welcher hinzugefügt werden soll</param>
        public static void AddOperatorTokenToList(List<InputToken> list, char op)
        {
            InputTokenType type = InputTokenType.Operator;

            list.Add(new InputToken(op.ToString(), type));
        }
    }

   
}
