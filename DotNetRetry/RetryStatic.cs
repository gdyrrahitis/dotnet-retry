namespace DotNetRetry
{
    public class RetryStatic
    {
        private static IRetry _instance;
        public static IRetry Instance {
            get { return _instance ?? (_instance = new Retry()); }
        }
    }
}