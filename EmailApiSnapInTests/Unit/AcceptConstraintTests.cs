using NUnit.Framework;
using EmailApiSnapIn;

namespace EmailApiSnapInTests.Unit
{
    [TestFixture]
    public class AcceptConstraintTests
    {
        IWebClient client;

        [SetUp]
        public void Setup()
        {
            client = new WebClientWrapper();
        }

        [Test]
        public void Use_xml_constraint()
        {
            AcceptConstraint.Xml()
                .ApplyConstraint(client);

            Assert.AreEqual(client.Headers[Headers.Accept], "text/xml");
        }

        [Test]
        public void Use_json_constraint()
        {
            AcceptConstraint.Json()
                .ApplyConstraint(client);

            Assert.AreEqual(client.Headers[Headers.Accept], "application/json");
        }
    }
}
