namespace AjProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertiesTests
    {
        [TestMethod]
        public void ShouldCreateProperties()
        {
            Properties properties = new Properties();

            Assert.IsNotNull(properties);
        }

        [TestMethod]
        public void ShouldRetrieveNullForUnknownProperty()
        {
            Properties properties = new Properties();

            Assert.IsNull(properties["foo"]);
        }

        [TestMethod]
        public void ShouldSetAndGetProperty()
        {
            Properties properties = new Properties();

            properties["foo"] = "bar";

            Assert.AreEqual("bar", properties["foo"]);
        }
    }
}
