namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bytes2you.Validation;

    public class Retry : IRetry
    {
        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="tries"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int tries, TimeSpan timeBetweenRetries) => Do(action, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private static void Do(Action action, int tries, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(tries, timeBetweenRetries);

            var exceptions = new List<Exception>();
            while (tries > 0)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    if (--tries > 0)
                        Task.Delay(timeBetweenRetries).Wait();
                    else
                        throw new AggregateException(exceptions);
                }
            }
        }

        private static void ValidateArguments(int tries, TimeSpan timeBetweenRetries)
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
        public T Attempt<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries) => Do(function, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private static T Do<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries)
        {
            ValidateArguments(tries, timeBetweenRetries);

            var exceptions = new List<Exception>();
            while (tries > 0)
            {
                try
                {
                    var result = function();
                    return result;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    if (--tries > 0)
                        Task.Delay(timeBetweenRetries).Wait();
                    else
                        throw new AggregateException(exceptions);
                }
            }

            throw new InvalidOperationException("Fatal error in function retry. Reached unreachable code section.");
        }
    }
}
