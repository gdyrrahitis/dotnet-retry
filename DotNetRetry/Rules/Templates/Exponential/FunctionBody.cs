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
    /// Performs a template strategy for returnable actions
    /// </summary>
    internal class FunctionBody: FunctionBodyTemplate
    {
        /// <summary>
        /// Creates an instance of <see cref="FunctionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        internal FunctionBody(Retriable retriable) : base(retriable)
        {
        }

        /// <summary>
        /// The actual retry algorithm.
        /// </summary>
        /// <typeparam name="T">The returnable type.</typeparam>
        /// <param name="function">The returnable function to retry.</param>
        /// <returns>A value of <typeparamref name="T"/>.</returns>
        protected override T Do<T>(Func<T> function)
        {
            var exceptions = new List<Exception>();
            var time = TimeSpan.Zero;
            var attempts = Retriable.Options.Attempts;
            var random = new Random();
            var n = 0;

            while (attempts-- > 0)
            {
                try
                {
                    var result = function();
                    return result;
                }
                catch (Exception ex)
                {
                    Retriable.OnFailureInvocation();
                    exceptions.Add(ex);

                    if (Retriable.CancellationRule != null && Retriable.CancellationRule.IsIn(ex))
                    {
                        Retriable.OnAfterRetryInvocation();
                        exceptions.ThrowFlattenAggregateException();
                    }

                    if (attempts > 0)
                    {
                        var wait = Math.Min(Math.Pow(2, n++) + random.Next(0, 1001),
                            Retriable.Options.Time.TotalMilliseconds);
                        var timeToWait = TimeSpan.FromMilliseconds(wait);
                        Task.Delay(timeToWait).Wait();
                        time = time.Add(timeToWait);
                    }
                    else
                    {
                        exceptions.ThrowFlattenAggregateException();
                    }

                    if (Retriable.CancellationRule != null && Retriable.CancellationRule.HasExceededMaxTime(time))
                    {
                        Retriable.OnAfterRetryInvocation();
                        exceptions.ThrowFlattenAggregateException();
                    }
                }
            }

            throw new InvalidOperationException(Constants.InvalidOperationExceptionErrorMessage);
        }
    }
}