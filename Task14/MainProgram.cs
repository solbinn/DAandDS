using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DequeLib;

namespace Task14
{
    class Project
    {
        static void Main(string[] args)
        {
            MyArrayDeque<int> deque = new MyArrayDeque<int>();
            deque.Add(1);
            deque.Add(7);
            deque.Add(3);
            deque.Print();
            deque.RemoveLastOccurrence(7);
            deque.RemoveFirst();
            deque.RemoveFirst();
            deque.AddFirst(5);
            deque.AddFirst(6);
            deque.AddFirst(7);
            deque.AddFirst(8);
            deque.AddFirst(9);
            deque.Print();
        }
    }
}
