namespace AjTwitter.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void ShouldCreateUser()
        {
            User user = new User("test");

            Assert.IsNotNull(user);
            Assert.AreEqual(0, user.MessagesCount);
            Assert.AreEqual(0, user.FollowersCount);
            Assert.AreEqual(0, user.FollowingCount);
        }

        [TestMethod]
        public void ShouldAddFollower()
        {
            User user1 = new User("test1");
            User user2 = new User("test2");

            user1.AddFollower(user2);

            Assert.AreEqual(1, user1.FollowersCount);
            Assert.AreEqual(1, user2.FollowingCount);

            Assert.AreEqual(user2, user1.Followers.First());
            Assert.AreEqual(user1, user2.Following.First());
        }

        [TestMethod]
        public void ShouldAddAndRetriveMessage()
        {
            User user = new User("test1");

            user.NewMessage("message");

            Assert.AreEqual(1, user.MessagesCount);

            Message message = user.Messages.First();

            Assert.IsNotNull(message);
            Assert.AreEqual("message", message.Text);
            Assert.AreEqual(user, message.User);
        }

        [TestMethod]
        public void ShouldGetOneOwnMessage()
        {
            User user = new User("test1");

            user.NewMessage("message");

            Assert.AreEqual(1, user.MessagesCount);

            int n = 0;

            foreach (Message message in user.GetMessages(10))
            {
                Assert.AreEqual("message", message.Text);
                Assert.AreEqual(user, message.User);
                n++;
            }

            Assert.AreEqual(1, n);
        }

        [TestMethod]
        public void ShouldGetOneHundredOwnMessages()
        {
            User user = new User("test1");

            for (int k = 1; k <= 100; k++)
                user.NewMessage("message" + k);

            Assert.AreEqual(100, user.MessagesCount);

            int n = 0;

            foreach (Message message in user.GetMessages(100))
            {
                Assert.AreEqual("message" + (100 - n), message.Text);
                Assert.AreEqual(user, message.User);
                n++;
            }

            Assert.AreEqual(100, n);
        }

        [TestMethod]
        public void ShouldGetOneFollowingMessage()
        {
            User user = new User("test1");
            User following = new User("following");

            following.AddFollower(user);

            following.NewMessage("message");

            int n = 0;

            foreach (Message message in user.GetMessages(10))
            {
                Assert.AreEqual("message", message.Text);
                Assert.AreEqual(following, message.User);
                n++;
            }

            Assert.AreEqual(1, n);
        }

        [TestMethod]
        public void ShouldGetOneHundredFollowingMessages()
        {
            User user = new User("test1");
            User following = new User("following");

            following.AddFollower(user);

            for (int k = 1; k <= 100; k++)
                following.NewMessage("message" + k);

            int n = 0;

            foreach (Message message in user.GetMessages(100))
            {
                Assert.AreEqual("message" + (100 - n), message.Text);
                Assert.AreEqual(following, message.User);
                n++;
            }

            Assert.AreEqual(100, n);
        }

        [TestMethod]
        public void ShouldGetOneHundredMessages()
        {
            User[] users = new User[10];

            for (int k = 0; k < users.Length; k++)
            {
                users[k] = new User("user" + k);
                if (k > 0)
                    users[k].AddFollower(users[0]);
            }

            for (int j = 0; j < 10; j++)
                for (int k = 0; k < users.Length; k++)
                    users[k].NewMessage("message" + j);

            int n = 0;
            DateTime dateTime = new DateTime();

            foreach (Message message in users[0].GetMessages(100))
            {
                Assert.IsTrue(dateTime >= message.DateTime);
                dateTime = message.DateTime;
                n++;
            }

            Assert.AreEqual(100, n);
        }
    }
}
