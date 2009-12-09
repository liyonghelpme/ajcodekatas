namespace AjClipper.Tests
{
    using System;

    using AjClipper;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValueEnvironmentTests
    {
        [TestMethod]
        public void CreateEnvironmentAsNormal()
        {
            ValueEnvironment environment = new ValueEnvironment();

            Assert.AreEqual(ValueEnvironmentType.Normal, environment.Type);
        }

        [TestMethod]
        public void CreateEnvironmentAsLocal()
        {
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Local);

            Assert.AreEqual(ValueEnvironmentType.Local, environment.Type);
            Assert.IsNull(environment.GetNonLocalEnvironment());
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            ValueEnvironment environment = new ValueEnvironment();

            environment.SetValue("foo", "bar");
            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void GetNullIfNoValue()
        {
            ValueEnvironment environment = new ValueEnvironment();

            Assert.IsNull(environment.GetValue("foo"));
        }

        [TestMethod]
        public void GetValueFromParent()
        {
            ValueEnvironment parent = new ValueEnvironment();
            ValueEnvironment environment = new ValueEnvironment(parent);

            parent.SetValue("foo", "bar");

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void SetAndGetPublicValue()
        {
            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment environment = new ValueEnvironment(parent);

            environment.SetPublicValue("foo", "bar");

            Assert.AreEqual("bar", environment.GetValue("foo"));
            Assert.AreEqual("bar", parent.GetValue("foo"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNoPublicEnvironmentInSetPublicValue()
        {
            ValueEnvironment parent = new ValueEnvironment();
            ValueEnvironment environment = new ValueEnvironment(parent);

            environment.SetPublicValue("foo", "bar");
        }

        [TestMethod]
        public void CreateAndSkipLocalEnvironment()
        {
            ValueEnvironment environment = new ValueEnvironment();
            ValueEnvironment local = new ValueEnvironment(environment, ValueEnvironmentType.Local);

            Assert.AreEqual(ValueEnvironmentType.Normal, environment.Type);
            Assert.AreEqual(ValueEnvironmentType.Local, local.Type);

            Assert.AreEqual(environment, local.GetNonLocalEnvironment());
            Assert.AreEqual(environment, environment.GetNonLocalEnvironment());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNoPublicEnvironmentInGetPublicEnvironment()
        {
            ValueEnvironment parent = new ValueEnvironment();
            ValueEnvironment environment = new ValueEnvironment(parent);

            environment.GetPublicEnvironment();
        }

        [TestMethod]
        public void GetPublicEnvironment()
        {
            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment environment = new ValueEnvironment(parent);

            Assert.AreEqual(parent, environment.GetPublicEnvironment());
            Assert.AreEqual(parent, parent.GetPublicEnvironment());
        }

        [TestMethod]
        public void DefineSetAndGetPublicValue()
        {
            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment environment = new ValueEnvironment(parent);

            environment.SetPublicValue("foo", null);

            environment.SetValue("foo", "bar");
            environment.SetValue("one", 1);

            Assert.AreEqual("bar", environment.GetValue("foo"));
            Assert.AreEqual("bar", parent.GetValue("foo"));

            Assert.AreEqual(1, environment.GetValue("one"));
            Assert.IsNull(parent.GetValue("one"));
        }

        [TestMethod]
        public void DefineSetAndGetLocalValue()
        {
            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment environment = new ValueEnvironment(parent, ValueEnvironmentType.Local);

            environment.SetLocalValue("foo", null);

            environment.SetValue("foo", "bar");

            Assert.AreEqual("bar", environment.GetValue("foo"));
            Assert.IsNull(parent.GetValue("foo"));
        }
    }
}
