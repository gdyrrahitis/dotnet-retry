using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Abstractions;
    using Core.Auxiliery;

    /// <summary>
    /// 
    /// </summary>
    internal class FunctionBody: FunctionBodyTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        internal FunctionBody(Retriable retriable) : base(retriable)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <param name="attempts"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        protected override T Do<T>(Func<T> function, ref int attempts, TimeSpan timeBetweenRetries, List<Exception> exceptions, TimeSpan time)
        {
            while (attempts > 0)
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

                    if (--attempts > 0)
                    {
                        Task.Delay(timeBetweenRetries).Wait();
                        time = time.Add(timeBetweenRetries);
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