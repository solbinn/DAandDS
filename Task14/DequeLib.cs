namespace DequeLib
{
    public class MyArrayDeque<T> where T : IComparable
    {
        T[] elements = new T[1];
        int head = 0;
        int tail = -1;
        public MyArrayDeque() => elements = new T[16];
        public MyArrayDeque(T[] array)
        {
            elements = array;
            tail = array.Length;
        }
        public MyArrayDeque(int numElements)
        {
            if (numElements != 0) elements = new T[numElements];
        }
        public void Add(T el)
        {
            if (tail == elements.Length - 1) Resize();
            elements[++tail] = el;
        }
        private void Resize()
        {
            T[] newArray;
            if (elements.Length != 0) newArray = new T[elements.Length * 2];
            else newArray = new T[1];
            Array.Copy(elements, newArray, elements.Length);
            Array.Clear(elements);
            elements = newArray;

        }
        public void AddAll(T[] array)
        {
            while (tail + array.Length > elements.Length) Resize();
            for (int i = 0; i < array.Length; i++) elements[++tail] = array[i];
        }
        public void Clear()
        {
            Array.Clear(elements);
            head = 0; tail = -1;
        }
        public bool IsEmpty() => head == tail - 1 ? true : false;
        public bool Contains(T el)
        {
            for (int i = head; i <= tail; i++) if (elements[i].CompareTo(el) == 0) return true;
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++) if (!Contains(array[i])) return false;
            return true;
        }
        public void Remove(T el)
        {
            for (int i = head; i <= tail; i++)
                if (elements[i].CompareTo(el) == 0)
                {
                    for (int j = i; j <= tail; j++) elements[j] = elements[j + 1];
                    tail--;
                    return;
                }
        }
        public void RemoveAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++) Remove(array[i]);
        }
        public void RetainAll(T[] array)
        {
            bool flag = false;
            for (int i = head; i <= tail; i++)
            {
                for (int j = 0; j < array.Length; j++)
                    if (elements[i].CompareTo(array[j]) == 0)
                    {
                        flag = true;
                        break;
                    }
                if (!flag) Remove(elements[i]);
                flag = false;
            }
        }
        public int Size() => tail - head + 1;
        public T[] ToArray()
        {
            int index = 0;
            T[] array = new T[Size()];
            for (int i = head; i <= tail; i++) array[index++] = elements[i];
            return array;
        }
        public T[] ToArray(T[] array)
        {
            if (array == null) return ToArray();
            array = ToArray();
            return array;
        }
        public T Element() => elements[head];
        public bool Offer(T el)
        {
            Add(el);
            if (Contains(el)) return true;
            else return false;
        }
        public T? Peek()
        {
            if (IsEmpty()) return default;
            return elements[head];
        }
        public T? Poll()
        {
            if (IsEmpty()) return default;
            return elements[head++];
        }
        public void AddFirst(T el)
        {
            if (head == 0) ShiftDeque();
            elements[--head] = el;
        }
        private void ShiftDeque()
        {
            T[] array = new T[elements.Length + 1];
            int index = 1;
            for (int i = head; i <= tail; i++) array[index++] = elements[i];
            tail++; head++;
            Array.Clear(elements);
            elements = array;
        }
        public void AddLast(T el) => Add(el);
        public T GetFirst() => Element();
        public T GetLast() => elements[tail];
        public bool OfferFirst(T el)
        {
            AddFirst(el);
            if (Contains(el)) return true;
            return false;
        }
        public bool OfferLast(T el) => Offer(el);
        public T Pop() => elements[head++];
        public void Push(T el) => AddFirst(el);
        public T? PeekFirst() => Peek();
        public T? PeekLast()
        {
            if (IsEmpty()) return default;
            return elements[tail];
        }
        public T? PollFist() => Poll();
        public T? PollLast()
        {
            if (IsEmpty()) return default;
            return elements[tail--];
        }
        public T RemoveLast() => elements[tail--];
        public T RemoveFirst() => elements[head++];
        public bool RemoveLastOccurrence(T el)
        {
            for (int i = tail; i >= head; i--)
            {
                if (el.CompareTo(elements[i]) == 0)
                {
                    for (int j = i; j <= tail; j++) elements[j] = elements[j + 1];
                    tail--;
                    return true;
                }
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T el)
        {
            for (int i = head; i <= tail; i++)
            {
                if (el.CompareTo(elements[i]) == 0)
                {
                    for (int j = i; j <= tail; j++) elements[j] = elements[j + 1];
                    tail--;
                    return true;
                }
            }
            return false;
        }
        public void Print()
        {
            for (int i = head; i <= tail; i++) Console.WriteLine(elements[i]);
            Console.WriteLine();
        }
    }
}
