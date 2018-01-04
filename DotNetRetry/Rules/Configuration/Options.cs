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
        /// <summary>
        /// Initializes a new <see cref="Options"/> object.
        /// </summary>
        /// <param name="attempts">The number of retry attempts.</param>
        /// <param name="time">Time specified for rule.</param>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values.</exception>
        public Options(int attempts, TimeSpan time)
        {
            Guards.ValidateArguments(attempts, time);
            Attempts = attempts;
            Time = time;
        }

        /// <summary>
        /// Initializes a new <see cref="Options"/> object.
        /// </summary>
        /// <param name="attempts">The number of retry attempts.</param>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        public Options(int attempts)
        {
            Guards.ValidateArguments(attempts);
            Attempts = attempts;
        }

        /// <summary>
        /// Initializes a new <see cref="Options"/> object.
        /// </summary>
        /// <param name="time">Time specified for rule.</param>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values.</exception>
        public Options(TimeSpan time)
        {
            Guards.ValidateArguments(time);
            Time = time;
        }

        /// <summary>
        /// The number of retry attempts.
        /// </summary>
        public int Attempts { get; }

        /// <summary>
        /// Time specified for rule.
        /// </summary>
        public TimeSpan Time { get; }
    }
}