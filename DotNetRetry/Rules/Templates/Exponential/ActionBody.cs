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
        /// <summary>
        /// Creates an instance of <see cref="ActionBody"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> parent class.</param>
        internal ActionBody(Retriable retriable) : base(retriable)
        {
        }

        /// <summary>
        /// The actual retry algorithm.
        /// </summary>
        /// <param name="action">The non-returnable action to retry.</param>
        protected override void Do(Action action)
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
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    var wait = Math.Min(Math.Pow(2, n++) + random.Next(0, 1001),
                        Retriable.Options.Time.TotalMilliseconds);
                    var timeToWait = TimeSpan.FromMilliseconds(wait);

                    Retriable.OnFailureInvocation();
                    exceptions.Add(ex);

                    if (Retriable.CancellationRule != null && Retriable.CancellationRule.IsIn(ex))
                    {
                        Retriable.OnAfterRetryInvocation();
                        exceptions.ThrowFlattenAggregateException();
                    }

                    if (attempts > 0)
                    {
                        Task.Delay(timeToWait).Wait();
                    }
                    else
                    {
                        exceptions.ThrowFlattenAggregateException();
                    }

                    time = time.Add(timeToWait);
                    if (Retriable.CancellationRule != null && Retriable.CancellationRule.HasExceededMaxTime(time))
                    {
                        Retriable.OnAfterRetryInvocation();
                        exceptions.ThrowFlattenAggregateException();
                    }
                }
            }
        }
    }
}