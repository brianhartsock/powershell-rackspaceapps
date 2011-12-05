using NUnit.Framework;
using EmailApiSnapIn;

namespace EmailApiSnapInTests.Integration
{
    [TestFixture]
    public class BaseUrlParameterTests : DefaultParameterValueTests
    {
        public BaseUrlParameterTests()
            : base("BaseUrl", EmailApiCommand.BaseUrlVariable, "base")
        {
        }
        public override void Setup()
        {
            base.Setup();

            Runspace.SetVariable(EmailApiCommand.AccessIdVariable, AccessId);
            Runspace.SetVariable(EmailApiCommand.SecretKeyVariable, SecretKey);
        }
    }
}
