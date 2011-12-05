using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Concrete Cmdlet for PUT requests to the API.
    /// </summary>
    [Cmdlet("Put", "Resource", SupportsShouldProcess=true)]
    public class PutResource : ModifyResourceCommand
    {
        public PutResource()
            : base("PUT")
        {
        }
    }
}
