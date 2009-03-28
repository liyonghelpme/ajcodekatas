namespace AjProcessor.Tests.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Processors;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseProcessorTests
    {
        [TestMethod]
        public void ShouldCreateBaseProcessor()
        {
            BaseProcessor processor = new BaseProcessor();

            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void ShouldProcessMessage()
        {
            BaseProcessor processor = new BaseProcessor();

            TestProcessor testProcessor = new TestProcessor();

            processor.RegisterProcessor(testProcessor);

            Message message = new Message("foo");

            processor.ProcessMessage(message);

            Assert.IsNotNull(testProcessor.ProcessedMessage);
            Assert.AreEqual(message, testProcessor.ProcessedMessage);
        }
    }
}
