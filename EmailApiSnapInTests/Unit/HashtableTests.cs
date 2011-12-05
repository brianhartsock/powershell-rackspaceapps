using NUnit.Framework;
using System.Collections;
using EmailApiSnapIn;
using NUnit.Framework.SyntaxHelpers;
using System;

namespace EmailApiSnapInTests.Unit
{
    [TestFixture]
    public class HashtableTests
    {
        Hashtable hash;

        [SetUp]
        public void Setup()
        {
            hash = new Hashtable();
        }

        [Test]
        public void Basic_test_with_strings()
        {
            hash["str1"] = "val1";
            hash["str2"] = "val2";

            Assert.That(hash.ToQueryString(), Is.EqualTo("?str1=val1&str2=val2"));
        }

        [Test]
        public void Test_with_ints()
        {
            hash["str1"] = 1;
            hash["str2"] = 2;

            Assert.That(hash.ToQueryString(), Is.EqualTo("?str1=1&str2=2"));
        }

        [Test]
        public void Just_one_value()
        {
            hash["one"] = "val";

            Assert.That(hash.ToQueryString(), Is.EqualTo("?one=val"));
        }

        [Test]
        public void Null_value_is_empty_string()
        {
            hash["null"] = null;

            Assert.That(hash.ToQueryString(), Is.EqualTo("?null="));
        }

        [Test]
        public void One_good_and_one_null_value()
        {
            hash["one"] = "value";
            hash["two"] = null;

            Assert.That(hash.ToQueryString(), Is.EqualTo("?one=value&two="));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Argument_null_exception()
        {
            var nullHash = (Hashtable)null;

            nullHash.ToQueryString();
        }
    }
}
