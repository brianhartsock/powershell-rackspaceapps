using NUnit.Framework;
using EmailApiSnapIn;

namespace EmailApiSnapInTests.Integration
{
    [TestFixture]
    public class SecretKeyParameterTests : DefaultParameterValueTests
    {
        public SecretKeyParameterTests()
            : base("SecretKey", EmailApiCommand.SecretKeyVariable, SecretKey)
        {
        }
        public override void Setup()
        {
            base.Setup();

            Runspace.SetVariable(EmailApiCommand.AccessIdVariable, AccessId);
            Runspace.SetVariable(EmailApiCommand.BaseUrlVariable, "base");
        }
    }
}
