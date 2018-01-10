using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension for exception collection.
    /// </summary>
    internal static class ExceptionsExtension
    {
        /// <summary>
        /// Throws a flatten aggregate exception to the caller.
        /// </summary>
        /// <param name="exceptions">An <see cref="IEnumerable{T}"/> of <see cref="Exception"/> objects.</param>
        /// <remarks>
        /// For more info on Flatten method see 
        /// https://msdn.microsoft.com/en-us/library/system.aggregateexception.flatten(v=vs.110).aspx
        /// </remarks>
        internal static void ThrowFlattenAggregateException(this IEnumerable<Exception> exceptions)
        {
            var aggregateException = new AggregateException(exceptions);
            throw aggregateException.Flatten();
        }
    }
}