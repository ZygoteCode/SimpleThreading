using System.Threading;

namespace SimpleThreading
{
    public class ThreadSafeCounter
    {
        private long _count;
        public long Value => Interlocked.Read(ref _count);

        public ThreadSafeCounter(long initialValue = 0)
        {
            _count = initialValue;
        }

        public long Increment() => Interlocked.Increment(ref _count);
        public long Decrement() => Interlocked.Decrement(ref _count);
        public long Add(long amount) => Interlocked.Add(ref _count, amount);
        public long Reset() => Interlocked.Exchange(ref _count, 0);
        public long Set(long newValue) => Interlocked.Exchange(ref _count, newValue);
    }
}