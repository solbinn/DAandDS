using ArrayDeque;

class Program
{
    static int CountDigitsInString(string? inputString)
    {
        int digitCount = 0;
        if (inputString is not null)
        {
            for (int index = 0; index < inputString.Length; index++)
            {
                if (Char.IsDigit(inputString[index])) digitCount++;
            }
        }
        return digitCount;
    }

    static int CountWhitespaceInString(string? inputString)
    {
        int whitespaceCount = 0;
        if (inputString is not null)
        {
            for (int index = 0; index < inputString.Length; index++)
            {
                if (inputString[index] == ' ') whitespaceCount++;
            }
        }
        return whitespaceCount;
    }

    static bool AreNumbersComparableInStrings(string? stringA, string? stringB)
    {
        if (CountDigitsInString(stringA) >= CountDigitsInString(stringB)) return true;
        else return false;
    }

    static void LoadFromFile(ref MyArrayDeque<string> queue)
    {
        using (StreamReader reader = new StreamReader("input.txt"))
        {
            string? inputLine;
            while ((inputLine = reader.ReadLine()) != null)
            {
                if (AreNumbersComparableInStrings(inputLine, queue.PeekFirst()))
                {
                    queue.AddLast(inputLine);
                }
                else queue.AddFirst(inputLine);
            }
        }
    }

    static void SaveToFile(MyArrayDeque<string> queue)
    {
        using (StreamWriter writer = new StreamWriter("sorted.txt"))
        {
            while (queue.Size() != 0)
            {
                writer.WriteLine(queue.PollFirst());
            }
        }
    }

    static MyArrayDeque<string> RemoveInvalidLines(MyArrayDeque<string> queue, int minWhitespace)
    {
        MyArrayDeque<string> filteredQueue = new MyArrayDeque<string>();
        while (queue.Size() != 0)
        {
            string? inputLine = queue.PollFirst();
            if (CountWhitespaceInString(inputLine) < minWhitespace)
            {
                filteredQueue.AddLast(inputLine);
            }
        }
        return filteredQueue;
    }

    static void Main(string[] args)
    {
        MyArrayDeque<string> queue = new MyArrayDeque<string>();
        LoadFromFile(ref queue);
        Console.WriteLine(queue.ToString());
        MyArrayDeque<string> backupQueue = new MyArrayDeque<string>(queue.ToArray());
        SaveToFile(backupQueue);

        int spaceCount = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(queue.ToString());
        backupQueue = new MyArrayDeque<string>(queue.ToArray());
        queue = RemoveInvalidLines(backupQueue, spaceCount);
        Console.WriteLine(queue.ToString());
    }
}
