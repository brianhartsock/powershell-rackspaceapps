using System.Collections.Generic;
using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Abstraction to make multiple output writers look as a single one.
    /// </summary>
    public class MacroOutputWriter : IOutputWriter
    {
        public IEnumerable<IOutputWriter> Writers { get; set; }

        #region IOutputWriter Members

        public void Write(Cmdlet cmd, string response)
        {
            foreach (var writer in Writers)
            {
                writer.Write(cmd, response);
            }
        }

        #endregion
    }
}
