
namespace EmailApiSnapIn
{
    /// <summary>
    /// Definition of all the customer headers used by the API so we don't have magic strings everywhere.
    /// </summary>
    public static class Headers
    {
        public const string Signature = "X-Api-Signature";
        public const string Accept = "Accept";
        public const string UserAgent = "User-Agent";
    }
}
