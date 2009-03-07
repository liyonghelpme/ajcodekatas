namespace AjProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Providers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProvidersTests
    {
        [TestMethod]
        public void ShouldGetApplicationName()
        {
            ApplicationRouteProvider provider = new ApplicationRouteProvider();

            Assert.AreEqual("Application", provider.GetRoute(new Message("foo", "Action", "Application/Processor")));
        }

        [TestMethod]
        public void ShouldGetProcessorName()
        {
            ProcessorRouteProvider provider = new ProcessorRouteProvider();

            Assert.AreEqual("Processor", provider.GetRoute(new Message("foo", "Action", "Application/Processor")));
        }

        [TestMethod]
        public void ShouldGetToAddress()
        {
            ToRouteProvider provider = new ToRouteProvider();

            Assert.AreEqual("Application/Processor", provider.GetRoute(new Message("foo", "Action", "Application/Processor")));
        }

        [TestMethod]
        public void ShouldGetAction()
        {
            ActionRouteProvider provider = new ActionRouteProvider();

            Assert.AreEqual("Action", provider.GetRoute(new Message("foo", "Action", "Application/Processor")));
        }
    }
}
