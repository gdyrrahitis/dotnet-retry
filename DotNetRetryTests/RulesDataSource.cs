namespace DotNetRetry.Unit.Tests
{
    using System.Collections.Generic;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Rules;

    public static class RulesDataSource
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[]{ Strategy.Sequential },
            new object[]{ Strategy.Exponential }
        };

        internal static IEnumerable<IActivator> Activators
        {
            get
            {
                var activators = new List<IActivator>
                {
                    new NullActivator(),
                    new TypeActivator()
                };
                return activators;
            }
        }
    }
}
