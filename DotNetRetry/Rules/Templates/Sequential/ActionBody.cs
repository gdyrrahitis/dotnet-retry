using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates.Sequential
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
        private readonly IWaitableFactory _waitableFactory;

        /// <summary>
        /// Creates an instance of <see cref="ActionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        /// <param name="waitableFactory"></param>
        internal ActionBody(Retriable retriable, IWaitableFactory waitableFactory) : base(retriable)
        {
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

        internal override TimeSpan WaitTime() => Retriable.Options.Time;

        internal override void Delay(int attempts, TimeSpan timeToWait, List<Exception> exceptions) => 
            _waitableFactory.Select(attempts).Wait(timeToWait, exceptions);
    }
}