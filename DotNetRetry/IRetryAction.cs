namespace DotNetRetry
{
    using System;

    public interface IRetryAction
    {
        void Attempt(Action action, int tries, TimeSpan timeBetweenRetries);
    }
}