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
                string input = Console.ReadLine();
                try
                {
                    InputToken[] tokens = InputUtils.CreateTokensFromInput(input);

                    if (tokens.Length == 1)
                    {
                        ConvertNumber(tokens[0]);
                    }
                    else
                    {
                        Operand result = CalculateTerm(tokens);
                        ConsoleUtils.WriteResult("= "+result.DecimalValue.ToString());
                    }

                }
                catch (InvalidOperationException iE)
                {
                    ConsoleUtils.WriteError("SyntaxError");
                }
                catch (Exception e)
                {
                    ConsoleUtils.WriteError(e.Message);
                }

                Console.WriteLine("Erneut rechnen? (j/n)");
            } while (Console.ReadKey().KeyChar != 'n');
        }


        public static void ConvertNumber(InputToken number)
        {
            Operand toConvert=InputUtils.CreateOperandFromInputToken(number);
            Console.WriteLine("Bin: "+ ConversionUtils.ConvertToBinary(toConvert));
            Console.WriteLine("Dez: " + toConvert.DecimalValue);
            Console.WriteLine("Okt: " + ConversionUtils.ConvertToOctal(toConvert));
            Console.WriteLine("Hex: "+ ConversionUtils.ConvertToHexadecimal(toConvert));
        }
        public static Operand CalculateTerm(InputToken[] tokens)
        {

            Stack<Operand> operandStack = new Stack<Operand>();
            Stack<Operator> operatorStack = new Stack<Operator>();


            for (int i = 0; i < tokens.Length; i++)
            {
                InputToken currentToken = tokens[i];

                switch (currentToken.Type)
                {

                    case InputTokenType.Operand:
                        operandStack.Push(InputUtils.CreateOperandFromInputToken(currentToken));
                        break;
                    case InputTokenType.Operator:
                        Operator op = InputUtils.CreateOperatorFromInputToken(currentToken);

                        if (op.Type == OperatorType.ClosingBracket)
                        {
                            CalculationUtils.CalculateInsideBrackets(operandStack, operatorStack);
                        }
                        else
                        {
                            Operator top;
                            if (operatorStack.TryPeek(out top))
                            {
                                if (op.Precedence != OperatorPrecedence.Ignore && top.Precedence != OperatorPrecedence.Ignore)
                                {

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

            while (operatorStack.Count > 0)
            {
                CalculationUtils.PopCalculateAndPushOperand(operandStack, operatorStack);
            }

            return operandStack.Pop();

        }


    }
}
