﻿using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates
{
    using System;
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
            BeforeRetry();
            var result = Do(function);
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
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <returns>The function return value</returns>
        protected abstract T Do<T>(Func<T> function);
    }
}