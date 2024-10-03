using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;
namespace ArraysGenerationLibrary
{
    public static class ArraysGeneration
    {
        public static Random random = new Random();
        public static int[] GenerateFirstGroup(int length)
        {
            int[] array = new int[length];
            for (int i=0;  i<length; i++)
            {
                array[i] = random.Next(1000);
            }
            return array;
        }

        public static int[] GenerateSecondGroup(int length)
        {
            int[] finalArray = new int[length];
            int curIndex = 0;
            int modulSubArray = (int)Math.Pow(10, random.Next(1, (int)Math.Log(length, 10)) + 1);
            while (length > 0)
            {
                int curSubLenth = random.Next(1, modulSubArray);
                if (length - curSubLenth < 0) curSubLenth = length;
                int[] subArray = new int[curSubLenth];
                subArray = GenerateFirstGroup(curSubLenth);
                SortingMethods.QuickSort(subArray);

                for (int i = 0; i < curSubLenth; i++)
                {
                    finalArray[curIndex++] = subArray[i];
                }
                length -= curSubLenth;
            }
            return finalArray;
        }

        public static int[] GenerateThirdGroup(int length)
        {
            int[] sortedArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                sortedArray[i] = random.Next(100000);
            }

            Array.Sort(sortedArray);
            int k = random.Next(1, length / 2);
            for (int m = 0; m < k; m++)
            {
                int i = random.Next(0, length);
                int j = random.Next(0, length);
                SortingMethods.Swap(ref sortedArray[i], ref sortedArray[j]);
            }
            return sortedArray;
        }

        public static int[] GenerateForthGroup(int length)
        {
            int[] finalArray = new int[length];
            finalArray = GenerateFirstGroup(length);
            int repeatNum = random.Next(1000);
            int repeating = ((random.Next(10, 91)) * length) / 100;
            for (int i = 0; i < repeating; i++) finalArray[i] = repeatNum;
            return finalArray;
        }
    }
}
