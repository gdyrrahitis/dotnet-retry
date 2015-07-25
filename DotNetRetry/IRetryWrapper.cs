namespace DotNetRetry
{
    public interface IRetryWrapper
    {
        IRetry GetRetryMechanism();
    }
}