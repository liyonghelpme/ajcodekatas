namespace AjClipper.Tests
{
    using System;

    using AjClipper;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValueEnvironmentTests
    {
        [TestMethod]
        public void ShouldSetValue()
        {
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("foo", "bar");
            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void ShouldGetNullIfNoValue()
        {
            ValueEnvironment environment = new ValueEnvironment();

            Assert.IsNull(environment.GetValue("foo"));
        }

        [TestMethod]
        public void ShouldGetValueFromParent()
        {
            ValueEnvironment parent = new ValueEnvironment();
            ValueEnvironment environment = new ValueEnvironment(parent);

            parent.SetValue("foo", "bar");

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }
    }
}
