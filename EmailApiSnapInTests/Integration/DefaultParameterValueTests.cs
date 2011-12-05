using System;
using NUnit.Framework;
using System.Management.Automation;
using NUnit.Framework.Extensions;
using EmailApiSnapIn;
using Moq;
using System.Net;
using NUnit.Framework.Constraints;
using System.Collections.Specialized;

namespace EmailApiSnapInTests.Integration
{
    //TODO - These tests don't actually verify anything on webclient, probably fine
    public abstract class DefaultParameterValueTests : EmailApiTestFixture
    {
        string paramName;
        string variableName;
        string variableValue;

        public DefaultParameterValueTests(string _paramName, string _variableName, string _variableValue)
        {
            paramName = _paramName;
            variableName = _variableName;
            variableValue = _variableValue;
        }

        [SetUp]
        public override void Setup() 
        {
            base.Setup();

            client.Setup(w => w.DownloadString(It.IsAny<string>()))
                .Returns("<xml></xml>")
                .AtMostOnce();
            client.Setup(w => w.UploadValues(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NameValueCollection>()))
                .Returns("<xml></xml>")
                .AtMostOnce();
        }


        [RowTest]
        [Row("get-resource 'url'", ExpectedException=typeof(ArgumentNullException))]
        [Row("post-resource 'url' @{}", ExpectedException = typeof(ArgumentNullException))]
        [Row("put-resource 'url' @{}", ExpectedException = typeof(ArgumentNullException))]
        [Row("remove-resource 'url'", ExpectedException = typeof(ArgumentNullException))]
        public void Throw_error_if_no_value_set(string command)
        {
            try
            {
                Runspace.Invoke(command);
            }
            catch (CmdletInvocationException e)
            {
                throw e.InnerException ?? e;
            }

        }

        [RowTest]
        [Row("get-resource 'url'")]
        [Row("post-resource 'url' @{}")]
        [Row("put-resource 'url' @{}")]
        [Row("remove-resource 'url'")]
        public void Verify_using_value_on_cmdlet_works(string cmd)
        {
            Runspace.Invoke("{0} -{1} {2}", cmd, paramName, variableValue);

            Headers.AssertUserAgent();
            Headers.AssertAuthorization(AccessId, SecretKey);
        }



        [RowTest]
        [Row("get-resource 'url'")]
        [Row("post-resource 'url' @{}")]
        [Row("put-resource 'url' @{}")]
        [Row("remove-resource 'url'")]
        public void Use_variable_for_value(string cmd)
        {
            Runspace.SessionStateProxy.SetVariable(variableName, variableValue);
            Runspace.Invoke(cmd);

            Headers.AssertUserAgent();
            Headers.AssertAuthorization(AccessId, SecretKey);
        }
    }
}