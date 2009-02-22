namespace AjLambda.Tests
{
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjLambda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnvironmentTests
    {
        [TestMethod]
        public void ShouldCreate()
        {
            Environment environment = new Environment();

            Assert.IsNotNull(environment);
        }

        [TestMethod]
        public void ShouldReturnNullIfUnknownName()
        {
            Environment environment = new Environment();

            Assert.IsNull(environment.GetValue("foo"));
        }

        [TestMethod]
        public void ShouldSaveValue()
        {
            Environment environment = new Environment();

            environment.DefineValue("foo", new Variable("x"));

            Expression value = environment.GetValue("foo");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Variable));
            Assert.AreEqual("x", value.ToString());
        }
    }
}
