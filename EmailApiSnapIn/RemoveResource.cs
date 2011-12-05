using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Concrete Cmdlet for DELETE requests to the API.
    /// </summary>
    [Cmdlet("Remove", "Resource", SupportsShouldProcess=true)]
    public class RemoveResource : EmailApiCommand
    {
        public RemoveResource()
            : base("DELETE")
        {
        }
    }
}
