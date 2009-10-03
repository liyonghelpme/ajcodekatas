namespace AjSimpleData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DomainTests
    {
        private Container container;
        private Domain domain;

        [TestInitialize]
        public void Setup()
        {
            this.container = new Container();
            this.domain = container.CreateDomain("Customers");
        }

        [TestMethod]
        public void CreateItemWithIntegerId()
        {
            Item item = this.domain.CreateItem(1);

            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual(this.domain, item.Domain);
        }

        [TestMethod]
        public void CreateItemWithStringId()
        {
            Item item = this.domain.CreateItem("Customer1");

            Assert.IsNotNull(item);
            Assert.AreEqual("Customer1", item.Id);
            Assert.AreEqual(this.domain, item.Domain);
        }

        [TestMethod]
        public void CreateAndGetItem()
        {
            Item item = this.domain.CreateItem("NewCustomer");

            Assert.AreEqual(item, this.domain.GetItem("NewCustomer"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfItemAlreadyExists()
        {
            this.domain.CreateItem("Customer1");
            this.domain.CreateItem("Customer1");
        }

        [TestMethod]
        public void GetAllItems()
        {
            for (int k = 1; k <= 10; k++)
                this.domain.CreateItem(k);

            IEnumerable<Item> items = this.domain.GetItems(null);

            Assert.IsNotNull(items);
            Assert.AreEqual(10, items.Count());
        }

        [TestMethod]
        public void GetItemsUsingPredicate()
        {
            for (int k = 1; k <= 10; k++)
                this.domain.CreateItem(k);

            IEnumerable<Item> items = this.domain.GetItems(c => (((int)c.Id) % 2)==0);

            Assert.IsNotNull(items);
            Assert.AreEqual(5, items.Count());
        }
    }
}
