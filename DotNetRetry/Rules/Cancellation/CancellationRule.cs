namespace DotNetRetry.Rules.Cancellation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Cancellation rules for enforced retry policies.
    /// </summary>
    public class CancellationRule
    {
        private readonly IList<Type> _exceptions = new List<Type>();
        private TimeSpan? _time;

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
        /// 
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal bool IsIn<TException>(TException ex) => 
            _exceptions.Any(x => x == ex.GetType());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="type"></param>
        internal void AddInExceptionList(Type type) => _exceptions.Add(type);
    }
}