namespace DotNetRetry.Rules
{
    using System;
    using Cancellation;
    using Core;

    /// <summary>
    /// A contract for retry rules.
    /// </summary>
    public interface IRule: IRetry
    {
        /// <summary>
        /// Sets an OnBeforeRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRule"/> instance.</returns>
        IRule OnBeforeRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnAfterRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRule"/> instance.</returns>
        IRule OnAfterRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnFailure event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRule"/> instance.</returns>
        IRule OnFailure(EventHandler handler);

        /// <summary>
        /// Sets cancellation rules for current retry policy.
        /// </summary>
        /// <param name="cancellationRules">A builder object to build cancellation rules on.</param>
        /// <returns>The same <see cref="IRule"/> instance.</returns>
        IRule Cancel(Action<CancellationRule> cancellationRules);
    }
}