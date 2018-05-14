namespace DotNetRetry.Rules.Configuration
{
    using System;
    using Core.Abstractions;
    using Core.Auxiliery;

    /// <summary>
    /// Specifies <see cref="Retriable"/> options.
    /// </summary>
    public class Options
    {
        private int _attempts;
        /// <summary>
        /// The number of retry attempts.
        /// </summary>
        public int Attempts
        {
            get { return _attempts; }
            set
            {
                Guards.ValidateArguments(value);
                _attempts = value;
            }
        }

        private TimeSpan _time;
        /// <summary>
        /// Time specified for rule.
        /// </summary>
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                Guards.ValidateArguments(value);
                _time = value;
            }
        }
    }
}