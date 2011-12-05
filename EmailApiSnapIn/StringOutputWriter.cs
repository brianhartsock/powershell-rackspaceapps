using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Simple output writer that just writes the response to the pipeline.
    /// </summary>
    public class StringOutputWriter : IOutputWriter
    {
        #region IOutputWriter Members

        public void Write(Cmdlet cmd, string response)
        {
            cmd.WriteObject(response);
        }

        #endregion
    }
}
