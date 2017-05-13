namespace DotNetRetry.Wrapper
{
    public class Retry: IRetryWrapper
    {
        public IRetry GetRetryMechanism() => Static.Retry.Instance;
    }
}