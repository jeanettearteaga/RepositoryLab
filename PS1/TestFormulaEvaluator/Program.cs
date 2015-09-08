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
            //Evaluator evaluator = new Evaluator();
            //evaluator.Evaluate();


            Console.WriteLine(Evaluator.Evaluate("2-2", null));

            try
            {
                Console.WriteLine(Evaluator.Evaluate("(2/0)", null));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message.ToString());
            }

            Console.WriteLine(Evaluator.Evaluate("(2+3)*5+2", null));

            try
            {
                Console.WriteLine(Evaluator.Evaluate("(2+3)/(5-5)", null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            //Console.WriteLine(Evaluator.Evaluate("2+7+1", null));

            //Console.WriteLine(Evaluator.Evaluate("2+1*3-1", null));

            Console.Read();
        }
    }
}
