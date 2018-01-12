using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Time
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    internal class TimerService
    {
        /// <summary>
        /// 
        /// </summary>
        public TimerService()
        {
            Time = TimeSpan.Zero;
        }

        public TimerService(TimeSpan time)
        {
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeToAdd"></param>
        /// <returns></returns>
        public TimeSpan Add(TimeSpan timeToAdd)
        {
            Time =  Time.Add(timeToAdd);
            return Time;
        }
    }
}