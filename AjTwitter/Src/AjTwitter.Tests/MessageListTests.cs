namespace AjTwitter.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MessageListTests
    {
        [TestMethod]
        public void ShouldCreateMessageMessageList()
        {
            MessageList messages = new MessageList();

            Assert.IsNotNull(messages);
            Assert.AreEqual(0, messages.Count);

            int n = 0;

            foreach (Message message in messages.Elements)
                n++;

            Assert.AreEqual(0, n);
        }

        [TestMethod]
        public void ShouldCreateMessageMessageListWithOneHundredMessages()
        {
            MessageList list = new MessageList();

            Message[] messages = new Message[100];

            User user = new User("test");

            for (int k = 0; k < messages.Length; k++)
            {
                messages[k] = new Message(user, "message" + k);
                list.Add(messages[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(messages.Length, list.Count);

            int n = messages.Length;

            foreach (Message message in list.Elements)
                Assert.AreEqual(messages[--n].Text, message.Text);

            Assert.AreEqual(0, n);
        }

        [TestMethod]
        public void ShouldCreateMessageMessageListWithTenThousandMessages()
        {
            MessageList list = new MessageList();

            Message[] messages = new Message[10000];

            User user = new User("test");

            for (int k = 0; k < messages.Length; k++)
            {
                messages[k] = new Message(user, "message" + k);
                list.Add(messages[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(messages.Length, list.Count);

            int n = messages.Length;

            foreach (Message message in list.Elements)
                Assert.AreEqual(messages[--n].Text, message.Text);

            Assert.AreEqual(0, n);
        }

        [TestMethod]
        public void ShouldCreateAndEmptyBigMessageMessageList()
        {
            MessageList list = new MessageList();

            Message[] messages = new Message[10000];

            User user = new User("test");

            for (int k = 0; k < messages.Length; k++)
            {
                messages[k] = new Message(user, "message" + k);
                list.Add(messages[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(messages.Length, list.Count);

            foreach (Message message in list.Elements)
                list.Remove(message);

            Assert.AreEqual(0, list.Count);
        }
    }
}
