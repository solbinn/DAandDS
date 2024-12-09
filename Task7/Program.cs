using MyVectorLib;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;

class Program
{
    public static void ValidateSegment(string segment, ref string currentIP, ref bool isValidIP, ref int segmentCount)
    {
        int segmentValue = Convert.ToInt32(segment);
        if (segmentValue > 0 && segmentValue < 256 && segment.Length == 3)
        {
            currentIP += segment + '.';
            segmentCount++;
        }
        else
        {
            currentIP = "";
            segmentCount = 0;
        }
        if (segmentCount == 4)
        {
            currentIP = currentIP.Remove(currentIP.Length - 1);
            segmentCount = 0;
            isValidIP = true;
        }
    }

    public static bool IsDuplicateIP(string ipAddress, MyVector<string> ipList)
    {
        for (int i = 0; i < ipList.Size(); i++)
            if (ipAddress == ipList.Get(i)) return true;
        return false;
    }

    public static void WriteToFile(MyVector<string> ipList)
    {
        using (StreamWriter writer = new StreamWriter("Output.txt"))
        {
            for (int i = 0; i < ipList.Size(); i++)
                writer.WriteLine(ipList.Get(i));
        }
    }

    static void Main(string[] args)
    {
        string inputPath = "Input.txt";
        using (StreamReader reader = new StreamReader(inputPath))
        {
            string inputLine;
            MyVector<string> ipList = new MyVector<string>();
            string constructedIP = "";
            bool isValidIP = false;
            int segmentCount = 0;

            while ((inputLine = reader.ReadLine()) != null)
            {
                string[] lineSegments = inputLine.Split(" ");
                foreach (string segment in lineSegments)
                {
                    string[] ipSegments = segment.Split(".");
                    foreach (string block in ipSegments)
                    {
                        ValidateSegment(block, ref constructedIP, ref isValidIP, ref segmentCount);
                        if (isValidIP)
                        {
                            if (!IsDuplicateIP(constructedIP, ipList))
                            {
                                ipList.Add(constructedIP);
                                constructedIP = "";
                                isValidIP = false;
                            }
                            else
                            {
                                constructedIP = "";
                                isValidIP = false;
                            }
                        }
                    }
                }
            }

            WriteToFile(ipList);
            ipList.Print();
        }
    }
}