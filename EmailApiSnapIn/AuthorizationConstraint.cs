using System;
using System.Text;
using System.Security.Cryptography;

namespace EmailApiSnapIn
{
    public class AuthorizationConstraint : IApiConstraint
    {
        public const string DefaultUserAgent = "C# Client Library";

        string accessId;
        string secretKey;
        string userAgent;

        public AuthorizationConstraint(string _accessId, string _secretKey)
            : this(_accessId, _secretKey, DefaultUserAgent)
        { }

        public AuthorizationConstraint(string _accessId, string _secretKey, string _userAgent)
        {
            accessId = _accessId;
            secretKey = _secretKey;
            userAgent = _userAgent;
        }

        #region IApiConstraint Members

        public void ApplyConstraint(IWebClient client)
        {
            client.Headers[Headers.UserAgent] = userAgent;

            var dateTime = DateTime.UtcNow.ToString("yyyyMMddHHmmssff");

            var signature = BuildSignature(accessId, userAgent, dateTime, secretKey);

            client.Headers[Headers.Signature] = accessId + ":" + dateTime + ":" + signature;
        }

        #endregion

        private string BuildSignature(string accessId, string userAgent, string dateTime, string secretKey)
        {            
            var dataToSign = accessId + userAgent + dateTime + secretKey;
            var hash = SHA1.Create();
            var signedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            return Convert.ToBase64String(signedBytes);
        }
    }
}
