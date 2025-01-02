using System.Text.RegularExpressions;
using HashMap;
using LinkedList;

public class VariableHandler
{
    enum DataType
    {
        Integer,
        Double,
        Float
    }

    static void Main(string[] args)
    {
        try
        {
            MyHashMap<string, string> variableMap = new MyHashMap<string, string>();
            string regexPattern = @"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)";
            string filePath = "input.txt";
            StreamReader reader = new StreamReader(filePath);
            string? currentLine = reader.ReadLine();

            if (currentLine == null) Console.WriteLine("Строка пуста");

            while (currentLine != null)
            {
                MatchCollection foundMatches = Regex.Matches(currentLine, regexPattern);
                foreach (Match foundMatch in foundMatches)
                {
                    string[] splitParts = foundMatch.Value.Split(' ');
                    string dataType = splitParts[0].Trim();
                    string assignedValue = splitParts[3].Trim();
                    string variableName = splitParts[1].Trim();
                    string combinedValue = dataType + " " + assignedValue;

                    if (variableMap.ContainsKey(variableName))
                        Console.WriteLine("Повтор " + $"{dataType} {variableName}={assignedValue}");
                    else
                        variableMap.Put(variableName, combinedValue);
                }
                currentLine = reader.ReadLine();
            }
            reader.Close();
            var entries = variableMap.EntrySet();
            foreach (var entry in entries)
                Console.WriteLine(entry.Value + " " + entry.Key);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception : " + ex.Message);
        }
    }
}
