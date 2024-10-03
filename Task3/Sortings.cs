using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
namespace Sorting
{
    public static class SortingMethods
    {
        public class TreeNode
    {
        public TreeNode(int data)
        {
            Data = data;
        }
        public int Data { get; set; }
        public TreeNode Left { get; set; }

        public TreeNode Right { get; set; }

        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }

        public int[] Transform(List<int> elements = null)
        {
            if (elements == null)
            {
                elements = new List<int>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }
    }

            public static int[] BubbleSort(int[] array)
            {
                bool swapped;
                int copy;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    swapped = false;
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j] > array[j + 1])
                        {
                            copy = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = copy;
                            swapped = true;
                        }
                    }
                    if (!swapped) break;
                }
                return array;
            }
            public static int[] ShakerSort(int[] array1)
            {
                bool swapped;
                int copy;
                for (int i = 0; i < array1.Length / 2; i++)
                {
                    swapped = false;
                    for (int j = i; j < array1.Length - i - 1; j++)
                    {
                        if (array1[j] > array1[j + 1])
                        {
                            copy = array1[j];
                            array1[j] = array1[j + 1];
                            array1[j + 1] = copy;
                            swapped = true;
                        }
                    }
                    for (int j = array1.Length - 2 - i; j > i; j--)
                    {
                        if (array1[j] < array1[j - 1])
                        {
                            copy = array1[j - 1];
                            array1[j - 1] = array1[j];
                            array1[j] = copy;
                            swapped = true;
                        }
                    }
                    if (!swapped) break;
                }
                return array1;
            }
            public static int[] CombSort(int[] array1)
            {
                bool swapped;
                int copy;
                double step = array1.Length - 1;
                swapped = false;
                while (step > 1)
                {
                    for (double j = 0; j + step < array1.Length; j++)
                    {
                        if (array1[(int)j] > array1[(int)(j + step)])
                        {
                            copy = array1[(int)j];
                            array1[(int)j] = array1[(int)(j + step)];
                            array1[(int)(j + step)] = copy;
                            swapped = true;
                            step /= 1.3;
                        }
                    }
                }
                for (double i = 0; i < array1.Length - 1; i++)
                {
                    swapped = false;
                    for (int j = 0; j < array1.Length - 1 - i; j++)
                    {
                        if (array1[j] > array1[j + 1])
                        {
                            copy = array1[j];
                            array1[j] = array1[j + 1];
                            array1[j + 1] = copy;
                            swapped = true;
                        }
                    }
                    if (!swapped) break;
                }
                return array1;
            }
            public static int[] InsertionSort(int[] array1)
            {
                int copy;
                for (int i = 1; i < array1.Length; i++)
                {
                    copy = array1[i];
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (array1[j] < copy)
                        {
                            array1[j + 1] = array1[j];
                            array1[j] = copy;
                        }
                    }
                }
                return array1;
            }

            public static void Swap(ref int firstNominal, ref int secondNominal)
            {
                var copyNominal = firstNominal;
                firstNominal = secondNominal;
                secondNominal = copyNominal;
            }

            public static int[] ShellSort(int[] array1)
            {
                var step = array1.Length / 2;
                while (step >= 1)
                {
                    for (var i = step; i < array1.Length; i++)
                    {
                        var j = i;
                        while ((j >= step) && (array1[j - step] > array1[j]))
                        {
                            Swap(ref array1[j], ref array1[j - step]);
                            j = j - step;
                        }
                    }

                    step = step / 2;
                }

                return array1;
            }
            public static int[] TreeSort(int[] array1)
            {
                TreeNode root = new TreeNode(array1[0]);
                for (int i = 1; i < array1.Length; i++) root.Insert(new TreeNode(array1[i]));
                int[] newArray = root.Transform();
                for (int i = 0; i < array1.Length; i++)
                {

                    array1[i] = newArray[i];
                }
                return array1;
            }

            public static int[] GnomeSort(int[] array1)
            {
                var index = 1;
                var nextIndex = index + 1;

                while (index < array1.Length)
                {
                    if (array1[index - 1] < array1[index])
                    {
                        index = nextIndex;
                        nextIndex++;
                    }
                    else
                    {
                        Swap(ref array1[index - 1], ref array1[index]);
                        index--;
                        if (index == 0)
                        {
                            index = nextIndex;
                            nextIndex++;
                        }
                    }
                }

                return array1;
            }
            public static int IndexOfMin(int[] array, int n)
            {
                int result = n;
                for (var i = n; i < array.Length; ++i)
                {
                    if (array[i] < array[result])
                    {
                        result = i;
                    }
                }

                return result;
            }

            public static int[] SelectionBaseSort(int[] array1, int currentIndex = 0)
            {
                if (currentIndex == array1.Length)
                    return array1;

                var index = IndexOfMin(array1, currentIndex);
                if (index != currentIndex)
                {
                    Swap(ref array1[index], ref array1[currentIndex]);
                }

                return SelectionBaseSort(array1, currentIndex + 1);
            }

        public static int[] SelectionSort(int[] array1)
        {
            SelectionBaseSort(array1, 0);
            return array1;
        }
            static int heapSize;
            public static void BuildHeap(int[] array1)
            {
                heapSize = array1.Length - 1;
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    HeapBaseSort(array1, i);
                }
            }
            public static int[] HeapBaseSort(int[] array1, int index)
            {
                int left = 2 * index;
                int right = 2 * index + 1;
                int largest = index;

                if (left <= heapSize && array1[left] > array1[index])
                {
                    largest = left;
                }

                if (right <= heapSize && array1[right] > array1[largest])
                {
                    largest = right;
                }

                if (largest != index)
                {
                    Swap(ref array1[index], ref array1[largest]);
                    HeapBaseSort(array1, largest);
                }
                return array1;
            }

        public static int[] HeapSort(int[] array1)
        {
            HeapBaseSort(array1, 0);
            return array1;
        }

            public static void QuickSort(int[] array1, int left, int right)
            {

                if (left > right) return;

                int p = array1[(left + right) / 2];
                int i = left;
                int j = right;
                while (i <= j)
                {
                    while (array1[i] < p) i++;
                    while (array1[j] > p) j--;
                    if (i <= j)
                    {
                        Swap(ref array1[i], ref array1[j]);
                        i++;
                        j--;
                    }
                }
                QuickSort(array1, left, j);
                QuickSort(array1, i, right);

            }
            public static int[] QuickSort(int[] array1)
            {
                QuickSort(array1, 0, (array1.Length) - 1);
                return array1;
            }

            public static int[] Merge(int[] array1, int left, int middle, int right, bool isReverse)
            {
                int length1 = middle - left + 1;
                int length2 = right - middle;

                int[] arrayLeft = new int[length1];
                int[] arrayRight = new int[length2];

                for (int i = 0; i < length1; i++) arrayLeft[i] = array1[left + i];
                for (int i = 0; i < length2; i++) arrayRight[i] = array1[middle + i + 1];

                int indexLeft = 0, indexRight = 0, index = left;

                while (indexLeft < length1 && indexRight < length2)
                {
                    if ((!isReverse && arrayLeft[indexLeft] < arrayRight[indexRight]) || (isReverse && arrayLeft[indexLeft] > arrayRight[indexRight]))
                    {
                        array1[index] = arrayLeft[indexLeft];
                        indexLeft++;
                    }
                    else
                    {
                        array1[index] = arrayRight[indexRight];
                        indexRight++;
                    }
                    index++;
                }

                while (indexLeft < length1)
                {
                    array1[index] = arrayLeft[indexLeft];
                    indexLeft++;
                    index++;
                }
                while (indexRight < length2)
                {
                    array1[index] = arrayRight[indexRight];
                    indexRight++;
                    index++;
                }
                return array1;
            }
        public static int[] MergeBaseSort(int[] array1, bool isReverse = false)
        {
            int left = 0;
            int right = array1.Length - 1;
            int middle=array1.Length /2;
            Merge(array1, left, middle, right, isReverse);
            return array1;
        }

        public static int[] MergeSort(int[] array1)
        {
            bool reverse = false;
            MergeBaseSort(array1, reverse);
            return array1;
        }

            public static int[] CountingBaseSort(int[] array1, int k)
            {
                var count = new int[k + 1];
                for (var i = 0; i < array1.Length; i++)
                {
                    count[array1[i]]++;
                }

                var index = 0;
                for (var i = 0; i < count.Length; i++)
                {
                    for (var j = 0; j < count[i]; j++)
                    {
                        array1[index] = i;
                        index++;
                    }
                }

                return array1;
            }

            public static int[] CountingSort(int[] array1)
        {
            int k=0;
            CountingBaseSort(array1, k);
                return array1;
        }

            public static int GetMaxVal(int[] array1, int size)
            {
                var maxVal = array1[0];
                for (int i = 1; i < size; i++)
                    if (array1[i] > maxVal)
                        maxVal = array1[i];
                return maxVal;
            }

            public static int[] RadixSort(int[] array1)
            {
                if (array1 == null || array1.Length == 0)
                    return array1;

                int i, j;
                var tmp = new long[array1.Length];
                for (int shift = sizeof(long) * 8 - 1; shift > -1; --shift)
                {
                    j = 0;
                    for (i = 0; i < array1.Length; ++i)
                    {
                        var move = (array1[i] << shift) >= 0;
                        if (shift == 0 ? !move : move)
                            array1[i - j] = array1[i];
                        else
                            tmp[j++] = array1[i];
                    }
                    Array.Copy(tmp, 0, array1, array1.Length - j, j);
                }

                return array1;
            }

            public static void CompareAndSwap(int[] array1, int i, int j, int direction)
            {
                int k;

                k = array1[i] > array1[j] ? 1 : 0;

                if (direction == k)
                {
                    Swap(ref array1[i], ref array1[j]);
                }
            }

            public static void BitonicMerge(int[] array1, int low, int count, int direction)
            {
                if (count > 1)
                {
                    int k = count / 2;
                    for (int i = low; i < low + k; i++)
                    {
                        CompareAndSwap(array1, i, i + k, direction);
                    }
                    BitonicMerge(array1, low, k, direction);
                    BitonicMerge(array1, low + k, k, direction);
                }
            }
            public static void BitonicBaseSort(int[] array1, int low, int count, int direction)
            {
                if (count > 1)
                {
                    int k = count / 2;
                    BitonicBaseSort(array1, low, k, 1);
                    BitonicBaseSort(array1, low + k, k, 0);
                    BitonicMerge(array1, low, count, direction);
                }
            }
        public static int[] BitonicSort(int[] array1)
        {
            BitonicBaseSort(array1, array1.Length, 0, 0);
            return array1;
        }
        }

    }






