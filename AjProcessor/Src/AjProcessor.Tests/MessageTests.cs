namespace AjProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void ShouldCreateMessageWithoutParameters()
        {
            Message message = new Message();

            Assert.IsNotNull(message);
            Assert.IsNull(message.Payload);
            Assert.IsNull(message.Action);
            Assert.IsNull(message.To);
        }

        [TestMethod]
        public void ShouldCreateMessageWithPayload()
        {
            object payload = new TestPayload();
            Message message = new Message(payload);

            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Payload);
            Assert.AreEqual(payload, message.Payload);
            Assert.IsNull(message.Action);
            Assert.IsNull(message.To);
        }

        [TestMethod]
        public void ShouldCreateMessageWithPayloadAndAction()
        {
            object payload = new TestPayload();
            Message message = new Message(payload, "Action");

            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Payload);
            Assert.AreEqual(payload, message.Payload);
            Assert.IsNotNull(message.Action);
            Assert.AreEqual("Action", message.Action);
            Assert.IsNull(message.To);
        }

        [TestMethod]
        public void ShouldCreateMessageWithPayloadActionAndTo()
        {
            object payload = new TestPayload();
            Message message = new Message(payload, "Action", "Processor");

            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Payload);
            Assert.AreEqual(payload, message.Payload);
            Assert.IsNotNull(message.Action);
            Assert.AreEqual("Action", message.Action);
            Assert.IsNotNull(message.To);
            Assert.AreEqual("Processor", message.To);
        }
    }
}
