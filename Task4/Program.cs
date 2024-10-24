using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{

    public class MyArrayList<T>
    {

        private int size;
        private T[] elementData;

        public MyArrayList()   //пустой список
        {
            elementData = new T[0];
        }

        public MyArrayList(T[] a)
        {
            elementData = new T[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
            size = a.Length;
        }

        public MyArrayList(int capacity) //создание массива размера 0 с запасом на какое-то количество эл, для 
        {
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T e)
        {
            if (size < elementData.Length)
            {
                elementData[size] = e;
                size++;
            }
            else
            {
                var newData = new T[elementData.Length / 2 * 3]; //чтобы избежать выхода за границы типа
                for (int j = 0; j < elementData.Length; j++)
                {
                    newData[j] = elementData[j];
                }
                newData[elementData.Length] = e;
                size++;
                elementData = newData;
            }
        }
        public void AddAll(T[] a)
        {
            if (a.Length <= elementData.Length - size)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    elementData[size + i] = a[i];
                }
                size += a.Length;
            }
            else
            {
                var newData = new T[a.Length + size];
                for (int i = 0; i < size; i++)
                {
                    newData[i] = elementData[i];
                }
                for (int i = 0; i < a.Length; i++)
                {
                    newData[i + size] = a[i];
                }
                size += a.Length;
                elementData = newData;
            }
        }
        public void Clear() //буфер не трогать
        {
            size = 0;
        }

        public bool Contains(object o)
        {
            if ((o is T) == false) return false;
            var x = (T)o;
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(x)) return true;
            }
            return false;
        }

        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!Contains(a[i])) return false;
            }
            return true;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }
        public void Remove(object o)
        {
            if ((o is T) == false) return;
            var x = (T)o;
            int y = 0;
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(x)) y++;
            }
            int k = 0; //хорошие элементы
            for (int l = 0; l < size; l++)
            {
                if (elementData[l].Equals(x)) continue;
                elementData[k] = elementData[l];
                k++;
            }
            size = k;
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Remove(a[i]);
            }
        }

        public void RetainAll(T[] a)
        {
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                bool found = false;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j].Equals(elementData[i]))
                    {
                        found = true;
                        break;
                    }

                }
                if (found)
                {
                    k++;
                }
            }
        }
        public int Size()
        {
            return size;
        }

        public T[] ToArray()
        {
            T[] returnedArray = new T[size];
            for (int i = 0; i < size; i++)
            {
                returnedArray[i] = elementData[i];
            }
            return returnedArray;
        }

        public T[] ToArray(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = elementData[i];
            }
            return a;
        }

        public void Resize()
        {
            T[] newArray = new T[elementData.Length * (3 / 2) + 1];
            for (int i = 0; i < elementData.Length; i++) newArray[i] = elementData[i];
            Array.Clear(elementData);
            elementData = newArray;

        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            if (size == elementData.Length) Resize();
            for (int i = size - 1; i >= index; i--) elementData[i + 1] = elementData[i];
            elementData[index] = e;
            size++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            int j = 0;
            while (size + a.Length > elementData.Length) Resize();
            for (int i = size - 1; i >= index; i--) elementData[i + a.Length] = elementData[i];
            for (int i = index; i < index + a.Length; i++) elementData[i] = a[j++];
            size += a.Length;
        }

        public T Get(int index)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index is out of range");
            }
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(elementData[i])) return i;
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (o.Equals(elementData[i])) return i;
            }
            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
            return element;
        }

        public void Set(int index, T e)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }

        public MyArrayList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex > size) throw new ArgumentOutOfRangeException("index");
            if (toIndex < 0 || toIndex > size) throw new ArgumentOutOfRangeException("index");
            MyArrayList<T> resultingArray = new MyArrayList<T>(toIndex - fromIndex);
            for (int i = 0; i < resultingArray.size; i++)
            {
                resultingArray.Set(i, elementData[fromIndex + i]);
            }
            return resultingArray;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < size; i++) s += elementData[i] + " ";
            return s;
        }
        public void Print()
        {
            Console.WriteLine(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };
            int[] array2 = { 3, 4, 6, 9 };
            MyArrayList<int> array = new MyArrayList<int>(array1);
            array.Print();
            array.AddAll(3, array2); //можем применить в этой строке любой из реализованных методов, например воспользуемся методом AddAll
            array.Print();
        }
    }
}