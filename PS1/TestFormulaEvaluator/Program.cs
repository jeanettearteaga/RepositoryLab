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
            Console.Read();
        }
    }
}
