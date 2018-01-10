using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;

    /// <summary>
    /// Template class for returnable functions.
    /// </summary>
    internal abstract class FunctionBodyTemplate
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="FunctionBodyTemplate"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> rules object.</param>
        protected FunctionBodyTemplate(Retriable retriable)
        {
            Retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function)
        {
            var exceptions = new List<Exception>();
            var time = TimeSpan.Zero;

            BeforeRetry();
            var result = Do(function, exceptions, time);
            AfterRetry();
            return result;
        }

        /// <summary>
        /// Hook to execute before retry policy execution.
        /// </summary>
        protected virtual void BeforeRetry() => Retriable.OnBeforeRetryInvocation();

        /// <summary>
        /// Hook to execute after retry policy execution.
        /// </summary>
        protected virtual void AfterRetry() => Retriable.OnAfterRetryInvocation();

        /// <summary>
        /// Attempts to retry an method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <returns>The function return value</returns>
        internal abstract T Do<T>(Func<T> function, List<Exception> exceptions, TimeSpan time= default(TimeSpan));
    }
}