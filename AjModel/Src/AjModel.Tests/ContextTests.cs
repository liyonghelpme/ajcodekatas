using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjModel.Tests.Entities;

namespace AjModel.Tests
{
    [TestClass]
    public class ContextTests
    {
        private EntityModel<Customer> entityModel;
        private SimpleDomain domain;
        private Repository<Customer> repository;
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.entityModel = new EntityModel<Customer>();
            this.domain = new SimpleDomain();
            this.repository = new Repository<Customer>(this.entityModel, this.domain.Customers);
            this.context = new Context();
        }

        [TestMethod]
        public void AddAndGetRepository()
        {
            this.context.AddRepository(this.repository);

            Repository repo = this.context.GetRepository("Customer");
            Assert.IsNotNull(repo);
            Assert.AreEqual("Customer", repo.EntityModel.Name);
        }
    }
}
