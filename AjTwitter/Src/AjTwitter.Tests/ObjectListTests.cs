namespace AjTwitter.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectListTests
    {
        [TestMethod]
        public void ShouldCreateUserObjectList()
        {
            ObjectList<User> users = new ObjectList<User>();

            Assert.IsNotNull(users);
            Assert.AreEqual(0, users.Count);

            int n = 0;

            foreach (User user in users.Elements)
                n++;

            Assert.AreEqual(0, n);
        }

        [TestMethod]
        public void ShouldCreateUserObjectListWithOneHundredUsers()
        {
            ObjectList<User> list = new ObjectList<User>();

            User[] users = new User[100];

            for (int k = 0; k < users.Length; k++)
            {
                users[k] = new User("user" + k);
                list.Add(users[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(users.Length, list.Count);

            int n = 0;

            foreach (User user in list.Elements)
                Assert.AreEqual(users[n++].Name, user.Name);

            Assert.AreEqual(users.Length, n);
        }

        [TestMethod]
        public void ShouldCreateUserObjectListWithTenThousandUsers()
        {
            ObjectList<User> list = new ObjectList<User>();

            User[] users = new User[10000];

            for (int k = 0; k < users.Length; k++)
            {
                users[k] = new User("user" + k);
                list.Add(users[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(users.Length, list.Count);

            int n = 0;

            foreach (User user in list.Elements)
                Assert.AreEqual(users[n++].Name, user.Name);

            Assert.AreEqual(users.Length, n);
        }

        [TestMethod]
        public void ShouldCreateAndEmptyBigUserObjectList()
        {
            ObjectList<User> list = new ObjectList<User>();

            User[] users = new User[10000];

            for (int k = 0; k < users.Length; k++)
            {
                users[k] = new User("user" + k);
                list.Add(users[k]);
            }

            Assert.IsNotNull(list);
            Assert.AreEqual(users.Length, list.Count);

            foreach (User user in list.Elements)
                list.Remove(user);

            Assert.AreEqual(0, list.Count);
        }
    }
}
