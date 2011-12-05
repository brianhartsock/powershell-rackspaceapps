using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Strategy pattern for writing output in a given cmdlet.
    /// </summary>
    public interface IOutputWriter
    {
        /// <summary>
        /// Writes the ReST request response in the given Cmdlet.
        /// </summary>
        /// <param name="cmd">The cmdlet.</param>
        /// <param name="response">The ReST response to write.</param>
        void Write(Cmdlet cmd, string response);
    }
}
