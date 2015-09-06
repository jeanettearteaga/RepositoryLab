using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
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
                Boolean isPlus = Regex.IsMatch(token[index], @"\+");
                Boolean isDigit = Regex.IsMatch(token[index], @"\d");
                Boolean isVariable;
                Boolean isMinus = Regex.IsMatch(token[index], @"\-");
                Boolean isMultiplication = Regex.IsMatch(token[index], @"\*");
                Boolean isDivide = Regex.IsMatch(token[index], @"\/");
                Boolean isLeftParenth = Regex.IsMatch(token[index], @"\(");
                Boolean isRightParenth = Regex.IsMatch(token[index], @"\)");

                // Checks if the curretn token is an integer then prosecutes in 
                // a certain way depending on what is at the highest position
                // in the operators stack.
                if (isDigit)
                {
                    int currentToken = Int32.Parse(token[index]);

                    if (operators.Count() == 0 || operators.Peek() != '*' || operators.Peek() != ('/'))
                    {
                        Value.Push(currentToken);
                        continue;
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
                    if (operators.Count() >= 2)
                    {
                        switch (operators.Peek())
                        {
                            case '+':
                                Value.Push(add(Value, operators));
                                operators.Push(currentToken);
                                break;

                            case '-':
                                Value.Push(subtract(Value, operators));
                                operators.Push(currentToken);
                                break;
                        }
                    }
                    operators.Push(currentToken);
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
                            Value.Push(add(Value, operators));
                            break;

                        case '-':
                            Value.Push(subtract(Value, operators));
                            break;
                    }

                    if (operators.Peek().Equals('('))
                    {
                        operators.Pop();
                    }
                    else
                        throw new Exception("Parenthesis missing");

                    if (operators.Count() != 0)
                    {
                        switch (operators.Peek())
                        {
                            case '*':
                                Value.Push(multiply(Value, operators));
                                break;

                            case '/':
                                Value.Push(divide(Value, operators));
                                break;
                        }
                    }
                }
            }

            // When operator stack is empty and Value stack contains only one integer.
            if (operators.Count() == 0)
            {
                if (Value.Count() == 1)
                {
                    answer = Value.Pop();
                }
                else
                {
                    throw new Exception("Invalid input");
                }
            }
            // When operator stack has more then one item     
            else
            {
                if (operators.Count() == 1)
                {
                    if ((operators.Peek() == '+') || (operators.Peek() == '-'))
                    {
                        if (Value.Count() == 2)
                        {
                            switch (operators.Peek())
                            {
                                case '+':
                                    answer = add(Value, operators);
                                    break;
                                case '-':
                                    answer = subtract(Value, operators);
                                    break;
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid input");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid input");
                    }
                }
                else
                {
                    throw new Exception("Invalid input");
                }
            }

            return answer;
        }

        public static int add(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 + val2;
        }

        public static int subtract(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 - val2;
        }

        public static int multiply(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 * val2;
        }

        public static int divide(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 / val2;
        }
    }
}
