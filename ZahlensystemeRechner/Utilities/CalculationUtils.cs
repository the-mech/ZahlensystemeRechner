using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    class CalculationUtils
    {


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


        public static void PopCalculateAndPushOperand(Stack<Operand> operands, Stack<Operator> operators)
        {
            Operator op = operators.Pop();
            Operand right = operands.Pop();
            Operand left = operands.Pop();

            operands.Push(Calculate(left, right, op));
        }


        public static void CalculateInsideBrackets(Stack<Operand> operands, Stack<Operator> operators)
        {
            while (operators.Peek().Type != OperatorType.OpeningBracket)
            {
                PopCalculateAndPushOperand(operands, operators);
            }

            operators.Pop();
        }

    }
}
