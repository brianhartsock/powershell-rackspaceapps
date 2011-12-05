using NUnit.Framework;
using Moq;
using System.Management.Automation;
using NUnit.Framework.Extensions;
using System.Collections.Specialized;
using EmailApiSnapIn;

namespace EmailApiSnapInTests.Integration
{
    [TestFixture]
    public class ModifyResourceTests : EmailApiTestFixture
    {
        string url = "customers";
        string response = "<xml><data/></xml>";

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Runspace.SetVariable(EmailApiCommand.AccessIdVariable, AccessId);
            Runspace.SetVariable(EmailApiCommand.SecretKeyVariable, SecretKey);
            Runspace.SetVariable(EmailApiCommand.BaseUrlVariable, BaseUrl);

            client.Setup(c => c.UploadValues(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NameValueCollection>()))
                .Returns(response)
                .AtMostOnce();
        }

        [RowTest]
        [Row("post-resource {0}", ExpectedException=typeof(ParameterBindingException))]
        [Row("post-resource -Data @{{}}", ExpectedException=typeof(ParameterBindingException))]
        [Row("post-resource", ExpectedException = typeof(ParameterBindingException))]
        [Row("put-resource {0}", ExpectedException = typeof(ParameterBindingException))]
        [Row("put-resource -Data @{{}}", ExpectedException = typeof(ParameterBindingException))]
        [Row("put-resource", ExpectedException = typeof(ParameterBindingException))]
        [Row("remove-resource", ExpectedException = typeof(ParameterBindingException))]
        public void Modify_action_without_valid_params_throws_error(string cmd)
        {
            Runspace.Invoke(cmd, url);
        }

        [RowTest]
        [Row("post-resource {0} @{{param1='value1';param2='value2'}}", "POST")]
        [Row("put-resource {0} @{{param1='value1';param2='value2'}}", "PUT")]
        public void Modify_action_with_valid_data(string cmd, string method)
        {
            Runspace.Invoke(cmd, url);

            AssertCommonalities();
            client.AssertMethod(method, BaseUrl, url);
            client.AssertContainsParameter("param1", "value1");
            client.AssertContainsParameter("param2", "value2");
        }

        [RowTest]
        [Row("post-resource {0} @{{}}", "POST")]
        [Row("put-resource {0} @{{}}", "PUT")]
        [Row("remove-resource {0}", "DELETE")]
        public void Modify_action_with_no_post_data(string cmd, string method)
        {
            Runspace.Invoke(cmd, url);

            AssertCommonalities();
            client.AssertMethod(method, BaseUrl, url);
            client.AssertContainsNoPostData();
        }

        private void AssertCommonalities()
        {
            Headers.AssertAuthorization(AccessId, SecretKey);
            Headers.AssertUserAgent();
            Mocks.VerifyAll();
        }
    }
}
