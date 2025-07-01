namespace SimpleThreading
{
    public class ThreadSafeQueue<T>
    {
        private readonly System.Collections.Concurrent.ConcurrentQueue<T> _queue = new System.Collections.Concurrent.ConcurrentQueue<T>();

        public void Enqueue(T item) => _queue.Enqueue(item);
        public bool TryDequeue(out T item) => _queue.TryDequeue(out item);
        public int Count => _queue.Count;
        public bool IsEmpty => _queue.IsEmpty;
    }
}