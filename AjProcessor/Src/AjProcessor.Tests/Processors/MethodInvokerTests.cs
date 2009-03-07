namespace AjProcessor.Tests.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjProcessor.Processors;

    [TestClass]
    public class MethodInvokerTests
    {
        [TestMethod]
        public void ShouldCreateMethodInvoker()
        {
            MethodInvoker processor = new MethodInvoker();

            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void ShouldInvokePublicMethod()
        {
            TestObject testObject = new TestObject();
            MethodInvoker processor = new MethodInvoker() { Object = testObject, MethodName = "Process" };

            processor.ProcessMessage(new Message("foo"));

            Assert.IsNotNull(testObject.ProcessedString);
            Assert.AreEqual("foo", testObject.ProcessedString);
        }

        [TestMethod]
        public void ShouldInvokePrivateMethod()
        {
            TestObject testObject = new TestObject();
            MethodInvoker processor = new MethodInvoker() { Object = testObject, MethodName = "PrivateProcess" };

            processor.ProcessMessage(new Message("foo"));

            Assert.IsNotNull(testObject.ProcessedString);
            Assert.AreEqual("foo", testObject.ProcessedString);
        }

        [TestMethod]
        public void ShouldInvokeIncrementMethod()
        {
            TestObject testObject = new TestObject();
            MethodInvoker processor = new MethodInvoker() { Object = testObject, MethodName = "Increment" };
            TestProcessor receiverProcessor = new TestProcessor();

            processor.ForwardMessage += receiverProcessor.ProcessMessage;

            processor.ProcessMessage(new Message(1));

            Assert.IsNotNull(testObject.ProcessedInteger);
            Assert.AreEqual(1, testObject.ProcessedInteger);
            Assert.IsNotNull(receiverProcessor.ProcessedMessage);
            Assert.AreEqual(2, receiverProcessor.ProcessedMessage.Payload);
        }
    }
}
