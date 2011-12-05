using NUnit.Framework;
using EmailApiSnapIn;

namespace EmailApiSnapInTests.Integration
{
    [TestFixture]
    public class AccessIdParameterTests : DefaultParameterValueTests
    {
        public AccessIdParameterTests()
            : base("AccessId", EmailApiCommand.AccessIdVariable, AccessId)
        {
        }
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Runspace.SetVariable(EmailApiCommand.SecretKeyVariable, SecretKey);
            Runspace.SetVariable(EmailApiCommand.BaseUrlVariable, "base");
        }
    }
}
