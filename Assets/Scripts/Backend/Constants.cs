namespace Backend
{
    public class Constants
    {
        public const int webRequestTimeout = 30;
        private static bool isLocalTesting = true;
        public static string LocalAddress = isLocalTesting ? "http://localhost:8081/api/" : "";
    }
}