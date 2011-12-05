using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Constraints;
using System.Net;
using EmailApiSnapIn;
using System.Security.Cryptography;
using NUnit.Framework;

namespace EmailApiSnapInTests
{
    public class SignatureConstraint : Constraint
    {
        string accessId;
        string secretKey;

        Queue<Action<MessageWriter>> messages;

        public SignatureConstraint(string _accessId, string _secretKey)
        {
            accessId = _accessId;
            secretKey = _secretKey;
            messages = new Queue<Action<MessageWriter>>();
        }

        public override bool Matches(object actual)
        {
            var headers = (WebHeaderCollection)actual;

            var tokens = headers[Headers.Signature].Split(':');

            if (tokens.Length != 3)
            {
                messages.Enqueue((w) => w.Write("Invalid number of tokens in header"));
                return false;
            }

            var _accessId = tokens[0];

            if (accessId != null && accessId != _accessId)
            {
                messages.Enqueue((w) => w.WriteExpectedValue(accessId));
                messages.Enqueue((w) => w.WriteActualValue(_accessId));
                return false;
            }
            var datetime = tokens[1];
            var signature = tokens[2];
            var userAgent = headers[Headers.UserAgent];

            return signature == BuildSignature(accessId, userAgent, datetime, secretKey);
        }

        //...This is an exact copy of the AuthorizationConstraint function? Problematic?  Nah
        private string BuildSignature(string accessId, string userAgent, string dateTime, string secretKey)
        {
            var dataToSign = accessId + userAgent + dateTime + secretKey;
            var hash = SHA1.Create();
            var signedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            return Convert.ToBase64String(signedBytes);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            foreach (var msg in messages)
            {
                msg.Invoke(writer);
            }
        }
    }
}
