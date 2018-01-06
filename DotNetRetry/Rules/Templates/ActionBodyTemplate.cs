﻿using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;

    /// <summary>
    /// Template class for non-returnable functions.
    /// </summary>
    internal abstract class ActionBodyTemplate: ExceptionBodyTemplate
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="ActionBodyTemplate"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> rules object.</param>
        protected ActionBodyTemplate(Retriable retriable): base(retriable)
        {
            Retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The function to try execute</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <param name="attempts"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        internal abstract bool Do(Action action, List<Exception> exceptions, TimeSpan time, int attempts);
    }
}
