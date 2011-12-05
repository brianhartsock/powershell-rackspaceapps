using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;

namespace EmailApiSnapInTests
{
    /// <summary>
    /// Wrist saving extension methods on a Posh Runspace.
    /// </summary>
    public static class RunspaceExtensions
    {
        public static Collection<PSObject> Invoke(this Runspace rs, string cmd)
        {
            return rs.CreatePipeline(cmd).Invoke();
        }

        public static Collection<PSObject> Invoke(this Runspace rs, string cmd, params object[] objs)
        {
            return rs.CreatePipeline(string.Format(cmd, objs)).Invoke();
        }

        public static void SetVariable(this Runspace rs, string name, object value)
        {
            rs.SessionStateProxy.SetVariable(name, value);
        }
    }
}
