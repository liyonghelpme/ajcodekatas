namespace AjProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjProcessor.Utilities;

    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void ShouldGetApplicationName()
        {
            Assert.AreEqual("Application", ToAddressUtilities.GetApplicationName("Application/Processor"));
        }

        [TestMethod]
        public void ShouldGetCurrentApplicationName()
        {
            Assert.AreEqual(".", ToAddressUtilities.GetApplicationName("Processor"));
        }

        [TestMethod]
        public void ShouldGetProcessorName()
        {
            Assert.AreEqual("Processor", ToAddressUtilities.GetProcessorName("Application/Processor"));
        }

        [TestMethod]
        public void ShouldGetProcessorNameWithoutApplication()
        {
            Assert.AreEqual("Processor", ToAddressUtilities.GetProcessorName("Processor"));
        }
    }
}
