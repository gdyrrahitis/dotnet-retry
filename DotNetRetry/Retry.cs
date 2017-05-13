namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Retry : IRetry
    {
        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public void Attempt(Action action, int tries, TimeSpan timeBetweenRetries) => Do(action, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private static void Do(Action action, int tries, TimeSpan timeBetweenRetries)
        {
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

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries) => Do(function, tries, timeBetweenRetries);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them
        /// </summary>
        private static T Do<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries)
        {
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
