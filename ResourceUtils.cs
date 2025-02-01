using System.Threading;

namespace SimpleThreading
{
    public class ResourceUtils
    {
        public static int Increment(ref int value) => Interlocked.Increment(ref value);

        public static int Decrement(ref int value) => Interlocked.Decrement(ref value);

        public static int Add(ref int value, int amount) => Interlocked.Add(ref value, amount);

        public static int Exchange(ref int target, int newValue) => Interlocked.Exchange(ref target, newValue);

        public static int CompareExchange(ref int target, int newValue, int comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static long Increment(ref long value) => Interlocked.Increment(ref value);

        public static long Decrement(ref long value) => Interlocked.Decrement(ref value);

        public static long Add(ref long value, long amount) => Interlocked.Add(ref value, amount);

        public static long Exchange(ref long target, long newValue) => Interlocked.Exchange(ref target, newValue);

        public static long CompareExchange(ref long target, long newValue, long comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref int target, int newValue, int expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static bool TrySet(ref long target, long newValue, long expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }
    }
}