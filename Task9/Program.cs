using MyStackLib;
using MyVectorLib;
using System.Data;
using System.Runtime.CompilerServices;


class PolishNotation
{
    public static int GetPriority(string operation)
    {
        switch (operation)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
                return 2;
            case "^":
            case "%":
                return 4;
            case "sqrt":
            case "abs":
            case "sin":
            case "cos":
            case "tan":
            case "ln":
            case "log":
            case "min":
            case "max":
            case "exp":
            case "trunc":
                return 3;
            default: return 0;

        }
    }
    public static bool IsFunction(string str)
    {
        switch (str)
        {
            case "sqrt":
            case "abs":
            case "sin":
            case "cos":
            case "tan":
            case "ln":
            case "log":
            case "min":
            case "max":
            case "exp":
            case "trunc":
                return true;
            default: return false;
        }
    }
    public static bool IsOperation(string str)
    {
        switch (str)
        {
            case "+":
            case "-":
            case "*":
            case "/":
            case "^":
            case "%":
                return true;
            default: return false;
        }
    }
    public static bool IsNumber(string str)
    {
        double num;
        bool result = double.TryParse(str, out num);
        return result;
    }
    public static void DoOperations(string token, ref MyStack<string> operations, ref MyStack<int> priority, ref MyVector<string> outQueue, Dictionary<string, string> arguments)
    {
        if (token == "") return;
        if (token == "pi")
        {
            outQueue.Add(Convert.ToString(Math.PI));
            return;
        }
        if (IsNumber(token))
        {
            outQueue.Add(token);
            return;
        }
        if (IsFunction(token))
        {
            operations.Push(token);
            priority.Push(GetPriority(token));
            return;
        }
        if (token == ";")
        {
            string oper = operations.Pop();
            int operPriotiry = priority.Pop();
            while (oper != "(")
            {
                outQueue.Add(oper);
                if (!operations.IsEmpty())
                {
                    oper = operations.Pop();
                    operPriotiry = priority.Pop();
                }
                else throw new Exception("Expression is wrong, '(' is not found"); ;
            }
            if (oper == "(")
            {
                operations.Push(oper);
                priority.Push(operPriotiry);
                return;
            }
            operations.Push(oper);
            priority.Push(operPriotiry);
            return;
        }
        if (IsOperation(token))
        {
            if (operations.IsEmpty())
            {
                operations.Push(token);
                priority.Push(GetPriority(token));
                return;
            }
            string oper = operations.Peek();
            int operPriotiry = priority.Peek();
            int nowPriority = GetPriority(token);
            while (operPriotiry >= nowPriority)
            {
                outQueue.Add(oper);
                oper = operations.Pop();
                operPriotiry = priority.Pop();
                if (!operations.IsEmpty())
                {
                    oper = operations.Peek();
                    operPriotiry = priority.Peek();
                }
                else break;
            }
            operations.Push(token);
            priority.Push(nowPriority);
            return;
        }
        if (token == ")")
        {
            string oper = operations.Pop();
            int operPriotiry = priority.Pop();
            while (oper != "(")
            {
                outQueue.Add(oper);
                if (!operations.IsEmpty())
                {
                    oper = operations.Pop();
                    operPriotiry = priority.Pop();
                }
                else throw new Exception("Expression is wrong, '(' is mot found");
            }
            if (operations.IsEmpty() && oper == "(") return;
            oper = operations.Pop();
            operPriotiry = priority.Pop();
            if (IsFunction(oper)) outQueue.Add(oper);
            else
            {
                operations.Add(oper);
                priority.Push(operPriotiry);
            }
            return;
        }
        foreach (string simbol in arguments.Keys)
        {
            if (token == simbol)
            {
                outQueue.Add(arguments[simbol]);
                return;
            }
        }

    }
    public static void Calculating(string func, ref MyVector<string> outQueue, ref int index)
    {
        int currentIndex = index;
        if (IsNumber(func)) return;
        if (IsOperation(func))
        {
            double ans = 0;
            double num1 = Convert.ToDouble(outQueue.Get(currentIndex - 1));
            double num2 = Convert.ToDouble(outQueue.Get(currentIndex - 2));
            switch (func)
            {
                case "+":
                    ans = num1 + num2;
                    break;
                case "-":
                    ans = num2 - num1;
                    break;
                case "*":
                    ans = num1 * num2;
                    break;
                case "/":
                    ans = num2 / num1;
                    break;
                case "^":
                    ans = Math.Pow(num2, num1);
                    break;
                case "%":
                    ans = num2 % num1;
                    break;
            }
            outQueue.Set(currentIndex - 2, Convert.ToString(ans));
            outQueue.RemoveByIndex(currentIndex - 1);
            outQueue.RemoveByIndex(currentIndex - 1);
            index -= 2;
            return;
        }
        if (IsFunction(func))
        {
            double ans = 0;
            bool flag = false;
            double num1 = Convert.ToDouble(outQueue.Get(currentIndex - 1));
            switch (func)
            {
                case "sqrt":
                    ans = Math.Sqrt(num1);
                    flag = true;
                    break;
                case "abs":
                    ans = Math.Abs(num1);
                    flag = true;
                    return;
                case "sin":
                    ans = Math.Sin(num1);
                    flag = true;
                    break;
                case "cos":
                    ans = Math.Cos(num1);
                    flag = true;
                    break;
                case "tan":
                    ans = Math.Tan(num1);
                    flag = true;
                    break;
                case "ln":
                    ans = Math.Log(num1);
                    flag = true;
                    break;
                case "exp":
                    ans = Math.Exp(num1);
                    flag = true;
                    break;
                case "trunc":
                    ans = Math.Truncate(num1);
                    flag = true;
                    break;
            }
            if (flag)
            {
                outQueue.Set(currentIndex - 1, Convert.ToString(ans));
                outQueue.RemoveByIndex(currentIndex);
                index--;
                return;
            }
            double num2 = Convert.ToDouble(outQueue.Get(currentIndex - 2));
            switch (func)
            {
                case "log":
                    ans = Math.Log(num2, num1);
                    break;
                case "min":
                    ans = Math.Min(num1, num2);
                    break;
                case "max":
                    ans = Math.Max(num1, num2);
                    break;
            }
            outQueue.Set(currentIndex - 2, Convert.ToString(ans));
            outQueue.RemoveByIndex(currentIndex - 1);
            outQueue.RemoveByIndex(currentIndex - 1);
            index -= 2;
            return;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your expression: ");
        string expression = Console.ReadLine();
        Dictionary<string, string> arguments = new Dictionary<string, string>();
        MyStack<string> stackOfOperations = new MyStack<string>();
        MyVector<string> outQueue = new MyVector<string>();
        MyStack<int> stackOfPriority = new MyStack<int>();
        string token = "";
        string letter = "";
        string number = "";
        foreach (string argument in args)
        {
            foreach (char element in argument)
            {
                if (Char.IsLetter(element)) letter = element.ToString();
                if (Char.IsDigit(element)) number += element.ToString();
            }
            arguments[letter] = number;
            letter = "";
            number = "";
        }

        if (expression != null)
        {
            foreach (char simbol in expression)
            {
                if (simbol == '(')
                {
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                    stackOfOperations.Push("(");
                    stackOfPriority.Add(0);
                    token = "";
                    continue;
                }
                if (simbol == ')')
                {
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                    token = ")";
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                }
                if (simbol == ';')
                {
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                    token = ";";
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                }
                if (simbol == ' ')
                {
                    DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
                    token = "";
                }
                else token += simbol;

            }
            DoOperations(token, ref stackOfOperations, ref stackOfPriority, ref outQueue, arguments);
            while (!stackOfOperations.IsEmpty())
            {
                outQueue.Add(stackOfOperations.Pop());
                stackOfPriority.Pop();
            }
        }
        outQueue.Print();
        Console.WriteLine("\n");
        for (int i = 0; i < outQueue.Size(); i++)
        {
            Calculating(outQueue.Get(i), ref outQueue, ref i);
        }
        outQueue.Print();
    }
}