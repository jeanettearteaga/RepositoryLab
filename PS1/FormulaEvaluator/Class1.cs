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
            Stack<char> operators = new Stack<char>();
            Stack<int> Value = new Stack<int>();

            string[] token = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            for (int index = 0; index < token.Length; index++)
            {
                Boolean isDigit = Regex.IsMatch(token[index], @"\d");
                //if (isDigit && (operators(char).Peek.equals("*") || ))
                //{
                //  Value<int>.Push(token[index]);
                //}
                //else
                //{
                //  operators<Char>.Push(substring[index]);
                //}

            }
            return 0;
        }
    }
}
