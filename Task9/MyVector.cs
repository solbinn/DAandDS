using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vector
{
    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        public MyVector(int initialCapacity, int capacitIncrement)
        {
            elementData = null;
            elementCount = initialCapacity;
            capacityIncrement = capacitIncrement;
        }


        public MyVector(int initialCapacity)
        {
            elementData = null;
            elementCount = initialCapacity;
            capacityIncrement = 0;
        }

        public MyVector()
        {
            elementData = null;
            elementCount = 10;
            capacityIncrement = 0;
        }
        public MyVector(T[] a)
        {
            elementData = new T[a.Length];
            for (int i = 0; i < a.Length; i++) elementData[i] = a[i];
            elementCount = a.Length;
            capacityIncrement = 0;

        }

        public void Add(T e)
        {
            if (elementCount == elementData.Length)
            {
                if (capacityIncrement == 0)
                {
                    T[] array = new T[(int)(elementCount * 2) + 1];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
                else
                {
                    T[] array = new T[(int)(elementCount + capacityIncrement) + 1];
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    elementData = array;
                }
            }
            elementData[elementCount] = e;
        }
        public void AddAll(T[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++) Add(a[i]);
        }
        
        public void Clear() { elementCount = 0; capacityIncrement = 0; }
        
        public bool Contains(T o)
        {
            foreach (T t in elementData)
            {
                if (t.Equals(o) || (t == null && o == null)) return true;
            }
            return false;
        }

        
        public bool ContainsAll(T[] a)
        {
            int n = a.Length;
            for (int i = 0; i < elementData.Length; i++)
            {
                if (Contains(elementData[i])) return true;
            }
            return false;
        }
        public bool isEmpty()
        {
            if (elementCount == 0 && capacityIncrement == 0) return true;
            else return false;
        }

        
        public void Remove(T o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (Contains(o))
                {
                    T removedElement = elementData[i]; 
                    for (int j = i; j < elementCount - 1; j++)
                    {
                        elementData[j] = elementData[j + 1]; 
                    }
                    elementCount--; 
                }
            }

        }
        
        public void RemoveAll(T[] a)
        {
            int len = a.Length;
            for (int i = 0; i < len; i++) Remove(a[i]);
        }
        
        public void RetainALl(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
            elementCount = a.Length;
        }
        
        public int Size()
        {
            return elementCount;
        }
        
        public void ToArray()
        {
            T[] array = new T[elementCount];
            for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
        }
        
        public void ToArray(T[] a)
        {
            int n = a.Length;
            if (n <= elementCount && n > 0)
            {
                T[] array = new T[n + elementCount];
                for (int i = 0; i < n; i++) array[i] = a[i];
                for (int i = n; i < elementCount; i++) array[i] = elementData[i];
                a = array;

            }
            else
            {
                T[] arr = new T[elementCount];
                for (int i = 0; i < elementCount; i++) arr[i] = elementData[i];
                a = arr;
            }
        }

        
        public void Add(int index, T e)
        {

            
            if (elementCount == elementData.Length)
            {

                T[] array = new T[(int)(elementCount * 1.5) + 1];
                for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                elementData = array;
            }

            
            for (int i = elementCount; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }

            
            elementData[index] = e;
            elementCount++;
        }

        
        public void AddAll(int e, T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(i, a[i]);
        }
        
        public T Get(int index)
        {
            return elementData[index];
        }

        
        public int IndexOf(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(o))
                {
                    return i; 
                }
            }
            return -1; 
        }
        
        public int LastIndexOf(object o)
        {
            int lastIndex = -1;
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(o))
                {
                    lastIndex = i; 
                }
            }
            return lastIndex; 
        }
        
        public T Remove(int index)
        {

            T removedElement = elementData[index]; 
            for (int i = index; i < elementCount - 1; i++)
            {
                elementData[i] = elementData[i + 1]; 
            }
            elementCount--; 
            return removedElement; 
        }
        
        public void Set(int index, T e) => elementData[index] = e;
        
        public T[] SubList(int fromIndex, int toIndex)
        {

            int length = toIndex - fromIndex; 
            T[] subArray = new T[length]; 
            for (int i = 0; i < length; i++)
            {
                subArray[i] = elementData[fromIndex + i]; 
            }
            return subArray; 
        }
        public void Print()
        {
            for (int i = 0; i < elementCount; i++)
            {
                Console.WriteLine(elementData[i] + " ");
            }
        }

        public T FirstElement()
        {
            return elementData[0];
        }
        public T LastElement()
        {
            return elementData[elementCount - 1];
        }
        public void RemoveElementAt(int pos)
        {
            for (int i = pos; i < elementCount - 1; i++)
            {
                elementData[i] = elementData[i + 1]; 
            }
            elementCount--; 
        }

        public void RemoveRange(int begin, int end)
        {
            for (int i = begin; i < end + 1; i++)
            {
                RemoveElementAt(i);
            }
        }
    }
}