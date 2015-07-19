namespace DotNetRetry
{
    public class RetryWrapper
    {
        public static IRetry GetRetryMechanism()
        {
            return RetryStatic.Instance;
        }
    }
}