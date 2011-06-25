using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjModel.Tests.Entities;

namespace AjModel.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private EntityModel<Customer> entityModel;
        private SimpleDomain domain;
        private Repository<Customer> repository;

        [TestInitialize]
        public void Setup()
        {
            this.entityModel = new EntityModel<Customer>();
            this.domain = new SimpleDomain();
            this.repository = new Repository<Customer>(this.entityModel, this.domain.Customers);
        }

        [TestMethod]
        public void GetEntities()
        {
            IEnumerable<Customer> entities = this.repository.GetEntities();

            Assert.IsNotNull(entities);
            Assert.AreEqual(this.domain.Customers.Count, entities.Count());
        }

        [TestMethod]
        public void GetEntity()
        {
            Customer entity = this.repository.GetEntity(1);

            Assert.IsNotNull(entity);
            Assert.AreEqual(1, entity.Id);
        }

        [TestMethod]
        public void AddEntity()
        {
            Customer entity = new Customer() { Id = 1000 };

            this.repository.AddEntity(entity);

            Customer newEntity = this.repository.GetEntity(1000);

            Assert.IsNotNull(newEntity);
            Assert.AreEqual(entity, newEntity);
        }

        [TestMethod]
        public void AddObject()
        {
            Customer entity = new Customer() { Id = 1000 };

            this.repository.AddObject(entity);

            object newObject = this.repository.GetObject(1000);

            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(Customer));

            Customer newEntity = (Customer)newObject;

            Assert.IsNotNull(newEntity);
            Assert.AreEqual(entity, newEntity);
        }

        [TestMethod]
        public void RemoveEntity()
        {
            Customer entity = this.repository.GetEntity(2);

            this.repository.RemoveEntity(entity);

            Assert.IsNull(this.repository.GetEntity(2));
        }

        [TestMethod]
        public void RemoveObject()
        {
            Object obj = this.repository.GetObject(2);

            this.repository.RemoveObject(obj);

            Assert.IsNull(this.repository.GetObject(2));
        }
    }
}
