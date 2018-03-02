namespace DotNetRetry
{
    using System;

    public interface IRetryFunction
    {
        T Attempt<T>(Func<T> action, int times, TimeSpan timeBetweenRetries);
    }
}