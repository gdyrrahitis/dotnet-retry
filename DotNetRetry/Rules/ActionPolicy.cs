namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using static Core.Auxiliery.Exceptions;

    /// <summary>
    /// 
    /// </summary>
    public class ActionPolicy: ActionPolicyBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        public ActionPolicy(Retriable retriable) : base(retriable)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attempts"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        protected override void Do(Action action, ref int attempts, TimeSpan timeBetweenRetries, List<Exception> exceptions, TimeSpan time)
        {
            while (attempts > 0)
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
                        ThrowFlattenAggregateException(exceptions);
                    }

                    if (--attempts > 0)
                    {
                        Task.Delay(timeBetweenRetries).Wait();
                        time = time.Add(timeBetweenRetries);
                    }
                    else
                    {
                        ThrowFlattenAggregateException(exceptions);
                    }

                    if (Retriable.CancellationRule != null && Retriable.CancellationRule.HasExceededMaxTime(time))
                    {
                        Retriable.OnAfterRetryInvocation();
                        ThrowFlattenAggregateException(exceptions);
                    }
                }
            }
        }
    }
}