using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Templates.Sequential
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Abstractions;
    using Core.Time;
    using Factories;

    /// <summary>
    /// Performs a template strategy for non-returnable actions
    /// </summary>
    internal class ActionBody: ActionBodyTemplate
    {
        private readonly IWaitableFactory _waitableFactory;

        /// <summary>
        /// Creates an instance of <see cref="ActionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        /// <param name="waitableFactory"></param>
        internal ActionBody(Retriable retriable, IWaitableFactory waitableFactory) : base(retriable)
        {
            _waitableFactory = waitableFactory;
        }

        /// <summary>
        /// The actual retry algorithm.
        /// </summary>
        /// <param name="action">The non-returnable action to retry.</param>
        /// <param name="exceptions"></param>
        /// <param name="timerService"></param>
        /// <param name="attempts"></param>
        internal override bool Do(Action action, List<Exception> exceptions, TimerService timerService, int attempts)
        {
            try
            {
                try
                {
                    action();
                }
                finally
                {
                    if (exceptions.Any())
                    {
                        Retriable.OnAfterRetryInvocation();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Retry(exceptions, ex, attempts, timerService);
            }

            return false;
        }

        /// <summary>
        /// The algorithm to calculate the wait timerService for this policy.
        /// </summary>
        /// <returns>The timerService to wait in <see cref="TimeSpan"/>.</returns>
        internal override TimeSpan WaitTime() => Retriable.Options.Time;

        /// <summary>
        /// The algorithm used to delay the retry of the specified action
        /// for this policy.
        /// </summary>
        /// <param name="attempts">The number of current attempts.</param>
        /// <param name="timeToWait">Time to wait before retry.</param>
        /// <param name="exceptions">Failures occurred up to this point.</param>
        internal override void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions)
        {
            var waitable = _waitableFactory.Select(attempts);
            waitable.Exceptions = exceptions;
            waitable.Wait(timeToWait);
        }
    }
}