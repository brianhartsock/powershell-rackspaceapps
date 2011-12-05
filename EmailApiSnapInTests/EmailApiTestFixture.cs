using NUnit.Framework;
using Moq;
using EmailApiSnapIn;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Collections.Specialized;

namespace EmailApiSnapInTests
{
    public abstract class EmailApiTestFixture
    {

        public const string BaseUrl = "http://baseUrl/";
        public const string AccessId = "123132";
        public const string SecretKey = "4341123";

        protected WebHeaderCollection Headers { get; private set; }
        protected Runspace Runspace { get; private set; }
        protected MockFactory Mocks { get; private set; }
        protected Mock<IEmailApiFactory> factory { get; private set; }
        protected Mock<IWebClient> client { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            CreateRunspace();
            Mocks = new MockFactory(MockBehavior.Loose);
            factory = Mocks.Create<IEmailApiFactory>();

            EmailApiCommand.Factory = factory.Object;

            client = Mocks.Create<IWebClient>();
            client.SetupAllProperties();
            client.Object.Headers = (Headers = new WebHeaderCollection());

            factory.Setup(f => f.CreateWebClient())
                .Returns(client.Object)
                .AtMostOnce();
        }

        [TearDown]
        public virtual void TearDown()
        {
            CloseRunspace();
        }        
        

        private void CreateRunspace()
        {
            Runspace = RunspaceFactory.CreateRunspace(CreateConfiguration());
            Runspace.Open();
        }

        public void CloseRunspace()
        {
            Runspace.Close();
            Runspace.Dispose();
        }

        private RunspaceConfiguration CreateConfiguration()
        {
            var config = RunspaceConfiguration.Create();

            PSSnapInException warning;
            config.AddPSSnapIn("EmailApiSnapIn", out warning);

            return config;
        }
    }
}
