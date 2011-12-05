using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using EmailApiSnapIn;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;

namespace EmailApiSnapInTests
{
    public static class WebHeaderCollectionExtensions
    {
        public static void AssertUserAgent(this WebHeaderCollection headers)
        {
            Assert.That(headers[Headers.UserAgent], Is.Not.Null);
            Assert.That(headers[Headers.UserAgent], Is.EqualTo(AuthorizationConstraint.DefaultUserAgent));
        }

        public static void AssertAuthorization(this WebHeaderCollection headers, string accessId, string secretKey)
        {
            Assert.That(headers, new SignatureConstraint(accessId, secretKey));
        }
    }
}
