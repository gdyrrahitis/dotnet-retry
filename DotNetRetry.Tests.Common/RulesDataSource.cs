using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
namespace DotNetRetry.Tests.Common
{
    using System.Collections.Generic;
    using Core.Activators;
    using Rules;

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
