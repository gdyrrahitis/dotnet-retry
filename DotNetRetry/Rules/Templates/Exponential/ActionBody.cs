using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates.Exponential
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Core.Auxiliery;

    /// <summary>
    /// Performs a template strategy for non-returnable actions
    /// </summary>
    internal class ActionBody: ActionBodyTemplate
    {
        private readonly Random _random;
        private const int MaxMilliSeconds = 1001;
        private const int MinMilliSeconds = 0;
        private const int Power = 2;
        private int _count;

        /// <summary>
        /// Creates an instance of <see cref="ActionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        /// <param name="random"></param>
        internal ActionBody(Retriable retriable, Random random) : base(retriable)
        {
            _random = random;
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

        internal override TimeSpan WaitTime()
        {
            var wait = Math.Min(Math.Pow(Power, _count) + _random.Next(MinMilliSeconds, MaxMilliSeconds),
                Retriable.Options.Time.TotalMilliseconds);
            return TimeSpan.FromMilliseconds(wait);
        }

        internal override void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions)
        {
            if (attempts > 0)
            {
                Task.Delay(timeToWait).Wait();
            }
            else
            {
                exceptions.ThrowFlattenAggregateException();
            }
        }
    }
}