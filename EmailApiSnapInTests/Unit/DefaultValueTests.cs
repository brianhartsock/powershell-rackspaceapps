using NUnit.Framework;
using EmailApiSnapIn;
using NUnit.Framework.SyntaxHelpers;

namespace EmailApiSnapInTests.Unit
{
    [TestFixture]
    public class DefaultValueTests : EmailApiTestFixture
    {        
        [Test]
        public void Return_null_if_value_is_never_set_with_no_default()
        {
            var variable = new DefaultValue<string>();

            Assert.That(variable.Value, Is.Null);
        }

        [Test]
        public void Return_value_set_if_vaue_is_set_with_no_default()
        {
            var variable = new DefaultValue<string>();
            variable.Value = "1";

            Assert.That(variable.Value, Is.EqualTo("1"));
        }

        [Test]
        public void Return_default_value_if_value_is_never_set()
        {
            var variable = new DefaultValue<string>(() => "2");

            Assert.That(variable.Value, Is.EqualTo("2"));
        }

        [Test]
        public void Return_second_default_if_nothing_else_is_valid()
        {
            var variable = new DefaultValue<string>(() => null, () => "2");

            Assert.That(variable.Value, Is.EqualTo("2"));
        }

        [Test]
        public void Return_value_even_if_all_defaults_are_value()
        {
            var variable = new DefaultValue<string>(() => "2", () => "3");
            variable.Value = "1";

            Assert.That(variable.Value, Is.EqualTo("1"));
        }
    }
}
