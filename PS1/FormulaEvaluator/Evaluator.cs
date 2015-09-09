using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    /// <summary>
    /// A Class that Calculates a mathematical format string mathematically including letters.
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// Delegate that takes in as input the Letter or letters that was in the string input of Evaluate 
        /// and "Looks up the value of that letter. It will return that value"
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public delegate int Lookup(String v);

        /// <summary>
        /// Function that takes in a string which is the Mathematical expression and a 
        /// Delegate for when we have an unknown variable like A, B7 etc.
        /// It returns the value of the solved mathematical expression from the input string.
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="variableEvaluator"></param>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            int answer = 0;

            //FormulaEvaluator.Evaluator.Lookup;

            Stack<char> operators = new Stack<char>();
            Stack<int> Value = new Stack<int>();

            string[] token = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            for (int index = 0; index < token.Length; index++)
            {
                Boolean isPlus = Regex.IsMatch(token[index], @"\+");
                Boolean isDigit = Regex.IsMatch(token[index], @"\d");
                Boolean isMinus = Regex.IsMatch(token[index], @"\-");
                Boolean isMultiplication = Regex.IsMatch(token[index], @"\*");
                Boolean isDivide = Regex.IsMatch(token[index], @"\/");
                Boolean isLeftParenth = Regex.IsMatch(token[index], @"\(");
                Boolean isRightParenth = Regex.IsMatch(token[index], @"\)");
                Boolean isVariable;

                // Checks if the curretn token is an integer then prosecutes in 
                // a certain way depending on what is at the highest position
                // in the operators stack.
                if (isDigit)
                {
                    int currentToken = Int32.Parse(token[index]);
                    
                    if ((operators.Count() == 0) || ((operators.Peek() != '*') && (operators.Peek() != '/')))
                    {
                        Value.Push(currentToken);
                        continue;
                    }

                    switch (operators.Peek())
                    {
                        case '*':
                            int currentVal = Value.Pop();
                            operators.Pop();
                            Value.Push(multiplyByToken(currentVal, currentToken));
                            break;

                        case '/':
                            currentVal = Value.Pop();
                            operators.Pop();
                            Value.Push(divideByToken(currentVal, currentToken));
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

        /// <summary>
        /// Takes in a stack of ints and a stack of chars
        /// 
        /// Function that adds the two popped values of a stack of ints and pops the add 
        /// sign from a stack of chars.
        /// 
        /// Returns the added popped integers from the int stack.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static int add(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 + val2;
        }

        /// <summary>
        /// Takes in a stack of ints and a tack of chars.
        /// 
        /// Function that subtracts the two popped values of a stack of ints and 
        /// pops the minus symbol from the stack of chars.
        /// 
        /// Returns the second popped int minus the first popped int.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static int subtract(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val2 - val1;
        }

        /// <summary>
        /// Takes in a stack of ints and a stack of chars.
        /// 
        /// Function that pops and multiplies the top two ints of the integer stack and 
        /// then pops the char stack to get rid of the multiply symbol.
        /// 
        /// Returns the multiplied result. 
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static int multiply(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            operators.Pop();
            return val1 * val2;
        }


        /// <summary>
        /// Takes in a satck of ints and a stack of chars.
        /// 
        /// Function that pops the two top values of the Value stack and then 
        /// divides the second popped int with the first popped int. And then pops the 
        /// cahr stack to get rid of the divide sign when called in Evaluator Method.
        /// 
        /// Returns the divided result.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static int divide(Stack<int> Value, Stack<char> operators)
        {
            int val1 = Value.Pop();
            int val2 = Value.Pop();
            if(val1 == 0)
            {
                throw new Exception("Can not divide by zero");
            }
            operators.Pop();
            return val2 / val1;
        }

        /// <summary>
        /// Takes in two integers 
        /// 
        /// Divides the int called numerator by the int called denominator and returns it.
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <returns></returns>
        public static int divideByToken(int numerator, int denominator)
        {
            if(denominator == 0)
            {
                throw new Exception("Can not divide by zero");
            }
             return (numerator / denominator);
        }

        /// <summary>
        /// Takes in two integers 
        /// 
        /// Multiplies the two integers and returns them.
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int multiplyByToken(int num1, int num2)
        {
            return num1 * num2;
        }

        public static System.Runtime.Serialization.SerializationInfo A2 { get; set; }
    }
}
