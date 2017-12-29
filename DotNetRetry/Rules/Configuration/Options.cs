namespace DotNetRetry.Rules.Configuration
{
    using System;
    using Core.Auxiliery;

    /// <summary>
    /// 
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values.</exception>
        public Options(int attempts, TimeSpan time)
        {
            Guards.ValidateArguments(attempts, time);
            Attempts = attempts;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attempts"></param>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        public Options(int attempts)
        {
            Guards.ValidateArguments(attempts);
            Attempts = attempts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values.</exception>
        public Options(TimeSpan time)
        {
            Guards.ValidateArguments(time);
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Attempts { get; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Time { get; }
    }
}