namespace DotNetRetry.Rules.Cancellation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Cancellation rules for enforced retry policies.
    /// </summary>
    public class CancellationRule
    {
        private TimeSpan? _time;
        private readonly HashSet<Type> _exceptions = new HashSet<Type>();

        /// <summary>
        /// The child exception rule.
        /// </summary>
        public readonly ExceptionRule ExceptionRule;

        /// <summary>
        /// Initializes a new instance of <see cref="CancellationRule"/>.
        /// </summary>
        public CancellationRule()
        {
            ExceptionRule = new ExceptionRule(this);
        }

        /// <summary>
        /// Stored time limit.
        /// </summary>
        public TimeSpan? StoredTime => _time;

        /// <summary>
        /// The stored exceptions.
        /// </summary>
        public IReadOnlyList<Type> StoredExceptions => new ReadOnlyCollection<Type>(_exceptions.ToList()); 

        /// <summary>
        /// Stops retrying after a specified time has passed.
        /// </summary>
        /// <param name="time">The time limit.</param>
        /// <returns>The <see cref="CancellationRule"/> instance.</returns>
        public CancellationRule After(TimeSpan time)
        {
            _time = time;
            return this;
        }

        /// <summary>
        /// Stops retrying when a specific exception occurs.
        /// </summary>
        /// <typeparam name="TException">The exception type on which the retry policy halts further actions.</typeparam>
        /// <returns>An <see cref="Cancellation.ExceptionRule"/> instance.</returns>
        public ExceptionRule OnFailure<TException>()
        {
            _exceptions.Add(typeof(TException));
            return ExceptionRule;
        }

        /// <summary>
        /// Stops retrying when a specific exception occurs.
        /// </summary>
        /// <param name="type">The exception type on which the retry policy halts further actions.</param>
        /// <returns>An <see cref="Cancellation.ExceptionRule"/> instance.</returns>
        public ExceptionRule OnFailure(Type type)
        {
            _exceptions.Add(type);
            return ExceptionRule;
        }

        /// <summary>
        /// Tests if certain exception is contained in the list.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="ex">The exception to test against.</param>
        /// <returns>
        /// Boolean value, <value>true</value> when exception object exists,
        /// <value>false</value> when does not exist.
        /// </returns>
        internal bool IsIn<TException>(TException ex) => 
            _exceptions.Any(e => e.IsEquivalentTo(ex.GetType()));

        /// <summary>
        /// Tests if timespan provided exceeds stored time.
        /// </summary>
        /// <param name="current">Current time.</param>
        /// <returns>
        /// Boolean value, <value>true</value> when <paramref name="current"/>
        /// has exceeded stored time, <value>false</value> when it hasn't.
        /// </returns>
        internal bool HasExceededMaxTime(TimeSpan current)
        {
            if (!_time.HasValue)
            {
                return false;
            }

            var result = _time.Value.Subtract(current);
            return result <= TimeSpan.Zero;
        }

        /// <summary>
        /// Adds exception in list.
        /// </summary>
        /// <param name="type">The type to add.</param>
        internal void AddExceptionType(Type type) => _exceptions.Add(type);
    }
}