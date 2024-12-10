using MyPriorityQueue;

MyPriorityQueue<int> array = new MyPriorityQueue<int>(10);
int[] mass = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
array.AddAll(mass);
int jjj = array.Poll();
array.Print();