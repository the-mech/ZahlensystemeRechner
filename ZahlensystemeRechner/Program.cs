using System;
using System.Collections.Generic;

namespace ZahlensystemeRechner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# is best!! Java suucks");

            bool done = false;

            string input = "2 +1 3";
            input.Trim();
            ProcessInput(input);
        }

        private static char[] availableOperators = { '+', '-' };

        private static Stack<string> operators = new Stack<string>();
        private static Stack<string> operands = new Stack<string>();

        static void ProcessInput(string input)
        {
            for(int i = 0; i<input.Length; i++)
            {
                char c = input[i];

            }
        }
    }
}
