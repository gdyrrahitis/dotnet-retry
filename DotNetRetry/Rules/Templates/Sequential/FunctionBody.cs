using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Templates.Sequential
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Core.Auxiliery;
    using Factories;

    /// <summary>
    /// Performs a template strategy for returnable actions
    /// </summary>
    internal class FunctionBody : FunctionBodyTemplate
    {
        private readonly IWaitableFactory _waitableFactory;

        /// <summary>
        /// Creates an instance of <see cref="FunctionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        /// <param name="waitableFactory"></param>
        internal FunctionBody(Retriable retriable, IWaitableFactory waitableFactory) : base(retriable)
        {
            _waitableFactory = waitableFactory;
        }

        /// <summary>
        /// The actual retry algorithm.
        /// </summary>
        /// <typeparam name="T">The returnable type.</typeparam>
        /// <param name="function">The returnable function to retry.</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <param name="attempts"></param>
        /// <returns>A value of <typeparamref name="T"/>.</returns>
        internal override bool Do<T>(Func<T> function, List<Exception> exceptions, TimeSpan time, int attempts, out T result)
        {
            try
            {
                result = function();
                return true;
            }
            catch (Exception ex)
            {
                Retry(exceptions, ex, attempts, time);
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// The algorithm to calculate the wait time for this policy.
        /// </summary>
        /// <returns>The time to wait in <see cref="TimeSpan"/>.</returns>
        internal override TimeSpan WaitTime() => Retriable.Options.Time;

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