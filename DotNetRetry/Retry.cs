using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetRetry
{
    public class Retry : IRetry
    {
        #region Public API
        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The returnless action to test</param>
        /// <param name="times">Retry count</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        public void Attempt(Action action, int times, TimeSpan timeBetweenRetries)
        {
            // Initialize retry stack
            var retryStack = new Stack<Action>(times);
            for (var i = 0; i < times; i++)
                retryStack.Push(action);
            // Test the attempted action
            TryPop(retryStack, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an action of type T.
        /// </summary>
        /// <param name="action">The returnless action to test</param>
        /// <param name="times">Retry count</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        public void Attempt<T>(Action<T> action, int times, TimeSpan timeBetweenRetries)
        {
            // Initialize retry stack
            var retryStack = new Stack<Action<T>>(times);
            for (var i = 0; i < times; i++)
                retryStack.Push(action);
            // Test the attempted action
            TryPop(retryStack, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an Func with a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="action">The action that's tested</param>
        /// <param name="times">Retry count</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <returns>The action return value</returns>
        public T Attempt<T>(Func<T> action, int times, TimeSpan timeBetweenRetries)
        {
            // Initialize retry stack
            var retryStack = new Stack<Func<T>>(times);
            for (var i = 0; i < times; i++)
                retryStack.Push(action);
            // Test the attempted action
            return TryPop(retryStack, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an asynchronous Func.
        /// </summary>
        /// <param name="action">A Task of a Func that's tested</param>
        /// <param name="times">>Retry count</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        public void AttemptAsync(Func<Task> action, int times, TimeSpan timeBetweenRetries)
        {
            // Initialize retry stack
            var retryStack = new Stack<Func<Task>>(times);
            for (var i = 0; i < times; i++)
                retryStack.Push(action);
            // Test the attempted action
            TryPopAsync(retryStack, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an asynchronous Func of type T with a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the Func of Action of T returns</typeparam>
        /// <param name="action">A Task of a Func that's tested</param>
        /// <param name="times">>Retry count</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <returns>The wrapped value of Task</returns>
        public T AttemptAsync<T>(Func<Task<T>> action, int times, TimeSpan timeBetweenRetries)
        {
            // Initialize retry stack
            var retryStack = new Stack<Func<Task<T>>>(times);
            for (var i = 0; i < times; i++)
                retryStack.Push(action);
            // Test the attempted action
            return TryPopAsync(retryStack, timeBetweenRetries);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Tries to pop actions from the stack. If the action fails it retries, until
        /// no actions left.
        /// </summary>
        /// <param name="retryStack">The stack that holds all the retry actions</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        private void TryPop(Stack<Action> retryStack, TimeSpan timeBetweenRetries)
        {
            var exceptions = new List<Exception>();
            try
            {
                // Pop the action. Attempt
                retryStack.Pop()();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                // Recursion root point
                if (retryStack.Count > 0)
                {
                    // Attempt a retry. Wait for `timeBetweenRetries`
                    Thread.Sleep(timeBetweenRetries);

                    // Recursive call to TryPop
                    TryPop(retryStack, timeBetweenRetries);
                }
                else
                {
                    // Throw back all the exceptions that occured
                    throw new AggregateException(exceptions);
                }
            }
        }

        /// <summary>
        /// Tries to pop actions from the stack. If the action fails it retries, until
        /// no actions left.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="retryStack">The stack that holds all the retry funcs</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        private void TryPop<T>(Stack<Action<T>> retryStack, TimeSpan timeBetweenRetries)
        {
            var exceptions = new List<Exception>();
            try
            {
                // Pop the action. Attempt
                retryStack.Pop();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                // Recursion root point
                if (retryStack.Count > 0)
                {
                    // Attempt a retry. Wait for `timeBetweenRetries`
                    Thread.Sleep(timeBetweenRetries);

                    // Recursive call to TryPop
                    TryPop(retryStack, timeBetweenRetries);
                }

                // Throw back all exceptions that occurred
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        /// Tries to pop actions from the stack. If the action fails it retries, until
        /// no actions left.
        /// </summary>
        /// <typeparam name="T">The type of the return value the Func returns</typeparam>
        /// <param name="retryStack">The stack that holds all the retry funcs</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <returns>The return value from Func of T</returns>
        private T TryPop<T>(Stack<Func<T>> retryStack, TimeSpan timeBetweenRetries)
        {
            var exceptions = new List<Exception>();
            try
            {
                // Pop the action. Attempt
                return retryStack.Pop()();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                // Recursion root point
                if (retryStack.Count > 0)
                {
                    // Attempt a retry. Wait for `timeBetweenRetries`
                    Thread.Sleep(timeBetweenRetries);

                    // Recursive call to TryPop
                    return TryPop(retryStack, timeBetweenRetries);
                }

                // Throw back all exceptions that occurred
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        /// Tries to pop actions from the stack. If the action fails it retries, until
        /// no actions left.
        /// </summary>
        /// <param name="retryStack">The stack that holds all the retry funcs</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <returns>An awaitable Task</returns>
        private void TryPopAsync(Stack<Func<Task>> retryStack, TimeSpan timeBetweenRetries)
        {
            var exceptions = new List<Exception>();
            try
            {
                retryStack.Pop()().Wait();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                // Recursion root point
                if (retryStack.Count > 0)
                {
                    // Attempt a retry. Wait for `timeBetweenRetries`
                    Task.Delay(timeBetweenRetries).Wait();

                    // Recursive call to TryPop
                    TryPopAsync(retryStack, timeBetweenRetries);
                }
                else
                    // Throw back all exceptions that occurred
                    throw new AggregateException(exceptions);

            }
        }

        /// <summary>
        /// Tries to pop actions from the stack. If the action fails it retries, until
        /// no actions left.
        /// </summary>
        /// <typeparam name="T">The type of the return value the Func returns</typeparam>
        /// <param name="retryStack">The stack that holds all the retry funcs</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <returns>The wrapped value of Task</returns>
        private T TryPopAsync<T>(Stack<Func<Task<T>>> retryStack, TimeSpan timeBetweenRetries)
        {
            var exceptions = new List<Exception>();
            try
            {
                return retryStack.Pop()().Result;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                // Recursion root point
                if (retryStack.Count > 0)
                {
                    // Attempt a retry. Wait for `timeBetweenRetries`
                    Task.Delay(timeBetweenRetries).Wait();

                    // Recursive call to TryPop
                    return TryPopAsync(retryStack, timeBetweenRetries);
                }

                // Throw back all exceptions that occurred
                throw new AggregateException(exceptions);
            }
        }
        #endregion
    }
}
