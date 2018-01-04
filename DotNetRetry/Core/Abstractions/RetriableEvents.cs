namespace DotNetRetry.Core.Abstractions
{
    using System;
    using Rules.Cancellation;

    /// <summary>
    /// A base class for retriable.
    /// </summary>
    public abstract partial class Retriable
    {
        /// <summary>
        /// The event which is fired before an attempt for a retry is made.
        /// </summary>
        protected event EventHandler BeforeRetry;

        /// <summary>
        /// The event which is fired after the attempt of calling the target method.
        /// </summary>
        protected event EventHandler AfterRetry;

        /// <summary>
        /// The event which is fired when an error occurs when attempting to call the target method.
        /// </summary>
        protected event EventHandler Failure;

        /// <summary>
        /// Invokes the OnBeforeRetry event.
        /// </summary>
        public virtual void OnBeforeRetryInvocation() => BeforeRetry?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Invokes the OnAfterRetry event.
        /// </summary>
        public virtual void OnAfterRetryInvocation() => AfterRetry?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Invokes the OnFailure event.
        /// </summary>
        public virtual void OnFailureInvocation() => Failure?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Sets an OnBeforeRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="Retriable"/> instance.</returns>
        public abstract Retriable OnBeforeRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnAfterRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="Retriable"/> instance.</returns>
        public abstract Retriable OnAfterRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnFailure event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="Retriable"/> instance.</returns>
        public abstract Retriable OnFailure(EventHandler handler);

        /// <summary>
        /// Sets cancellation rules for current retry policy.
        /// </summary>
        /// <param name="cancellationRules">A builder object to build cancellation rules on.</param>
        /// <returns>The same <see cref="Retriable"/> instance.</returns>
        public abstract Retriable Cancel(Action<CancellationRule> cancellationRules);
    }
}