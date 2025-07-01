using System;
using System.Threading;

namespace SimpleThreading
{
    public static class ResourceUtils
    {
        public static int Increment(ref int value) => Interlocked.Increment(ref value);

        public static int Decrement(ref int value) => Interlocked.Decrement(ref value);

        public static int Add(ref int value, int amount) => Interlocked.Add(ref value, amount);

        public static int Exchange(ref int target, int newValue) => Interlocked.Exchange(ref target, newValue);

        public static int CompareExchange(ref int target, int newValue, int comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref int target, int newValue, int expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static long Increment(ref long value) => Interlocked.Increment(ref value);

        public static long Decrement(ref long value) => Interlocked.Decrement(ref value);

        public static long Add(ref long value, long amount) => Interlocked.Add(ref value, amount);

        public static long Exchange(ref long target, long newValue) => Interlocked.Exchange(ref target, newValue);

        public static long CompareExchange(ref long target, long newValue, long comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref long target, long newValue, long expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static float Increment(ref float value)
        {
            float currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal + 1.0f;
            }
            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static float Decrement(ref float value)
        {
            float currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal - 1.0f;
            }
            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static float Add(ref float value, float amount)
        {
            float currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal + amount;
            }
            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static float Exchange(ref float target, float newValue) => Interlocked.Exchange(ref target, newValue);

        public static float CompareExchange(ref float target, float newValue, float comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref float target, float newValue, float expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static double Increment(ref double value)
        {
            double currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal + 1.0;
            }
            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static double Decrement(ref double value)
        {
            double currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal - 1.0;
            }
            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static double Add(ref double value, double amount)
        {
            double currentVal, newVal;

            do
            {
                currentVal = value;
                newVal = currentVal + amount;
            }

            while (Interlocked.CompareExchange(ref value, newVal, currentVal) != currentVal);

            return newVal;
        }

        public static double Exchange(ref double target, double newValue) => Interlocked.Exchange(ref target, newValue);

        public static double CompareExchange(ref double target, double newValue, double comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref double target, double newValue, double expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static string Exchange(ref string target, string newValue) => Interlocked.Exchange(ref target, newValue);

        public static string CompareExchange(ref string target, string newValue, string comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref string target, string newValue, string expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static T Exchange<T>(ref T target, T newValue) where T : class => Interlocked.Exchange(ref target, newValue);

        public static T CompareExchange<T>(ref T target, T newValue, T comparand) where T : class
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet<T>(ref T target, T newValue, T expectedValue) where T : class
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static byte[] Exchange(ref byte[] target, byte[] newValue) => Interlocked.Exchange(ref target, newValue);

        public static byte[] CompareExchange(ref byte[] target, byte[] newValue, byte[] comparand)
            => Interlocked.CompareExchange(ref target, newValue, comparand);

        public static bool TrySet(ref byte[] target, byte[] newValue, byte[] expectedValue)
        {
            return Interlocked.CompareExchange(ref target, newValue, expectedValue) == expectedValue;
        }

        public static void ExecuteLocked(object lockObject, Action action)
        {
            lock (lockObject)
            {
                action();
            }
        }

        public static T ExecuteLocked<T>(object lockObject, Func<T> func)
        {
            lock (lockObject)
            {
                return func();
            }
        }

        public static bool SpinUntil(Func<bool> condition, int timeoutMilliseconds = -1)
        {
            var spinner = new SpinWait();
            long startTime = timeoutMilliseconds == -1 ? 0 : Environment.TickCount;

            while (!condition())
            {
                if (timeoutMilliseconds != -1 && Environment.TickCount - startTime > timeoutMilliseconds)
                {
                    return false;
                }

                spinner.SpinOnce();
            }
            return true;
        }

        public static void ExecuteOnce(Action initializer, ref bool initialized, object lockObject)
        {
            if (!initialized)
            {
                lock (lockObject)
                {
                    if (!initialized)
                    {
                        initializer();
                        initialized = true;
                    }
                }
            }
        }
    }
}