using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
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

        protected readonly FunctionBodyTemplate FunctionBody;

        /// <summary>
        /// The parent <see cref="Retriable"/>.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Creates an instance of <see cref="Looper"/>.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="functionBody"></param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        protected Looper(ActionBodyTemplate actionBody, FunctionBodyTemplate functionBody, Retriable retriable)
        {
            Retriable = retriable;
            ActionBody = actionBody;
            FunctionBody = functionBody;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public void Attempt(Action action) => Do(action);

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="function">The function to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public T Attempt<T>(Func<T> function) => Do(function);

        /// <summary>
        /// Runs an infinite loop, which only breaks if a cancellation policy is set.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        protected abstract void Do(Action action);

        /// <summary>
        /// Runs an infinite loop, which only breaks if a cancellation policy is set.
        /// </summary>
        /// <param name="function">The function to execute.</param>
        protected abstract T Do<T>(Func<T> function);
    }
}