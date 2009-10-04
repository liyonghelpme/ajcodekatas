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
        public void HasItem()
        {
            this.domain.CreateItem("NewCustomer");
            this.domain.CreateItem("OtherCustomer");

            Assert.IsTrue(this.domain.HasItem("NewCustomer"));
            Assert.IsTrue(this.domain.HasItem("OtherCustomer"));
            Assert.IsFalse(this.domain.HasItem("UnknownCustomer"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfIdIsNullInRemoveItem()
        {
            this.domain.RemoveItem(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfIdIsNullInCreateItem()
        {
            this.domain.CreateItem(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfIdIsNullInGetItem()
        {
            this.domain.GetItem(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfIdIsNullInHasItem()
        {
            this.domain.HasItem(null);
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
            this.CreateItems(10);

            IEnumerable<Item> items = this.domain.GetItems(null);

            Assert.IsNotNull(items);
            Assert.AreEqual(10, items.Count());
        }

        [TestMethod]
        public void GetItemsUsingPredicate()
        {
            this.CreateItems(10);

            IEnumerable<Item> items = this.domain.GetItems(c => (((int)c.Id) % 2)==0);

            Assert.IsNotNull(items);
            Assert.AreEqual(5, items.Count());
        }

        [TestMethod]
        public void PreserveItemsAfterQueryAndDelete()
        {
            this.CreateItems(10);

            IEnumerable<Item> items = this.domain.GetItems(c => (((int)c.Id) % 2) == 0);

            for (int k = 2; k <= 10; k += 2)
                this.domain.RemoveItem(k);

            Assert.IsNotNull(items);
            Assert.AreEqual(5, items.Count());
        }

        [TestMethod]
        public void PreserveRetrieveItemAfterQueryAndSetValue()
        {
            this.CreateItems(10);

            IEnumerable<Item> items = this.domain.GetItems(c => "Address 1".Equals(c.GetValue("Address")));

            Assert.AreEqual(1, items.Count());

            Item item = this.domain.GetItem(1);
            item.SetValue("Address", "New Address");

            Assert.AreEqual("New Address", item.GetValue("Address"));
            Assert.AreEqual("Address 1", items.First().GetValue("Address"));
        }

        [TestMethod]
        public void RemoveItem()
        {
            this.CreateItems(3);

            Assert.IsTrue(this.domain.HasItem(2));
            this.domain.RemoveItem(2);
            Assert.IsFalse(this.domain.HasItem(2));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfTryToRemoveUnknownItem()
        {
            this.domain.RemoveItem(2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfTryToGetUnknownItem()
        {
            this.domain.GetItem(2);
        }

        private void CreateItems(int nitems)
        {
            for (int k = 1; k <= nitems; k++)
            {
                Item item = this.domain.CreateItem(k);
                item.SetValue("Name", "Customer " + k);
                item.SetValue("Address", "Address " + k);
            }
        }
    }
}
