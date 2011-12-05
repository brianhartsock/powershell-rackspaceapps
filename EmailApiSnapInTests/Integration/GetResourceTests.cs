using NUnit.Framework;
using Moq;
using EmailApiSnapIn;
using NUnit.Framework.SyntaxHelpers;
using System.Xml;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Net;

namespace EmailApiSnapInTests.Integration
{
    [TestFixture]
    public class GetResourceTests : EmailApiTestFixture
    {
        string url = "customers";
        string response = "<xml><data /></xml>";

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Runspace.SetVariable(EmailApiCommand.AccessIdVariable, AccessId);
            Runspace.SetVariable(EmailApiCommand.SecretKeyVariable, SecretKey);
            Runspace.SetVariable(EmailApiCommand.BaseUrlVariable, BaseUrl);

            client.Setup(c => c.DownloadString(It.IsAny<string>()))
                .Returns(response)
                .AtMostOnce();
        }

        [Test]
        public void Simple_get_request_with_just_url()
        {
            var output = Runspace.Invoke("get-resource '{0}'", url);

            AssertThatOutputIsExpectedXml(output);
            AssertCommonalities();
            client.VerifyGetOnUrl(BaseUrl, url);
        }

        [Test]
        public void Get_request_with_query()
        {
            var output = Runspace.Invoke("get-resource '{0}' -Query @{{param1='a'; param2=1}}", url);

            AssertThatOutputIsExpectedXml(output);
            AssertCommonalities();
            client.VerifyGetOnUrl(BaseUrl, url);
            client.VerifyGetContainedParmeter("param1", "a");
            client.VerifyGetContainedParmeter("param2", "1");
        }

        private void AssertCommonalities()
        {
            Headers.AssertAuthorization(AccessId, SecretKey);
            Headers.AssertUserAgent();
            Mocks.VerifyAll();
        }


        private void AssertThatOutputIsExpectedXml(Collection<PSObject> output)
        {
            Assert.That(output, Has.Count(1));
            Assert.That(output[0].BaseObject, Is.TypeOf(typeof(XmlElement)));
        }
    }
}
