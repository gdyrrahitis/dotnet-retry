namespace DotNetRetry
{
    /// <summary>
    /// A contract which derives from retry action and function contracts
    /// </summary>
    public interface IRetry : IRetryAction, IRetryFunction
    {
    }
}