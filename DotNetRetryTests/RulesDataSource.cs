namespace DotNetRetry.Tests
{
    using System.Collections.Generic;
    using DotNetRetry.Rules;

    public static class RulesDataSource
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[]{ Rule.Sequential }
        };
    }
}
