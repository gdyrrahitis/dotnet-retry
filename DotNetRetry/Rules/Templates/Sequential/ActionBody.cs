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