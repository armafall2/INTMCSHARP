using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class ElementaryOperations
    {

        public static void BasicOperation(int a, int b, char operation)
        {
            int result = 0;

            switch (operation)
            {
                case '+':
                    result = a + b;
                    Console.WriteLine($"{a} {operation} {b} = {result}");
                    break;

                case '-':
                    result = a - b;
                    Console.WriteLine($"{a} {operation} {b} = {result}");
                    break;

                case '/':
                    if (b == 0)
                    {
                        Console.WriteLine($"{a} {operation} {b} = Opération Invalide");
                    }
                    else
                    {
                        result = a / b;
                        Console.WriteLine($"{a} {operation} {b} = {result}");
                    }
                    break;

                case '*':
                    result = a * b;
                    Console.WriteLine($"{a} {operation} {b} = {result}");
                    break;

                default:
                    Console.WriteLine($"{a} {operation} {b} = Opération Invalide");
                    break;
            }

        }

        public static void IntegerDivision(int a, int b)
        {
            int result = 0;
            int r = 0;
            if (b == 0)
            {
                Console.WriteLine("Opération invalide");
            }
            else
            {
                result = a / b;
                r = a - (b * result);

                if (r != 0)
                {
                    Console.WriteLine($"{a} / {b} = {result} + {r}");
                }
                else
                {
                    Console.WriteLine($"{a} / {b} = {result}");
                }
            }


        }

        public static void Pow(int a, int b)
        {
            int result = 1;
            if (b < 0)
            {
                Console.WriteLine($"Opération invalide");
            }
            else
            {
                for (int i = 0; i < b; i++)
                {
                    result *= a;
                }
                Console.WriteLine($"{a} ^ {b} = {result}");
            }

        }
    }
}
