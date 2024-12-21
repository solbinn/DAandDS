using System.Text.RegularExpressions;

class Program
{
    static string NormalizeTag(string inputTag)
    {
        if (inputTag[1] == '/')
        {
            inputTag = inputTag.Remove(1, 1);
        }
        return inputTag.ToUpper();
    }

    static void ExtractTagsFromFile(ref MyHashMap<string, int> tagMap)
    {
        using (StreamReader fileReader = new StreamReader("input.txt"))
        {
            Regex tagPattern = new Regex(@"^</?\w+>$");
            string? textLine;
            while ((textLine = fileReader.ReadLine()) != null)
            {
                bool isTag = false;
                string currentTag = "";
                for (int index = 0; index < textLine.Length; index++)
                {
                    if (textLine[index] == '<')
                    {
                        isTag = true;
                        currentTag = "";
                    }

                    if (isTag == true)
                    {
                        currentTag += textLine[index];
                    }

                    if (textLine[index] == '>')
                    {
                        MatchCollection foundTags = tagPattern.Matches(currentTag);
                        if (foundTags.Count > 0)
                        {
                            currentTag = NormalizeTag(currentTag);
                            if (tagMap.ContainsKey(currentTag))
                            {
                                tagMap.Put(currentTag, tagMap.Get(currentTag) + 1);
                            }
                            else tagMap.Put(currentTag, 1);
                        }
                        isTag = false;
                        currentTag = "";
                    }
                }
            }
        }
    }

    static void Main(string[] args)
    {
        MyHashMap<string, int> tagMap = new MyHashMap<string, int>();

        ExtractTagsFromFile(ref tagMap);
        Console.WriteLine(tagMap);
    }
}
