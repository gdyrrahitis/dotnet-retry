using System;
using System.Threading.Tasks;

namespace DotNetRetry
{
    public interface IRetry
    {
        void Attempt(Action action, int times, TimeSpan timeBetweenRetries);

        void Attempt<T>(Action<T> action, int times, TimeSpan timeBetweenRetries);

        T Attempt<T>(Func<T> action, int times, TimeSpan timeBetweenRetries);

        void AttemptAsync(Func<Task> action, int times, TimeSpan timeBetweenRetries);

        T AttemptAsync<T>(Func<Task<T>> action, int times, TimeSpan timeBetweenRetries);
    }
}