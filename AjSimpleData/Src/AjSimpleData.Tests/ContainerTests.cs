namespace AjSimpleData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContainerTests
    {
        private Container container;

        [TestInitialize]
        public void SetupContainer()
        {
            this.container = new Container();
        }

        [TestMethod]
        public void CreateDomain()
        {
            Domain domain = this.container.CreateDomain("Domain");

            Assert.IsNotNull(domain);
            Assert.AreEqual("Domain", domain.Name);
            Assert.AreEqual(this.container, domain.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfDomainAlreadyDefined()
        {
            this.container.CreateDomain("Domain");
            this.container.CreateDomain("Domain");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfDomainNameIsNull()
        {
            this.container.CreateDomain(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfDomainNameIsEmpty()
        {
            this.container.CreateDomain(string.Empty);
        }

        [TestMethod]
        public void CreateAndGetDomain()
        {
            Domain domain = this.container.CreateDomain("Customers");

            Assert.AreEqual(domain, this.container.GetDomain("Customers"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfGetUnknownDomain()
        {
            this.container.GetDomain("Unknown");
        }

        [TestMethod]
        public void HasDomain()
        {
            Assert.IsFalse(this.container.HasDomain("Customers"));
            this.container.CreateDomain("Customers");
            Assert.IsTrue(this.container.HasDomain("Customers"));
            Assert.IsFalse(this.container.HasDomain("Suppliers"));
        }
    }
}
