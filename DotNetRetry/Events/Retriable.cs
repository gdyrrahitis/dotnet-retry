namespace DotNetRetry.Events
{
    using System;

    /// <summary>
    /// A base class for retriable 
    /// </summary>
    public abstract class Retriable
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
    }
}