using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using EmailApiSnapIn;
using System.Collections.Specialized;

namespace EmailApiSnapInTests
{
    public static class ClientMockExtensions
    {
        public static void VerifyGetContainedParmeter(this Mock<IWebClient> client, string name, string value)
        {
            client.Verify(c => c.DownloadString(It.Is<string>(s => s.IndexOf(name + "=" + value) > s.IndexOf("?"))));
        }

        public static void VerifyGetOnUrl(this Mock<IWebClient> client, string baseUrl, string resource)
        {
            client.Verify(c => c.DownloadString(It.Is<string>(s => s.StartsWith(baseUrl + resource))));
        }

        public static void AssertMethod(this Mock<IWebClient> client, string method, string baseUrl, string resource)
        {
            client.Verify(c => c.UploadValues(method, baseUrl + resource, It.IsAny<NameValueCollection>()));
        }

        public static void AssertContainsParameter(this Mock<IWebClient> client, string name, string value)
        {
            client.Verify(c => c.UploadValues(
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.Is<NameValueCollection>(n => n[name] != null && n[name] == value)));
        }

        public static void AssertContainsNoPostData(this Mock<IWebClient> client)
        {
            client.Verify(c => c.UploadValues(It.IsAny<string>(), It.IsAny<string>(), It.Is<NameValueCollection>(n => n.Count == 0)));
        }
    }
}
