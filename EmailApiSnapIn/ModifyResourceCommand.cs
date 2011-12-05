using System.Management.Automation;
using System.Collections;
using System.Collections.Specialized;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Abstract class for ReST requests that perform a modification to the system (i.e. PUT and POST).  The basic premise
    /// is any request that needs data in the body of the request should extend this.
    /// </summary>
    public abstract class ModifyResourceCommand : EmailApiCommand
    {
        public ModifyResourceCommand(string method)
            : base(method)
        {
        }

        [Parameter(Position = 1, Mandatory = true)]
        public Hashtable Data;

        protected override NameValueCollection GetData()
        {
            var nvc = new NameValueCollection();

            foreach (var key in Data.Keys)
            {
                nvc.Add(key.ToString(), Data[key].ToString());   
            }

            return nvc;
        }        
    }
}
