using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleThreading.Semaphores
{
    public class ResourceFairSemaphore : IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentQueue<TaskCompletionSource<bool>> _queue = new ConcurrentQueue<TaskCompletionSource<bool>>();
        private bool _disposed = false;

        public ResourceFairSemaphore(int maxResources = 1)
        {
            _semaphore = new SemaphoreSlim(maxResources, maxResources);
        }

        public async Task<bool> LockResourceAsync(int timeoutMilliseconds = Timeout.Infinite)
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            _queue.Enqueue(tcs);

            if (_queue.TryPeek(out var first) && first == tcs)
            {
                if (await _semaphore.WaitAsync(timeoutMilliseconds))
                {
                    _queue.TryDequeue(out _);
                    return true;
                }
                else
                {
                    _queue.TryDequeue(out _);
                    return false;
                }
            }
            return false;
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
