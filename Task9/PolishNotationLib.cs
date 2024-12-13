using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace PolishNotationLib
{
    public class PolishNotaton : stack.MyStack<double>
    {
        public static int Priority(string line)
        {
            switch (line)
            {
                case "+":
                case "-":
                case "<":
                case ">":
                    return 1;
                case "*":
                case "/":
                case "^":
                case "(-1)*":
                case "sqrt":
                case "abs":
                case "sin":
                case "cos":
                case "tg":
                case "ln":
                case "lg":
                case "%":
                case "//":
                case "exp":
                case "trunc":
                    return 2;
                default:
                    return 0;
            }
        }
        public static string CreatePol(string line)
        {
            stack.MyStack<string> operators = new stack.MyStack<string>();
            string result = "";
            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    result += c;
                }
                else if (IsOperator(c.ToString()))
                {
                    while (operators.Search(operators.Peek()) > 0 && IsOperator(operators.Peek()) && Priority(operators.Peek()) >= Priority(c.ToString()))
                    {
                        result += operators.Pop();
                    }
                    operators.Push(c.ToString());
                }
                else if (c.ToString() == "(")
                {
                    operators.Push(c.ToString());
                }
                else if (c.ToString() == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        result += operators.Pop();
                    }
                    operators.Pop();
                }
            }
            while (operators.Search(operators.Peek()) > 0)
            {
                result += operators.Pop();
            }
            return result;
        }

        private static bool IsOperator(string c)
        {
            return c == "+" || c == "-" || c == "*" || c == "/" || c == "^" || c == "(-1)*" || c == "sqrt" || c == "abs" || c == "sin" || c == "cos" || c == "tg" || c == "ln" || c == "lg" || c == ">" || c == "<" || c == "%" || c == "//" || c == "exp" || c == "trunc";
        }

        public static double CalculatorPol(string line)
        {
            stack.MyStack<double> list = new stack.MyStack<double>();
            string[] tokens = line.Split(' ');

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double operand))
                {
                    list.Push(operand);
                }
                else
                {
                    double operand2 = list.Pop();
                    double operand1 = list.Pop();
                    switch (token)
                    {
                        case "+":
                            list.Push(operand1 + operand2);
                            break;
                        case "-":
                            list.Push(operand1 - operand2);
                            break;
                        case "*":
                            list.Push(operand1 * operand2);
                            break;

                        case "/":
                            list.Push(operand1 / operand2);
                            break;

                        case "^":
                            double s = 1;
                            for (int i = 0; i < operand2; i++) s *= operand1;
                            list.Push(s);
                            break;
                        case "(-1)*":
                            list.Push((-1) * operand1);
                            break;
                        case "sqrt":
                            list.Push(Math.Sqrt(operand1));
                            break;
                        case "abs":
                            list.Push(Math.Abs(operand1));
                            break;
                        case "sin":
                            list.Push(Math.Sin(operand1));
                            break;
                        case "cos":
                            list.Push(Math.Cos(operand1));
                            break;
                        case "tg":
                            list.Push(Math.Tan(operand1));
                            break;
                        case "ln":
                            list.Push(Math.Log(operand1));
                            break;
                        case "lg10":
                            list.Push(Math.Log10(operand1));
                            break;
                        case "<":
                            if (operand1 < operand2) list.Push(operand2);
                            break;
                        case ">":
                            if (operand1 > operand2) list.Push(operand1);
                            break;
                        case "%":
                            list.Push(operand2 % operand1);
                            break;
                        case "//":
                            list.Push(operand2 / operand2 - operand2 % operand1);
                            break;
                        case "exp":
                            list.Push(Math.Exp(operand1));
                            break;
                        case "trunc":
                            list.Push(Math.Round(operand1));
                            break;
                        default:
                            return 0;

                    }
                }

            }
            return list.Pop();

        }
    }
}