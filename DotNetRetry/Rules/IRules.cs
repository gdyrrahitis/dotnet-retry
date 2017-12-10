namespace DotNetRetry.Rules
{
    using System;

    /// <summary>
    /// A contract for retry rules.
    /// </summary>
    public interface IRules: IRetry
    {
        /// <summary>
        /// Sets an OnBeforeRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRules"/> instance.</returns>
        IRules OnBeforeRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnAfterRetry event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRules"/> instance.</returns>
        IRules OnAfterRetry(EventHandler handler);

        /// <summary>
        /// Sets an OnFailure event.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> to handle the event.</param>
        /// <returns>The same <see cref="IRules"/> instance.</returns>
        IRules OnFailure(EventHandler handler);
    }
}