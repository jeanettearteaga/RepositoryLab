using FormulaEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFormulaEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1:");
            Console.WriteLine("Answer: 0");
            Console.WriteLine(Evaluator.Evaluate("2-2", null));

            Console.WriteLine("2:");
            Console.WriteLine("Answer: Can not divide by 0");
            try
            {
                Console.WriteLine(Evaluator.Evaluate("(2/0)", null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            Console.WriteLine("3:");
            Console.WriteLine("Answer: 27");
            Console.WriteLine(Evaluator.Evaluate("(2+3)*5+2", null));

            Console.WriteLine("4:");
            Console.WriteLine("Answer: Can not divide by 0");
            try
            {
                Console.WriteLine(Evaluator.Evaluate("(2+3)/(5-5)", null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            Console.WriteLine("5:");
            Console.WriteLine("Answer: 10");
            //Console.WriteLine(Evaluator.Evaluate("2+7+1", null));

            Console.WriteLine("6:");
            Console.WriteLine("Answer: 8");
            //Console.WriteLine(Evaluator.Evaluate("2+1*3-1", null));

            Console.Read();
        }
    }
}
