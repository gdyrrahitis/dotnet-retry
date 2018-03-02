namespace DotNetRetry.Static
{
    public class Retry
    {
        private static IRetry _instance;
        public static IRetry Instance => _instance ?? (_instance = new DotNetRetry.Retry());
    }
}