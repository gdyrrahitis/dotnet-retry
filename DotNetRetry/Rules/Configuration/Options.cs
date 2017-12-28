namespace DotNetRetry.Rules.Configuration
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 
        /// </summary>
        public Options(int attempts, TimeSpan time)
        {
            Attempts = attempts;
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