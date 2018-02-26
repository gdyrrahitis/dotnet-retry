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
    using Core.Time;

    /// <summary>
    /// Template class for returnable functions.
    /// </summary>
    internal abstract class FunctionBodyTemplate: ExceptionBodyTemplate
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="FunctionBodyTemplate"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> rules object.</param>
        protected FunctionBodyTemplate(Retriable retriable): base(retriable)
        {
            Retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="function">The function to try execute</param>
        /// <param name="exceptions">Failures happened up to this point.</param>
        /// <param name="timerService">Time to wait for retry.</param>
        /// <param name="attempts">Remaining attempts.</param>
        /// <param name="result"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        internal abstract bool Do<T>(Func<T> function, List<Exception> exceptions, TimerService timerService, int attempts, out T result);
    }
}