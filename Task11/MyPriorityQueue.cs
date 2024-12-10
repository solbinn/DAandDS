namespace MyPriorityQueue
{
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private T[] queue;
        private int size = 0;
        private Comparer<T> _comparer;

        public MyPriorityQueue()
        {
            queue = new T[11];
            _comparer = Comparer<T>.Default;
        }
        public MyPriorityQueue(T[] array)
        {
            queue = new T[array.Length];
            for (int i = 0; i < array.Length; i++) Add(array[i]);
            _comparer = Comparer<T>.Default;
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = new T[initialCapacity];
            _comparer = Comparer<T>.Default;
        }
        public MyPriorityQueue(int initialCapacity, Comparer<T> comparator)
        {
            queue = new T[initialCapacity];
            _comparer = comparator;
        }
        public MyPriorityQueue(MyPriorityQueue<T> array)
        {
            queue = new T[array.Size()];
            for (int i = 0; i < array.Size(); i++) Add(array.Get(i));
            _comparer = Comparer<T>.Default;
        }
        private void Swap(int firstIndex, int secondIndex)
        {
            T temp = queue[firstIndex];
            queue[firstIndex] = queue[secondIndex];
            queue[secondIndex] = temp;
        }
        public void Resize() => Array.Resize(ref queue, queue.Length < 64 ? queue.Length * 2 + 1 : queue.Length * 3 / 2 + 1);
        public void Add(T item)
        {
            if (size == queue.Length) Resize();
            queue[size++] = item;
            int i = size - 1;
            int parent = (i - 1) / 2;

            while (i > 0 && queue[parent].CompareTo(queue[i]) < 0)
            {
                Swap(i, parent);
                i = parent;
                parent = (i - 1) / 2;
            }
        }
        public void AddAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++) Add(array[i]);
        }
        public void Clear()
        {
            Array.Clear(queue, 0, size);
            size = 0;
        }
        public bool Contains(T obj) => IndexOf(obj) != -1;
        private int IndexOf(T value) => Array.IndexOf(queue, value);
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array) if (!Contains(item)) return false;
            return true;
        }
        public bool IsEmpty() => size == 0;
        public void Remove(T obj)
        {
            if (IsEmpty() || IndexOf(obj) == -1) return;
            RemoveByIndex(IndexOf(obj));
        }
        public void RemoveAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (IndexOf(array[i]) == -1) continue;
                RemoveByIndex(IndexOf(array[i]));
            }
        }
        public void RetainAll(T[]? array)
        {
            if (array == null) throw new ArgumentNullException();
            bool waste = true;
            for (int i = 0; i < queue.Length; i++)
            {
                foreach (T item in array) { if (queue[i].Equals(item)) waste = false; break; }
                if (waste)
                {
                    RemoveByIndex(i);
                    i--;
                }
            }

        }
        public T[] ToArray()
        {
            T[] newQueue = new T[size];
            Array.Copy(queue, newQueue, size);
            return newQueue;
        }
        public void ToArray(T[]? array)
        {
            if (array == null) throw new ArgumentNullException();
            if (array.Length < size) throw new ArgumentException();
            Array.Copy(queue, array, size);
        }
        public T Element() => queue[0];
        public bool Offer(T obj)
        {
            Add(obj);
            return true;
        }
        public T? Peek()
        {
            if (IsEmpty()) return default;
            return queue[0];
        }
        public T? Poll()
        {
            if (IsEmpty()) return default;
            T top = queue[0];
            RemoveByIndex(0);
            return top;
        }
        private void Heapify(int i)
        {
            int leftChild;
            int rightChild;
            int largestChild;
            while (true)
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                largestChild = i;
                if (leftChild < size && queue[leftChild].CompareTo(queue[largestChild]) > 0) largestChild = leftChild;
                if (rightChild < size && queue[rightChild].CompareTo(queue[largestChild]) > 0) largestChild = rightChild;
                if (largestChild == i) break;
                Swap(i, largestChild);
                i = largestChild;
            }
        }
        private void RemoveByIndex(int index)
        {
            if (index < 0 || index > size) throw new Exception("Please, enter correct index");
            if (IsEmpty()) throw new Exception("Queue is Empty");
            if (size == 1)
            {
                size--;
                Array.Resize(ref queue, size);
                return;
            }
            Swap(index, size - 1);
            size--;
            Array.Resize(ref queue, size);
            for (int i = size / 2; i >= 0; i--) Heapify(i);
        }
        public int Size() => size;
        public void Print()
        {
            for (int i = 0; i < queue.Length; i++) Console.Write(queue[i] + "\n");
        }
        public T Get(int index) => queue[index];
    }
}