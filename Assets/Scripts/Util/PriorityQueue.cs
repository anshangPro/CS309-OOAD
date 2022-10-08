using System;
using System.Collections.Generic;

namespace Util
{
    public class PriorityQueue<T> where T : IComparable<T> {
        private readonly SortedList<T, int> _list = new();
        private int _count;

        public bool Contains(T item)
        {
            return _list.ContainsKey(item);
        }

        public void Add(T item) {
            if (_list.ContainsKey(item)) _list[item]++;
            else _list.Add(item, 1);

            _count++;
        }

        public T PopFirst() {
            if (Size() == 0) return default(T);
            T result = _list.Keys[0];
            if (--_list[result] == 0)
                _list.RemoveAt(0);

            _count--;
            return result;
        }

        public T PopLast() {
            if (Size() == 0) return default(T);
            int index = _list.Count - 1;
            T result = _list.Keys[index];
            if (--_list[result] == 0)
                _list.RemoveAt(index);

            _count--;
            return result;
        }

        public int Size() {
            return _count;
        }

        public T PeekFirst() {
            if (Size() == 0) return default(T);
            return _list.Keys[0];
        }

        public T PeekLast() {
            if (Size() == 0) return default(T);
            int index = _list.Count - 1;
            return _list.Keys[index];
        }
    }
}