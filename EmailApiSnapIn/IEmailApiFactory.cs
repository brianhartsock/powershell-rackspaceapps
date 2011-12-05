
namespace EmailApiSnapIn
{
    /// <summary>
    /// Factory for the web client class.  Although I don't like it, the main reason this is here is I don't know a good way
    /// to inject the dependency to Powershell, so I have EmailApiCommand.Factory set as this so I can override it when testing.
    /// </summary>
    public interface IEmailApiFactory
    {
        IWebClient CreateWebClient();
    }

    /// <summary>
    /// Concrete factory returning WebClientWrapper for IWebClient factory method.
    /// </summary>
    public class EmailApiFactory : IEmailApiFactory
    {
        #region IEmailApiFactory Members

        public IWebClient CreateWebClient()
        {
            return new WebClientWrapper();
        }

        #endregion
    }
}
