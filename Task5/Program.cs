using ArrayList;
using System.Formats.Asn1;

class Program
{
    public static void CharCheck(ref string tag, char c, ref bool isReady)
    {
        switch (c)
        {
            case '<':
                tag = "<";
                break;
            case '/':
                if (tag == "<") tag += "/";
                else tag = "";
                break;
            case '>':
                CheckFigure(ref tag, c, ref isReady);
                break;
            default:
                if (Char.IsLetter(c))
                {
                    if (tag != "") tag += c;
                }
                else if (Char.IsDigit(c))
                {
                    CheckFigure(ref tag, c, ref isReady);
                }
                else
                {
                    tag = "";
                }
                break;
        }
    }

    public static void CheckFigure(ref string tag, char c, ref bool isReady)
    {
        if (string.IsNullOrEmpty(tag)) return;

        if (tag.Length == 1)
        {
            tag = "";
            return;
        }

        if (tag.Length == 2 && tag[1] == '/')
        {
            tag = "";
            return;
        }

        tag += c;

        if (tag[tag.Length - 1] == '>') isReady = true;
    }

    public static string SgnPart(string s)
    {
        string letterPart = "";
        foreach (char c in s)
            if (Char.IsLetter(c)) letterPart += c;

        return letterPart.ToLower();
    }

    public static bool IsRepeated(string tag, MyArrayList<string> array)
    {
        string s = SgnPart(tag);
        for (int i = 0; i < array.Size(); i++)
            if (s == array.Get(i)) return true;

        return false;
    }

    static void Main(string[] args)
    {
        string path = "Input.txt";
        using (StreamReader sr = new StreamReader(path))
        {
            string line = sr.ReadLine();
            Console.WriteLine("Tags from the file:");
            Console.WriteLine(line);
            MyArrayList<string> array = new MyArrayList<string>();
            MyArrayList<string> sgn = new MyArrayList<string>();
            string tag = "";
            bool tagIsReady = false;

            while (line != null)
            {
                foreach (char c in line)
                {
                    CharCheck(ref tag, c, ref tagIsReady);
                    if (tagIsReady)
                    {
                        if (!IsRepeated(tag, sgn))
                        {
                            array.Add(tag);
                            sgn.Add(SgnPart(tag));
                        }
                        tag = "";
                        tagIsReady = false;
                    }
                }
                line = sr.ReadLine();
            }
            Console.WriteLine("Final list of tags: ");
            array.Print();
        }
    }
}