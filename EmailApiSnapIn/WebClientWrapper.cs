using System.Collections.Specialized;
using System.Net;
using System.Text;
using EmailApiSnapIn;
using System.Xml;
using System.Web;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Wrapper class for the System.Net.WebClient class.
    /// </summary>
    public class WebClientWrapper : IWebClient
    {
        private WebClient client;

        public WebClientWrapper()
        {
            client = new WebClient();
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return client.Headers;
            }
            set
            {
                client.Headers = value;
            }
        }

        public string DownloadString(string url)
        {
            return client.DownloadString(url);
        }

        public string UploadValues(string method, string address, NameValueCollection data)
        {
            return Encoding.UTF8.GetString(client.UploadValues(address, method, data));
        }
    }
}

