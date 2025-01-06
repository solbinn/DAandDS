using HashMap;

namespace HashSet

{
    public class MyHashSet<T> where T : IComparable
    {
        MyHashMap<T, object> map;
        public MyHashSet()
        {
            map = new MyHashMap<T, object>();
        }
        public MyHashSet(T[] a)
        {
            map = new MyHashMap<T, object>();
            foreach (T obj in a)
                map.Put(obj, true);
        }
        public MyHashSet(int initialCapacity, float loadFactor)
        {
            map = new MyHashMap<T, object>(initialCapacity, loadFactor);
        }
        public MyHashSet(int initialCapacity)
        {
            map = new MyHashMap<T, object>(initialCapacity);
        }
        public void Add(T obj)
        {
            map.Put(obj, true);
        }
        public void addAll(T[] a)
        {
            foreach (T obj in a)
                map.Put(obj, true);
        }
        public void Clear() => map.Clear();
        public bool Contains(T obj) => map.ContainsKey(obj);
        public bool ContainsAll(T[] a)
        {
            foreach (T obj in a)
                if (!map.ContainsKey(obj)) return false;
            return true;
        }
        public bool IsEmpty() => map.IsEmpty();
        public void Remove(T obj) => map.Remove(obj);
        public void Remove(T[] a)
        {
            foreach (T obj in a) map.Remove(obj);
        }
        public void RetainAll(T[] a)
        {
            T[] g = map.KeySet();
            foreach (T obj in g)
                if (!a.Contains<T>(obj)) map.Remove(obj);
        }
        public void Size() => map.Size();
        public T[] ToArray()
        {
            T[] el = map.KeySet();
            return el;
        }
        public T[] ToArray(T[] a)
        {
            if (a == null)
                ToArray();
            else
            {
                T[] el = new T[a.Length + map.Size()];
                int i = 0;
                foreach (T obj in a)
                {
                    el[i] = obj;
                    i++;
                }
                T[] tw = map.KeySet();
                T[] ret = el.Concat(tw).ToArray();
                return ret;
            }
            return ToArray();
        }
        public T First()
        {
            T[] arr = map.KeySet();
            T min = arr[0];
            foreach (T obj in arr)
            {
                if (obj.CompareTo(min) < 0)
                    min = obj;
            }
            return min;
        }
        public T Last()
        {
            T[] arr = map.KeySet();
            T max = arr[0];
            foreach (T obj in arr)
            {
                if (obj.CompareTo(max) > 0)
                    max = obj;
            }
            return max;
        }
        public MyHashSet<T> SubSet(T start, T end)
        {
            MyHashSet<T> ret = new MyHashSet<T>();
            T[] arr = map.KeySet();
            foreach (T obj in arr)
                if ((obj.CompareTo(start) == 0 || obj.CompareTo(start) > 0) && obj.CompareTo(end) < 0) ret.Add(obj);
            return ret;
        }
        public MyHashSet<T> TailSet(T start, T end)
        {
            MyHashSet<T> ret = new MyHashSet<T>();
            T[] arr = map.KeySet();
            foreach (T obj in arr)
                if (obj.CompareTo(start) > 0) ;
            return ret;
        }
        public MyHashSet<T> HeadSet(T start)
        {
            MyHashSet<T> ret = new MyHashSet<T>();
            T[] arr = map.KeySet();
            foreach (T obj in arr)
                if (obj.CompareTo(start) < 0) ret.Add(obj);
            return ret;
        }
    }
}
