using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Abstractions;
    using Templates;
    using static Core.Auxiliery.Guards;

    /// <summary>
    /// 
    /// </summary>
    internal class Sequential: IRetry
    {
        private readonly Retriable _retriable;
        private readonly ActionBody _actionBody;
        private readonly FunctionBody _functionBody;

        internal Sequential(Retriable retriable)
        {
            _retriable = retriable;
            _actionBody = new ActionBody(retriable);
            _functionBody = new FunctionBody(retriable);
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int attempts, TimeSpan timeBetweenRetries) => Do(() =>
        {
            action();
            _retriable.OnAfterRetryInvocation();
        }, attempts, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private void Do(Action action, int attempts, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(attempts, timeBetweenRetries);

            var exceptions = new List<Exception>();
            var time = TimeSpan.Zero;

            _actionBody.Attempt(action, ref attempts, timeBetweenRetries, exceptions, time);
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int attempts, TimeSpan timeBetweenRetries) =>
            Do(() =>
            {
                var result = function();
                _retriable.OnAfterRetryInvocation();
                return result;
            }, attempts, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them.
        /// </summary>
        private T Do<T>(Func<T> function, int attempts, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(attempts, timeBetweenRetries);

            var exceptions = new List<Exception>();
            var time = TimeSpan.Zero;

            return _functionBody.Attempt(function, ref attempts, timeBetweenRetries, exceptions, time);
        }
    }
}
