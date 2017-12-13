using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry.Rules
{
    using System;
    using Events;

    internal class Exponential : IRetry
    {
        private readonly Retriable _retriable;

        internal Exponential(Retriable retriable)
        {
            _retriable = retriable;
        }

        public void Attempt(Action action, int tries, TimeSpan timeBetweenRetries)
        {
            throw new NotImplementedException();
        }

        public T Attempt<T>(Func<T> action, int times, TimeSpan timeBetweenRetries)
        {
            throw new NotImplementedException();
        }
    }
}