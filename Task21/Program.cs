using System.ComponentModel.Design.Serialization;
using System.Runtime.Intrinsics.X86;
namespace BinarayTree
{
    public class MyComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
        {
            return x.CompareTo(y);
            throw new NotImplementedException();

        }
    }
    public class MyTreeMap<K, V> where K : IComparable
    {
        private class Node
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(K Key, V Value)
            {
                this.Key = Key;
                this.Value = Value;
                Left = null;
                Right = null;
            }
        }
        IComparer<K> comparator;
        Node? root;

        int size;
        public MyTreeMap()
        {
            comparator = new MyComparator<K>();
            size = 0;
        }
        public MyTreeMap(IComparer<K> comp)
        {
            comparator = comp;
        }
        public void Clear()
        {
            root = null;
            size = 0;
        }
        public bool ContainsKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0)
                    step = step.Left;
                else if (comparator.Compare(key, step.Key) > 0)
                    step = step.Right;
                else
                    return true;
            }
            return false;
        }
        public bool ContainsValue(V value)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (step.Value.Equals(value)) return true;
                    stack.Push(step);
                    step = step.Left;
                }
                step = stack.Pop();
                step = step.Right;
            }
            return false;
        }
        public List<KeyValuePair<K, V>> EntrySet()
        {
            List<KeyValuePair<K, V>> entries = new List<KeyValuePair<K, V>>();
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    entries.Add(new KeyValuePair<K, V>(step.Key, step.Value));
                    stack.Push(step);
                    step = step.Left;
                }
                step = stack.Pop();
                step = step.Right;
            }
            return entries;
        }
        public V Get(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0)
                    step = step.Left;
                else if (comparator.Compare(key, step.Key) > 0)
                    step = step.Right;
                else
                    return step.Value;
            }
            return default(V);
        }
        public bool IsEmpty() => size == 0;
        public K[] KeySet()
        {
            K[] set = new K[size];
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            int index = 0;
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    set[index] = step.Key;
                    stack.Push(step);
                    step = step.Left;
                    index++;
                }
                step = stack.Pop();
                step = step.Right;
            }
            return set;
        }
        public void Put(K key, V value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                size++;
                return;
            }
            Node newAdd = new Node(key, value);
            size++;
            Node step = root;
            while (step != null)
            {
                if (comparator.Compare(newAdd.Key, step.Key) < 0)
                {
                    if (step.Left == null) { step.Left = newAdd; break; }
                    else
                        step = step.Left;
                }
                else if (comparator.Compare(newAdd.Key, step.Key) > 0)
                {
                    if (step.Right == null) { step.Right = newAdd; break; }
                    else step = step.Right;
                }
                else
                {
                    step.Value = value;
                    size--;
                    return;
                }
            }
        }
        public void Remove(K key)
        {
            if (comparator.Compare(key, root.Key) == 0 && root.Right == null && root.Left == null)
            {
                root = null;
                size--;
                return;
            }
            Node high = root;
            Node step = root;
            if (comparator.Compare(key, root.Key) < 0)
                step = root.Left;
            else if (comparator.Compare(key, root.Key) > 0)
                step = root.Right;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) == 0)
                {
                    if (step.Left == null && step.Right == null)
                    {
                        if (comparator.Compare(step.Key, high.Key) < 0)
                        {
                            size--;
                            high.Left = null;
                            return;
                        }
                        else
                        {
                            size--;
                            high.Right = null;
                            return;
                        }
                    }
                    else if ((step.Left == null && step.Right != null) || (step.Right == null && step.Left != null))
                    {
                        if (step.Left != null)
                        {
                            step.Value = step.Left.Value;
                            step.Key = step.Left.Key;
                            step.Right = step.Left.Right;
                            step.Left = step.Left.Left;
                            size--;
                            return;
                        }
                        else if (step.Right != null)
                        {
                            step.Value = step.Right.Value;
                            step.Key = step.Right.Key;
                            step.Right = step.Right.Right;
                            step.Left = step.Right.Left;
                            size--;
                            return;
                        }
                    }
                    else if (step.Left != null && step.Right != null)
                    {
                        Node max = step.Left;
                        if (max.Right == null)
                            max = step.Left;
                        while (max.Right != null)
                            max = max.Right;
                        Node maxHigh = max;
                        if (maxHigh.Left != null)
                        {
                            step.Value = max.Value;
                            step.Key = max.Key;
                            maxHigh.Value = maxHigh.Left.Value;
                            maxHigh.Key = maxHigh.Left.Key;
                            maxHigh.Left = maxHigh.Left.Left;
                        }
                        else if (maxHigh.Left == null)
                        {
                            step.Value = max.Value;
                            step.Key = max.Key;
                            step.Left.Right = max.Left;
                        }
                        size--;
                        return;
                    }
                }
                else if (comparator.Compare(key, step.Key) < 0)
                {
                    high = step;
                    step = step.Left;
                }
                else if (comparator.Compare(key, step.Key) > 0)
                {
                    high = step;
                    step = step.Right;
                }
            }
        }
        public int Size() => size;
        public K FirstKey()
        {
            if (root != null)
                return root.Key;
            return default(K);
        }
        public K LastKey()
        {
            Node step = root;
            while (step != null)
            {
                if (step.Right == null)
                    return step.Key;
                step = step.Right;
            }
            return default(K);
        }
        public MyTreeMap<K, V> HeadMap(K end)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(step.Key, end) < 0)
                        returnTree.Put(step.Key, step.Value);
                    stack.Push(step);
                    step = step.Left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    if (comparator.Compare(step.Key, end) >= 0)
                        break;
                    step = step.Right;
                }
            }
            return returnTree;
        }
        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(step.Key, start) >= 0 && comparator.Compare(step.Key, end) < 0)
                        returnTree.Put(step.Key, step.Value);
                    stack.Push(step);
                    step = step.Left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.Right;
                }
            }
            return returnTree;
        }
        public MyTreeMap<K, V> TailMap(K start)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(step.Key, start) >= 0)
                        returnTree.Put(step.Key, step.Value);
                    stack.Push(step);
                    step = step.Left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.Right;
                }
            }
            return returnTree;
        }
        public IEnumerable<KeyValuePair<K, V>> FirstEntry()
        {
            yield return new KeyValuePair<K, V>(root.Key, root.Value);
        }
        public IEnumerable<KeyValuePair<K, V>> LastEntry()
        {
            Node step = root;
            while (step != null)
            {
                if (step.Right == null)
                    yield return new KeyValuePair<K, V>(root.Key, root.Value);
                step = step.Right;
            }
        }
        public IEnumerable<KeyValuePair<K, V>> LowerEntry(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0 || comparator.Compare(key, step.Key) == 0)
                    step = step.Left;
                else
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> FloorEntry(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0)
                    step = step.Left;
                else
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> HigherEntry(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0 || comparator.Compare(key, step.Key) == 0)
                    step = step.Right;
                else
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> CeilingEntry(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0)
                    step = step.Right;
                else
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
            }
        }
        public K LowerKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0 || comparator.Compare(key, step.Key) == 0)
                    step = step.Left;
                else
                    return step.Key;
            }
            return default(K);
        }
        public K FloorKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0)
                    step = step.Left;
                else
                    return step.Key;
            }
            return default(K);
        }
        public K HigherKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0 || comparator.Compare(key, step.Key) == 0)
                    step = step.Right;
                else
                    return step.Key;
            }
            return default(K);
        }
        public K CeilingKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0)
                    step = step.Right;
                else
                    return step.Key;
            }
            return default(K);
        }
        public K PollFirstEntry()
        {
            K key = root.Key;
            Remove(root.Key);
            return key;
        }
        public K PollLastEntry()
        {
            K key = LastKey();
            Remove(key);
            return key;
        }
        public override string ToString()
        {
            string res = "";
            Node treeElement = root;
            Print("", treeElement);
            void Print(string space, Node root)
            {
                if (root == null)
                    return;
                else
                {
                    Print(space + '\t', root.Left);
                    res += $"{space + root.Key}\n";
                    Print(space + '\t', root.Right);
                }
            }
            return res;
        }
        public void PrintTree()
        {

            Console.WriteLine(this);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            MyTreeMap<int, int> myTreeMap = new MyTreeMap<int, int>();
            myTreeMap.Put(15, 1);
            myTreeMap.Put(9, 2);
            myTreeMap.Put(25, 3);
            myTreeMap.Put(7, 4);
            myTreeMap.Put(13, 5);
            myTreeMap.Put(16, 6);
            myTreeMap.Put(18, 7);
            myTreeMap.Put(4, 8);
            myTreeMap.Put(7, 5);
            myTreeMap.Put(8, 9);
            myTreeMap.Put(3, 10);
            myTreeMap.Put(2, 11);
            myTreeMap.Put(1, 12);

            myTreeMap.PrintTree();
        }
    }
}