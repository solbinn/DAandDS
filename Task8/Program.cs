using VectorLibrary;
namespace Task8
{
    class MyStack<T> : MyVector<T>
    {
        MyVector<T> element;
        int top;
        public MyStack()
        {
            top = -1;
            element = new MyVector<T>();
        }
        public void Push(T item)
        {
            top++;
            element.Add(top, item);
        }
        public void Pop()
        {
            if (top == -1) throw new ArgumentOutOfRangeException("Stack is empty");
            element.Remove(top);
            top--;
        }
        public T Peek()
        {
            if (top == -1) throw new ArgumentOutOfRangeException("index");
            else return element.LastElement();
        }
        public bool Empty()
        {
            if (top == -1) return true;
            else return false;
        }
        public int Search(T item)
        {
            for (int i = top; i >= 0; i--)
            {
                if (element.Get(i).Equals(item))
                    return top - i + 1; 
            }
            return -1;
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < top+1; i++) s += element.Get(i) + " ";
            return s;
        }
        public void Print()
        {
            Console.WriteLine(this);
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();
            Console.WriteLine("проверка стека на пустоту:");
            Console.WriteLine(stack.Empty());
            stack.Clear();
            for (int i = 1; i <= 6; ++i)
                stack.Push(i);
            stack.Print();
            Console.WriteLine(stack.Search(1));
            stack.Pop();
            stack.Print();
            Console.WriteLine("проверка стека на пустоту:");
            Console.WriteLine(stack.Empty());
        }
    }
}
