using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules
{
    using System;
    using Core;
    using Core.Abstractions;

    /// <summary>
    /// 
    /// </summary>
    internal class Exponential : IRetry
    {
        private readonly Retriable _retriable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        internal Exponential(Retriable retriable)
        {
            _retriable = retriable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attempts"></param>
        /// <param name="timeBetweenRetries"></param>
        public void Attempt(Action action, int attempts, TimeSpan timeBetweenRetries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="attempts"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <returns></returns>
        public T Attempt<T>(Func<T> action, int attempts, TimeSpan timeBetweenRetries)
        {
            throw new NotImplementedException();
        }
    }
}