using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    /// <summary>
    /// Specifies a Delayer contract
    /// </summary>
    internal interface IDelayer: ISyncDelayer
    {
    }
}