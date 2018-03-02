using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Abstractions;
    using Core.Auxiliery;
    using Core.Time;

    /// <summary>
    /// Defines an abstract exception body.
    /// </summary>
    internal abstract class ExceptionBodyTemplate
    {
        private readonly Retriable _retriable;

        /// <summary>
        /// Creates a new instance of <see cref="ExceptionBodyTemplate"/>.
        /// </summary>
        /// <param name="retriable">The parent <see cref="Retriable"/>.</param>
        protected ExceptionBodyTemplate(Retriable retriable)
        {
            _retriable = retriable;
        }

        /// <summary>
        /// The algorithm to calculate the wait timerService for this policy.
        /// </summary>
        /// <returns>The timerService to wait in <see cref="TimeSpan"/>.</returns>
        internal abstract TimeSpan WaitTime();

        /// <summary>
        /// The algorithm used to delay the retry of the specified action
        /// for this policy.
        /// </summary>
        /// <param name="attempts">The number of current attempts.</param>
        /// <param name="timeToWait">Time to wait before retry.</param>
        /// <param name="exceptions">Failures occurred up to this point.</param>
        internal abstract void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions);

        /// <summary>
        /// Retry body template method.
        /// </summary>
        /// <param name="exceptions">Failures occurred up to this point.</param>
        /// <param name="ex">Current failure that occurred.</param>
        /// <param name="attempts">The number of current attempts.</param>
        /// <param name="timerService">Current timerService passed.</param>
        internal void Retry(List<Exception> exceptions, Exception ex, int attempts, TimerService timerService)
        {
            DispatchBeforeRetryEvent();
            DispatchOnFailureEvent();
            AddExceptionInList(exceptions, ex);
            CancelIfCertainExceptionOccurred(exceptions, ex);
            Delay(attempts, WaitTime(), exceptions);
            CancelIfExceededTime(exceptions, timerService);
        }

        private void DispatchBeforeRetryEvent() => _retriable.OnBeforeRetryInvocation();

        private void DispatchOnFailureEvent() => _retriable.OnFailureInvocation();

        private static void AddExceptionInList(ICollection<Exception> exceptions, Exception ex) => 
            exceptions.Add(ex);

        private void DispatchAfterRetryEvent() => _retriable.OnAfterRetryInvocation();

        private void CancelIfExceededTime(IReadOnlyCollection<Exception> exceptions, TimerService timerService)
        {
            if (HasExceededTime(timerService))
            {
                DispatchAfterRetryEventAndThrowAggregateException(exceptions);
            }
        }

        private bool HasExceededTime(TimerService timerService) => 
            AreAnyCancellationRulesSet() && 
            _retriable.CancellationRule.HasExceededMaxTime(timerService.Add(_retriable.Options.Time));

        private bool AreAnyCancellationRulesSet() => _retriable.CancellationRule != null;

        private void DispatchAfterRetryEventAndThrowAggregateException(IReadOnlyCollection<Exception> exceptions)
        {
            DispatchAfterRetryEvent();
            exceptions.ThrowFlattenAggregateException();
        }

        private void CancelIfCertainExceptionOccurred(IReadOnlyCollection<Exception> exceptions, Exception ex)
        {
            if (IsExceptionIncludedInList(ex))
            {
                DispatchAfterRetryEventAndThrowAggregateException(exceptions);
            }
        }

        private bool IsExceptionIncludedInList(Exception ex) =>
            AreAnyCancellationRulesSet() && _retriable.CancellationRule.IsIn(ex);
    }
}
