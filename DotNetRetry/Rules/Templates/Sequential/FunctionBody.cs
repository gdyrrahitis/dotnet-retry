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
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <returns>A value of <typeparamref name="T"/>.</returns>
        internal override T Do<T>(Func<T> function, List<Exception> exceptions, TimeSpan time = default(TimeSpan))
        {
            var attempts = Retriable.Options.Attempts;

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

            throw new InvalidOperationException(Constants.InvalidOperationExceptionErrorMessage);
        }
    }
}