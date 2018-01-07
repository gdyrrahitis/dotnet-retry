using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Loops
{
    using System;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// Defines an abstract looper.
    /// </summary>
    internal abstract class Looper
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly ActionBodyTemplate ActionBody;

        /// <summary>
        /// The parent <see cref="Retriable"/>.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Creates an instance of <see cref="Looper"/>.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        protected Looper(ActionBodyTemplate actionBody, Retriable retriable)
        {
            Retriable = retriable;
            ActionBody = actionBody;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public void Attempt(Action action)
        {
            BeforeRetry();
            Do(action);
            AfterRetry();
        }

        /// <summary>
        /// Runs an infinite loop, which only breaks if a cancellation policy is set.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        protected abstract void Do(Action action);

        /// <summary>
        /// Hook to execute before retry policy execution.
        /// </summary>
        protected virtual void BeforeRetry() => Retriable.OnBeforeRetryInvocation();

        /// <summary>
        /// Hook to execute after retry policy execution.
        /// </summary>
        protected virtual void AfterRetry() => Retriable.OnAfterRetryInvocation();
    }
}