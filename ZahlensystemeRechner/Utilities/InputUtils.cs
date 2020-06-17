using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    public class InputUtils
    {
        //(2*-4)+2
        //( 2 * -4 ) + 2
        //-h_AFFEF
        public static Operand CreateOperandFromInputToken(InputToken token)
        {
            string input = token.Token;

            bool isNegative = false;

            if (input[0] == '-')
            {
                isNegative = true;
                input = input.Remove(0, 1);
            }

            string[] tokens = input.Split('_');
            if (tokens.Length > 2)
            {
                throw new ArgumentException("Operand "+input+" ist ungültig.");
            }
            else if (tokens.Length > 1)
            {
                //erster part im string ist das prefix zweiter part ist der wert
                
                string prefix = tokens[0];
                string value = tokens[1];

                if (prefix.Length > 1)
                {
                    throw new ArgumentException("Prefix <"+prefix+"> ist ungültig.");
                }
                else
                {

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
                            throw new ArgumentException("Prefix <" + prefix + ">ist ungültig.");
                    }
                }
                
            }
            else
            {
                return OperandUtils.CreateFromDecimalInput(tokens[0], isNegative);
            }

        }
        public static Operator CreateOperatorFromInputToken(InputToken token)
        {
            if (token.Type == InputTokenType.Operand)
            {
                throw new ArgumentException("Expected Operator, got Operand");
            }

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

        public static InputToken[] CreateTokensFromInput(string input)
        {

            input = input.Replace("-(", "-1*(");

            List<InputToken> token = new List<InputToken>();
            bool parsingNumber = false;
            StringBuilder numberBuilder = new StringBuilder();
            for (int i = 0; i < input.Length; i++){
                if ("()+-*/".Contains(input[i]) )
                {

                    if (parsingNumber)
                    {
                        AddOperandTokenToList(token, numberBuilder.ToString());
                        numberBuilder.Clear();
                        parsingNumber = false;

                        AddOperatorTokenToList(token, input[i]);
                    }
                    else
                        if (input[i] == '-')
                        {
                            numberBuilder.Append(input[i]);
                            parsingNumber = true;
                        }
                    else
                    {
                        AddOperatorTokenToList(token, input[i]);
                    }



                }
                else
                {
                    numberBuilder.Append(input[i]);
                    parsingNumber = true;
                }
            }

            //am ende noch die letzte zahl zur liste hinzufügen
            if (parsingNumber)
            {
                token.Add(new InputToken(numberBuilder.ToString(), InputTokenType.Operand));
                numberBuilder.Clear();
            }

            return token.ToArray();
        }

        public static void AddOperandTokenToList(List<InputToken> list, string operand)
        {
            list.Add(new InputToken(operand, InputTokenType.Operand));
        }

        public static void AddOperatorTokenToList(List<InputToken> list, char op)
        {
            InputTokenType type = InputTokenType.Operator;

            list.Add(new InputToken(op.ToString(), type));
        }
    }

   
}
