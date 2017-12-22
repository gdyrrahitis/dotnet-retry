using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Abstractions;
    using static Core.Auxiliery.Constants;
    using static Core.Auxiliery.Guards;

    internal class Sequential: IRetry
    {
        private readonly Retriable _retriable;
        private readonly ActionPolicy _actionPolicy;
        private readonly FunctionPolicy _functionPolicy;

        internal Sequential(Retriable retriable)
        {
            _retriable = retriable;
            _actionPolicy = new ActionPolicy(retriable);
            _functionPolicy = new FunctionPolicy(retriable);
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
            _actionPolicy.Attempt(action, ref attempts, timeBetweenRetries, exceptions, time);

            //while (attempts > 0)
            //{
            //_retriable.OnBeforeRetryInvocation();
            //try
            //{
            //    action();
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    _retriable.OnFailureInvocation();
            //    exceptions.Add(ex);

            //    if (_retriable.CancellationRule != null && _retriable.CancellationRule.IsIn(ex))
            //    {
            //        _retriable.OnAfterRetryInvocation();
            //        ThrowFlattenAggregateException(exceptions);
            //    }

            //    if (--attempts > 0)
            //    {
            //        Task.Delay(timeBetweenRetries).Wait();
            //        time = time.Add(timeBetweenRetries);
            //    }
            //    else
            //    {
            //        ThrowFlattenAggregateException(exceptions);
            //    }

            //    if (_retriable.CancellationRule != null && _retriable.CancellationRule.HasExceededMaxTime(time))
            //    {
            //        _retriable.OnAfterRetryInvocation();
            //        ThrowFlattenAggregateException(exceptions);
            //    }
            //}
            //_retriable.OnAfterRetryInvocation();
            //}
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

            return _functionPolicy.Attempt(function, ref attempts, timeBetweenRetries, exceptions, time);
        }
    }
}
