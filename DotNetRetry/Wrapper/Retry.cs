namespace DotNetRetry.Wrapper
{
    public class Retry: IRetryWrapper
    {
        public IRetry GetRetryMechanism()
        {
            return Static.Retry.Instance;
        }
    }
}