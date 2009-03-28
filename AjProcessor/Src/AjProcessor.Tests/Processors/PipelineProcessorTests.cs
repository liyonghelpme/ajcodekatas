namespace AjProcessor.Tests.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjProcessor.Processors;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PipelineProcessorTests
    {
        [TestMethod]
        public void ShouldCreatePipelineProcessor()
        {
            PipelineProcessor processor = new PipelineProcessor();

            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void ShouldCreateAndProcessOneStepPipeline()
        {
            PipelineProcessor processor = new PipelineProcessor();
            TestProcessor testProcessor = new TestProcessor();
            TestProcessor receiverProcessor = new TestProcessor();
            ICollection<IProcessor> processors = new List<IProcessor>();
            processors.Add(testProcessor);
            processor.RegisterProcessor(receiverProcessor);
            processor.Processors = processors;

            processor.ProcessMessage(new Message("foo"));

            testProcessor.AutoEvent.WaitOne();
            receiverProcessor.AutoEvent.WaitOne();

            Assert.IsNotNull(testProcessor.ProcessedMessage);
            Assert.AreEqual("foo", testProcessor.ProcessedMessage.Payload);
            Assert.IsNotNull(receiverProcessor.ProcessedMessage);
            Assert.AreEqual("foo", receiverProcessor.ProcessedMessage.Payload);
        }

        [TestMethod]
        public void ShouldCreateAndProcessTwoStepsPipeline()
        {
            PipelineProcessor processor = new PipelineProcessor();
            TestProcessor testProcessor1 = new TestProcessor();
            TestProcessor testProcessor2 = new TestProcessor();
            TestProcessor receiverProcessor = new TestProcessor();
            ICollection<IProcessor> processors = new List<IProcessor>();
            processors.Add(testProcessor1);
            processors.Add(testProcessor2);
            processor.RegisterProcessor(receiverProcessor);
            processor.Processors = processors;

            processor.ProcessMessage(new Message("foo"));

            testProcessor1.AutoEvent.WaitOne();
            testProcessor2.AutoEvent.WaitOne();
            receiverProcessor.AutoEvent.WaitOne();

            Assert.IsNotNull(testProcessor1.ProcessedMessage);
            Assert.AreEqual("foo", testProcessor1.ProcessedMessage.Payload);
            Assert.IsNotNull(testProcessor2.ProcessedMessage);
            Assert.AreEqual("foo", testProcessor2.ProcessedMessage.Payload);
            Assert.IsNotNull(receiverProcessor.ProcessedMessage);
            Assert.AreEqual("foo", receiverProcessor.ProcessedMessage.Payload);
        }
    }
}
