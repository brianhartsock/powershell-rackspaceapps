using System.Collections.Specialized;
using System.Net;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Abstraction for the System.Net.WebClient class.
    /// </summary>
    public interface IWebClient
    {
        WebHeaderCollection Headers { get; set; }

        string DownloadString(string url);

        string UploadValues(string method, string address, NameValueCollection data);
    }
}
