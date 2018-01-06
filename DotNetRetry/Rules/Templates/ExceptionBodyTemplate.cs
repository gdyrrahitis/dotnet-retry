namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Core.Auxiliery;

    internal abstract class ExceptionBodyTemplate
    {
        private readonly Retriable _retriable;

        protected ExceptionBodyTemplate(Retriable retriable)
        {
            _retriable = retriable;
        }

        internal abstract TimeSpan WaitTime();
        internal abstract void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions);

        internal void Retry(List<Exception> exceptions, Exception ex, int attempts, TimeSpan time)
        {
            _retriable.OnFailureInvocation();
            exceptions.Add(ex);

            CancelIfCertainExceptionOccurred(exceptions, ex);
            Delay(attempts, WaitTime(), exceptions);
            CancelIfExceededTime(exceptions, time);
        }

        private void CancelIfExceededTime(List<Exception> exceptions, TimeSpan time)
        {
            if (HasExceededTime(time))
            {
                DispatchAfterRetryEventAndThrowAggregateException(exceptions);
            }
        }

        private bool HasExceededTime(TimeSpan time) => 
            AreCancellationRulesSet() && 
            _retriable.CancellationRule.HasExceededMaxTime(time.Add(_retriable.Options.Time));

        private bool AreCancellationRulesSet() => _retriable.CancellationRule != null;

        private void DispatchAfterRetryEventAndThrowAggregateException(List<Exception> exceptions)
        {
            _retriable.OnAfterRetryInvocation();
            exceptions.ThrowFlattenAggregateException();
        }

        private void CancelIfCertainExceptionOccurred(List<Exception> exceptions, Exception ex)
        {
            if (IsExceptionIncludedInList(ex))
            {
                DispatchAfterRetryEventAndThrowAggregateException(exceptions);
            }
        }

        private bool IsExceptionIncludedInList(Exception ex) =>
            AreCancellationRulesSet() && _retriable.CancellationRule.IsIn(ex);
    }
}
