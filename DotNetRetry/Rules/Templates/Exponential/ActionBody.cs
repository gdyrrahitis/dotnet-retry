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
    /// 
    /// </summary>
    internal class ActionBody: ActionBodyTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        internal ActionBody(Retriable retriable) : base(retriable)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
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
        }
    }
}