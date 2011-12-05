using System.Xml;
using System.Management.Automation;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Output writer that converts the response to XML before writing it.
    /// </summary>
    public class XmlOutputWriter : IOutputWriter
    {
        #region IOutputWriter Members

        public void Write(Cmdlet cmd, string response)
        {
            var doc = new XmlDocument();
            doc.LoadXml(response);

            cmd.WriteObject(doc.DocumentElement);
        }

        #endregion
    }
}
