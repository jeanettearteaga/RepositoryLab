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
            int answer = 0; // Still have to assign the correct value at the end of the for loop.

            Stack<char> operators = new Stack<char>();
            Stack<int> Value = new Stack<int>();

            string[] token = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            for (int index = 0; index < token.Length; index++)
            {
                Boolean isDigit = Regex.IsMatch(token[index], @"\d");
                Boolean isVariable;
                Boolean isPlus = Regex.IsMatch(token[index], "+");
                Boolean isMinus = Regex.IsMatch(token[index], "-");
                Boolean isMultiplication = Regex.IsMatch(token[index], "*");
                Boolean isDivide = Regex.IsMatch(token[index], "/");
                Boolean isLeftParenth = Regex.IsMatch(token[index], "(");
                Boolean isRightParenth = Regex.IsMatch(token[index], ")");

                // Checks if the curretn token is an integer then prosecutes in 
                // a certain way depending on what is at the highest position
                // in the operators stack.
                if (isDigit)
                {
                    int currentToken = Int32.Parse(token[index]);

                    if(!operators.Peek().Equals('*') || !operators.Peek().Equals('/'))
                    {
                        Value.Push(currentToken);
                    }

                    switch (operators.Peek())
                    {
                        case '*':
                            int currentVal = Value.Pop();
                            operators.Pop();
                            int newVal = (currentVal * currentToken);
                            Value.Push(newVal);
                            break;

                        case '/':
                            currentVal = Value.Pop();
                            operators.Pop();
                            newVal = (currentVal / currentToken);
                            Value.Push(newVal);
                            break;
                    }
                }

                if (isPlus || isMinus)
                {
                    char currentToken = Convert.ToChar(token[index]);

                    switch (operators.Peek())
                    {
                        case '+':
                            int val1 = Value.Pop();
                            int val2 = Value.Pop();
                            operators.Pop();
                            int newVal = val1 + val2;
                            Value.Push(newVal);
                            operators.Push(currentToken);
                            break;

                        case '-':
                            val1 = Value.Pop();
                            val2 = Value.Pop();
                            operators.Pop();
                            newVal = val1 + val2;
                            Value.Push(newVal);
                            operators.Push(currentToken);
                            break;
                    }
                }

                else if (isMultiplication || isDivide)
                {
                    char currentToken = Convert.ToChar(token[index]);
                    operators.Push(currentToken);
                }

                else if (isLeftParenth)
                {
                    char currentToken = Convert.ToChar(token[index]);
                    operators.Push(currentToken);
                }

                else if (isRightParenth)
                {
                    char currentToken = Convert.ToChar(token[index]);

                    switch (operators.Peek())
                    {
                        case '+':
                            int val1 = Value.Pop();
                            int val2 = Value.Pop();
                            operators.Pop();
                            int newVal = val1 + val2;
                            Value.Push(newVal);
                            break;

                        case '-':
                            val1 = Value.Pop();
                            val2 = Value.Pop();
                            operators.Pop();
                            newVal = val1 - val2;
                            Value.Push(newVal);
                            break;
                    }

                    //if (operators.Peek().Equals('('))
                    //{
                    //    operators.Pop();
                    //}
                    //else
                    //    thorw error

                    switch (operators.Peek())
                    {
                        case '*':
                            int val1 = Value.Pop();
                            int val2 = Value.Pop();
                            operators.Pop();
                            int newVal = val1 * val2;
                            Value.Push(newVal);
                            break;

                        case '/':
                            val1 = Value.Pop();
                            val2 = Value.Pop();
                            operators.Pop();
                            newVal = val1 / val2;
                            Value.Push(newVal);
                            break;
                    }
                }
            }
            return answer;
        }
    }
}
