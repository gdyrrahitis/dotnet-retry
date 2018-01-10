using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Templates.Exponential
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Factories;

    /// <summary>
    /// Performs a template strategy for non-returnable actions
    /// </summary>
    internal class ActionBody: ActionBodyTemplate
    {
        private readonly Random _random;
        private readonly IWaitableFactory _waitableFactory;
        private const int MaxMilliSeconds = 1001;
        private const int MinMilliSeconds = 0;
        private const int Power = 2;
        private int _count;

        /// <summary>
        /// Creates an instance of <see cref="ActionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        /// <param name="random"></param>
        /// <param name="waitableFactory"></param>
        internal ActionBody(Retriable retriable, Random random, IWaitableFactory waitableFactory) : base(retriable)
        {
            _random = random;
            _waitableFactory = waitableFactory;
        }

        /// <summary>
        /// The actual retry algorithm.
        /// </summary>
        /// <param name="action">The non-returnable action to retry.</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <param name="attempts"></param>
        internal override bool Do(Action action, List<Exception> exceptions, TimeSpan time, int attempts)
        {
            _count = exceptions.Count;

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                Retry(exceptions, ex, attempts, time);
            }

            return false;
        }

        /// <summary>
        /// The algorithm to calculate the wait time for this policy.
        /// min((2 ^ n) + random(0, 1000), backoff)
        /// </summary>
        /// <returns>The time to wait in <see cref="TimeSpan"/>.</returns>
        internal override TimeSpan WaitTime()
        {
            var wait = Math.Min(Math.Pow(Power, _count) + _random.Next(MinMilliSeconds, MaxMilliSeconds),
                Retriable.Options.Time.TotalMilliseconds);
            return TimeSpan.FromMilliseconds(wait);
        }

        /// <summary>
        /// The algorithm used to delay the retry of the specified action
        /// for this policy.
        /// </summary>
        /// <param name="attempts">The number of current attempts.</param>
        /// <param name="timeToWait">Time to wait before retry.</param>
        /// <param name="exceptions">Failures occurred up to this point.</param>
        internal override void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions)
        {
            var waitable = _waitableFactory.Select(attempts);
            waitable.Exceptions = exceptions;
            waitable.Wait(timeToWait);
        }
    }
}