using System.Collections.Generic;

namespace Domain.Infrastructure.Helpers
{
    public class UniqueQueue<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _hashSet;
        private readonly Queue<T> _queue;


        public UniqueQueue()
        {
            _hashSet = new HashSet<T>();
            _queue = new Queue<T>();
        }


        public int Count => _hashSet.Count;

        public void Clear()
        {
            _hashSet.Clear();
            _queue.Clear();
        }


        public bool Contains(T item)
        {
            return _hashSet.Contains(item);
        }


        public void Enqueue(T item)
        {
            if (_hashSet.Add(item))
            {
                _queue.Enqueue(item);
            }
        }

        public T Dequeue()
        {
            T item = _queue.Dequeue();
            _hashSet.Remove(item);
            return item;
        }


        public T Peek()
        {
            return _queue.Peek();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _queue.GetEnumerator();
        }
    }
}