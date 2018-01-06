namespace DotNetRetry.Rules.Loops
{
    using System;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// 
    /// </summary>
    internal abstract class Looper
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly ActionBodyTemplate ActionBody;

        protected readonly Retriable Retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="Looper"/>.
        /// </summary>
        /// <param name="actionBody">The <see cref="ActionBodyTemplate"/> rules object.</param>
        /// <param name="retriable"></param>
        protected Looper(ActionBodyTemplate actionBody, Retriable retriable)
        {
            Retriable = retriable;
            ActionBody = actionBody;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public void Attempt(Action action)
        {
            BeforeRetry();
            Do(action);
            AfterRetry();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        protected abstract void Do(Action action);

        /// <summary>
        /// Hook to execute before retry policy execution.
        /// </summary>
        protected virtual void BeforeRetry() => Retriable.OnBeforeRetryInvocation();

        /// <summary>
        /// Hook to execute after retry policy execution.
        /// </summary>
        protected virtual void AfterRetry() => Retriable.OnAfterRetryInvocation();
    }
}