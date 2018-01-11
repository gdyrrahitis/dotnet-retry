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
            Value = TimeSpan.Zero;
        }

        public TimerService(TimeSpan value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Value { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        public TimeSpan Add(double toAdd) => Value.Add(TimeSpan.FromMilliseconds(toAdd));
    }
}