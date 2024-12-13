using PolishNotationLib;
namespace Task9
{
    class Program : PolishNotaton
    {
        public static void Main(string[] args)
        {
            string task = "7+8*9";
            string line = CreatePol(task);
            double result = CalculatorPol(line);
            Console.WriteLine(result);
        }
    }
}