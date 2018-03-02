namespace DotNetRetry.Wrapper
{
    public interface IRetryWrapper
    {
        IRetry GetRetryMechanism();
    }
}