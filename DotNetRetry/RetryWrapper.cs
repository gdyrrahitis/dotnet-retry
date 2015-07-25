namespace DotNetRetry
{
    public class RetryWrapper: IRetryWrapper
    {
        public IRetry GetRetryMechanism()
        {
            return RetryStatic.Instance;
        }
    }
}