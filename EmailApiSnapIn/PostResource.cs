using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Concrete Cmdlet for POST requests to the API.
    /// </summary>
    [Cmdlet("Post", "Resource", SupportsShouldProcess=true)]
    public class PostResource : ModifyResourceCommand
    {
        public PostResource()
            : base("POST")
        { }
    }
}
