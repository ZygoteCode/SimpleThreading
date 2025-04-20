using System;
using System.Threading;

namespace SimpleThreading.Semaphores
{
    public class ResourceSemaphore : IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private bool _disposed = false;

        public ResourceSemaphore(int maxResources = 1)
        {
            _semaphore = new SemaphoreSlim(maxResources, maxResources);
        }

        public bool LockResource(int timeoutMilliseconds = Timeout.Infinite)
        {
            return _semaphore.Wait(timeoutMilliseconds);
        }

        public void UnlockResource()
        {
            _semaphore.Release();
        }

        public bool IsResourceAvailable() => _semaphore.CurrentCount > 0;

        public void Dispose()
        {
            if (!_disposed)
            {
                _semaphore.Dispose();
                _disposed = true;
            }
        }
    }
}