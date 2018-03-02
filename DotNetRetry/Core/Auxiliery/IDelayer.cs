using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    /// <summary>
    /// Specifies a Delayer contract
    /// </summary>
    internal interface IDelayer: ISyncDelayer
    {
    }
}