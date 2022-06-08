using System;
using static MathOperations.MathOperation;

namespace MathOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // e.g:  ((15 / (7 - (1 + 1)) ) * -3 ) -(2 + (1 + 1))
            Console.WriteLine("Enter Expression eg: ((15 / (7 - (1 + 1)) ) * -3 ) -(2 + (1 + 1))");
            string expression = Console.ReadLine();
            try
            {
                string postfix = PostFix(expression);
                Node r = ExpressionTree(postfix);
                Console.WriteLine(EvalTree(r));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
