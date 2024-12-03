using MyVectorLib;

namespace MyStackLib
{
    public class MyStack<T> : MyVector<T>
    {
        public MyStack() : base(1, 5) { }

        public void Push(T element) => Add(element);

        public T Pop()
        {
            T element = RemoveByIndex(Size() - 1);
            return element;
        }
        public T Peek() => LastElement();

        public int Search(T item)
        {
            return Size() - IndexOf(item);
        }

    }
}