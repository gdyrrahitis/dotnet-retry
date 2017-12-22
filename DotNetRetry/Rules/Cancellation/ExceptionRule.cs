namespace DotNetRetry.Rules.Cancellation
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionRule
    {
        private readonly CancellationRule _cancellationRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationRule"></param>
        public ExceptionRule(CancellationRule cancellationRule)
        {
            _cancellationRule = cancellationRule;
        }

        /// <summary>
        /// Adds a <typeparamref name="TException"/> exception in the list of exceptions that stop
        /// the current retry policy.
        /// </summary>
        /// <typeparam name="TException">The exception type on which the retry policy halts further actions.</typeparam>
        /// <returns>An <see cref="ExceptionRule"/> instance.</returns>
        public ExceptionRule Or<TException>()
        {
            _cancellationRule.AddInExceptionList(typeof(TException));
            return this;
        }

        /// <summary>
        /// Adds an <paramref name="type"/> exception in the list of exceptions that stop
        /// the current retry policy.
        /// </summary>
        /// <param name="type">The exception type on which the retry policy halts further actions.</param>
        /// <returns>An <see cref="ExceptionRule"/> instance.</returns>
        public ExceptionRule Or(Type type)
        {
            _cancellationRule.AddInExceptionList(type);
            return this;
        }

        /// <summary>
        /// Stops from adding further exception types in the exception list
        /// and returns control back to <see cref="CancellationRule"/> instance.
        /// </summary>
        /// <returns>The parent <see cref="CancellationRule"/> instance.</returns>
        public CancellationRule End() => _cancellationRule;
    }
}