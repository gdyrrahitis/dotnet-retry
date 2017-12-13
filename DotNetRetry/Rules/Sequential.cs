using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bytes2you.Validation;
    using Events;

    internal class Sequential: IRetry
    {
        private const string InvalidOperationExceptionErrorMessage = "Fatal error in function retry. Reached unreachable code section.";

        private readonly Retriable _retriable;

        internal Sequential(Retriable retriable)
        {
            _retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="tries"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int tries, TimeSpan timeBetweenRetries) => Do(() =>
        {
            action();
            _retriable.OnAfterRetryInvocation();
        }, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private void Do(Action action, int tries, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(tries, timeBetweenRetries);

            var exceptions = new List<Exception>();

            while (tries > 0)
            {
                _retriable.OnBeforeRetryInvocation();
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    _retriable.OnFailureInvocation();
                    exceptions.Add(ex);
                    if (--tries > 0)
                        Task.Delay(timeBetweenRetries).Wait();
                    else
                        ThrowFlattenAggregateException(exceptions);
                }
                _retriable.OnAfterRetryInvocation();
            }
        }

        /// <summary>
        /// Throws a flatten aggregate exception to the caller.
        /// </summary>
        /// <param name="exceptions">An <see cref="IEnumerable{T}"/> of <see cref="Exception"/> objects.</param>
        /// <remarks>
        /// For more info on Flatten method see 
        /// https://msdn.microsoft.com/en-us/library/system.aggregateexception.flatten(v=vs.110).aspx
        /// </remarks>
        private static void ThrowFlattenAggregateException(IEnumerable<Exception> exceptions)
        {
            var aggregateException = new AggregateException(exceptions);
            throw aggregateException.Flatten();
        }

        /// <summary>
        /// Validates arguments <paramref name="tries"/> and <paramref name="timeBetweenRetries"/>.
        /// </summary>
        /// <param name="tries">Number of tries.</param>
        /// <param name="timeBetweenRetries">Time to wait between retries.</param>
        private void ValidateArguments(int tries, TimeSpan timeBetweenRetries)
        {
            Guard.WhenArgument(tries, nameof(tries)).IsLessThan(1).Throw();
            Guard.WhenArgument(timeBetweenRetries, nameof(timeBetweenRetries))
                .IsEqual(TimeSpan.Zero)
                .IsEqual(TimeSpan.MinValue)
                .Throw();
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="tries"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries) =>
            Do(() =>
            {
                var result = function();
                _retriable.OnAfterRetryInvocation();
                return result;
            }, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them.
        /// </summary>
        private T Do<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(tries, timeBetweenRetries);

            var exceptions = new List<Exception>();
            while (tries > 0)
            {
                _retriable.OnBeforeRetryInvocation();
                try
                {
                    var result = function();
                    return result;
                }
                catch (Exception ex)
                {
                    _retriable.OnFailureInvocation();
                    exceptions.Add(ex);
                    if (--tries > 0)
                        Task.Delay(timeBetweenRetries).Wait();
                    else
                        ThrowFlattenAggregateException(exceptions);
                }
                _retriable.OnAfterRetryInvocation();
            }

            throw new InvalidOperationException(InvalidOperationExceptionErrorMessage);
        }
    }
}
