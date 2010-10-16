using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Proxy;

namespace Patterns.Tests.Proxy
{
    [TestClass]
    public class CustomerProxyTests
    {
        [TestMethod]
        public void GetCustomer()
        {
            ICustomer customer = new CustomerProxy(1);
            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Customer 1", customer.Name);
            Assert.AreEqual("Address 1", customer.Address);
            Assert.AreEqual("Notes 1", customer.Notes);
        }

        [TestMethod]
        public void GetAndReGetCustomer()
        {
            ICustomer customer = new CustomerProxy(1);

            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Customer 1", customer.Name);
            Assert.AreEqual("Address 1", customer.Address);
            Assert.AreEqual("Notes 1", customer.Notes);

            customer.Id = 2;

            Assert.AreEqual(2, customer.Id);
            Assert.AreEqual("Customer 2", customer.Name);
            Assert.AreEqual("Address 2", customer.Address);
            Assert.AreEqual("Notes 2", customer.Notes);
        }
    }
}
