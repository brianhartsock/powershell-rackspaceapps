using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Simple output writer that writes the response as verbose output.
    /// </summary>
    public class VerboseOutputWriter : IOutputWriter
    {
        #region IOutputWriter Members

        public void Write(Cmdlet cmd, string response)
        {
            cmd.WriteVerbose(response);
        }

        #endregion
    }

}
