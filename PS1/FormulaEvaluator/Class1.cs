using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    public class Class1
    {
    }


    public static class Evaluator
    {

        public delegate int Lookup(String v);

        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            Stack<Char> operators = new Stack<char>();
            Stack<int> operands = new Stack<int>();

            string[] substring = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            for (int index = 0; index < substring.Length; index++)
            {

            }
            return 0;
        }
    }
}
