
namespace EmailApiSnapIn
{
    public class AcceptConstraint : IApiConstraint
    {
        public static AcceptConstraint Xml()
        {
            return new AcceptConstraint("text/xml");
        }

        public static AcceptConstraint Json()
        {
            return new AcceptConstraint("application/json");
        }

        string contentType;

        private AcceptConstraint(string _contentType)
        {
            contentType = _contentType;
        }

        #region IApiConstraint Members

        public void ApplyConstraint(IWebClient client)
        {
            client.Headers[Headers.Accept] = contentType;
        }

        #endregion
    }
}
