namespace DotNetRetry.Core.Auxiliery
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionsExtension
    {
        /// <summary>
        /// Throws a flatten aggregate exception to the caller.
        /// </summary>
        /// <param name="exceptions">An <see cref="IEnumerable{T}"/> of <see cref="Exception"/> objects.</param>
        /// <remarks>
        /// For more info on Flatten method see 
        /// https://msdn.microsoft.com/en-us/library/system.aggregateexception.flatten(v=vs.110).aspx
        /// </remarks>
        public static void ThrowFlattenAggregateException(this IEnumerable<Exception> exceptions)
        {
            var aggregateException = new AggregateException(exceptions);
            throw aggregateException.Flatten();
        }
    }
}