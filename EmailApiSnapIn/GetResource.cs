using System.Management.Automation;
using System.Collections;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Concrete Cmdlet for GET requests to the API.
    /// </summary>
    [Cmdlet("Get", "Resource", SupportsShouldProcess=true)]
    public class GetResource : EmailApiCommand
    {
        public GetResource()
            : base("GET")
        {
        }

        [Parameter(Position = 1)]
        public Hashtable Query;

        protected override string BuildUrl()
        {
            return base.BuildUrl() + GetQueryString();
        }

        private string GetQueryString()
        {
            if (Query != null)
            {
                return Query.ToQueryString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
