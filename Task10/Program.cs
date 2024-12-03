using MyListLib;

namespace MyHeapLib
{
    public class Heap<T> where T : IComparable<T>
    {
        private MyArrayList<T> items = new MyArrayList<T>();

        public void Swap(int indexA, int indexB)
        {
            T temp = items.Get(indexA);
            items.Set(indexA, items.Get(indexB));
            items.Set(indexB, temp);
        }
        public Heap(T[] initialArray)
        {
            items.AddAll(initialArray);
            for (int i = items.Size() / 2; i >= 0; i--) Heapify(i);
        }
        public void Add(T element)
        {
            items.Add(element);
            int currentIndex = items.Size() - 1;
            int parentIndex = (currentIndex - 1) / 2;

            while (currentIndex > 0 && items.Get(parentIndex).CompareTo(items.Get(currentIndex)) < 0)
            {
                Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }
        public void Heapify(int index)
        {
            int leftChildIndex;
            int rightChildIndex;
            int largestIndex;

            while (true)
            {
                leftChildIndex = 2 * index + 1;
                rightChildIndex = 2 * index + 2;
                largestIndex = index;
                if (leftChildIndex < items.Size() && items.Get(leftChildIndex).CompareTo(items.Get(largestIndex)) > 0) largestIndex = leftChildIndex;
                if (rightChildIndex < items.Size() && items.Get(rightChildIndex).CompareTo(items.Get(largestIndex)) > 0) largestIndex = rightChildIndex;
                if (largestIndex == index) break;
                Swap(index, largestIndex);
                index = largestIndex;
            }
        }
        public T GetMax() => items.Get(items.Size() - 1);
        public T RemoveMax()
        {
            if (items.IsEmpty()) throw new Exception("Heap is empty");
            T maxElement = items.Get(0);
            if (items.Size() == 1)
            {
                items.RemoveByIndex(items.Size() - 1);
                return maxElement;
            }
            Swap(0, items.Size() - 1);
            items.RemoveByIndex(items.Size() - 1);
            Heapify(0);
            return maxElement;
        }
        public void IncreaseKey(int index, T newValue)
        {
            if (index > items.Size() - 1) throw new IndexOutOfRangeException();
            items.Set(index, newValue);
            for (int i = items.Size() / 2; i >= 0; i--) Heapify(i);
        }
        public int Size() => items.Size();

        public void Merge(Heap<T> otherHeap)
        {
            while (otherHeap.Size() != 0)
            {
                T element = otherHeap.RemoveMax();
                Add(element);
            }
        }
        public void Print()
        {
            for (int i = 0; i < items.Size(); i++) Console.Write(items.Get(i) + "\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] firstArray = { 2, 10, 4, 11, 6, 9 };
            int[] secondArray = { 14, 57, 89 };
            Heap<int> firstHeap = new Heap<int>(firstArray);
            Heap<int> secondHeap = new Heap<int>(secondArray);
            firstHeap.Merge(secondHeap);
            firstHeap.Print();
        }
    }
}
