using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates.Sequential
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

            while (attempts-- > 0)
            {
                try
                {
                    action();
                    return;
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
                        Task.Delay(Retriable.Options.Time).Wait();
                    }
                    else
                    {
                        exceptions.ThrowFlattenAggregateException();
                    }

                    time = time.Add(Retriable.Options.Time);
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