namespace AjSimpleData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ItemTests
    {
        private Container container;
        private Domain domain;
        private Item item;

        [TestInitialize]
        public void Setup()
        {
            this.container = new Container();
            this.domain = this.container.CreateDomain("Customers");
            this.item = this.domain.CreateItem(1);
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            this.item.SetValue("Name", "Customer 1");
            Assert.AreEqual("Customer 1", this.item.GetValue("Name"));
        }

        [TestMethod]
        public void ResetValue()
        {
            this.item.SetValue("Name", "Customer 1");
            this.item.SetValue("Name", "Customer Name");
            Assert.AreEqual("Customer Name", this.item.GetValue("Name"));
        }

        [TestMethod]
        public void GetNullIfNoValueDefined()
        {
            Assert.IsNull(this.item.GetValue("NoField"));
        }

        [TestMethod]
        public void GetNames()
        {
            this.item.SetValue("Name", "Customer Name");
            this.item.SetValue("Address", "Customer Address");

            ICollection<string> names = this.item.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(2, names.Count);
            Assert.IsTrue(names.Contains("Name"));
            Assert.IsTrue(names.Contains("Address"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfValueIsNull()
        {
            this.item.SetValue("Name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfValueHasNotSupportedType()
        {
            this.item.SetValue("Address", new { Street = "Street", City = "City" });
        }

        [TestMethod]
        public void SetAndRemoveValue()
        {
            this.item.SetValue("Name", "Customer Name");

            Assert.IsNotNull(this.item.GetValue("Name"));

            this.item.RemoveValue("Name");

            Assert.IsNull(this.item.GetValue("Name"));
        }

        [TestMethod]
        public void SetMultipleValues()
        {
            this.item.AddValue("Addresses", "Address 1");
            this.item.AddValue("Addresses", "Address 2");

            IList<object> addresses = this.item.GetValues("Addresses");

            Assert.IsNotNull(addresses);
            Assert.AreEqual(2, addresses.Count);
            Assert.IsTrue(addresses.Contains("Address 1"));
            Assert.IsTrue(addresses.Contains("Address 2"));
        }

        [TestMethod]
        public void RemoveUnknownValue()
        {
            this.item.RemoveValue("Address", "Address 1");
            Assert.IsNull(this.item.GetValue("Address"));
        }

        [TestMethod]
        public void RemoveMultipleValue()
        {
            this.item.AddValue("Addresses", "Address 1");
            this.item.AddValue("Addresses", "Address 2");
            this.item.AddValue("Addresses", "Address 3");
            this.item.RemoveValue("Addresses", "Address 1");

            IList<object> addresses = this.item.GetValues("Addresses");

            Assert.IsNotNull(addresses);
            Assert.AreEqual(2, addresses.Count);
            Assert.IsTrue(addresses.Contains("Address 2"));
            Assert.IsTrue(addresses.Contains("Address 3"));
        }

        [TestMethod]
        public void GetValueAsList()
        {
            this.item.SetValue("Name", "Customer Name");

            ICollection<object> values = this.item.GetValues("Name");

            Assert.IsNotNull(values);
            Assert.AreEqual(1, values.Count);
            Assert.IsTrue(values.Contains("Customer Name"));
        }
    }
}
