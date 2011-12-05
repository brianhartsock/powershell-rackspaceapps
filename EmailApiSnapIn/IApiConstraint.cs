
namespace EmailApiSnapIn
{
    /// <summary>
    /// Strategy pattern for applying constraints to the WebClient.
    /// </summary>
    public interface IApiConstraint
    {
        void ApplyConstraint(IWebClient client);
    }
}
