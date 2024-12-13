
using System.Numerics;

namespace stack
{
    public class MyStack<T> : vector.MyVector<T>
    {
        private vector.MyVector<T> stack;
        private int size;
        public MyStack()
        {
            stack = new vector.MyVector<T>();
            size = -1;
        }
        public void Push(T item)
        {
            stack.Add(item);
            size++;
        }
        public T Pop()
        {
            T result = Peek();
            stack.Remove(size--);
            size--;
            return result;
        }

        public T Peek()
        {
            return Get(size--);
        }
        public bool Empty()
        {
            if (size == -1) return true;
            else return false;
        }
        public int Search(T item)
        {
            int n = stack.IndexOf(item);
            if (n > 0) return n;
            else return -1;

        }
    }
}