using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjModel.Tests.Entities;

namespace AjModel.Tests
{
    [TestClass]
    public class EntityModelTests
    {
        [TestMethod]
        public void GetNames()
        {
            EntityModel model = new EntityModel(typeof(Customer));

            Assert.AreEqual("Customer", model.Name);
            Assert.IsNull(model.SetName);
        }

        [TestMethod]
        public void GetProperties()
        {
            EntityModel model = new EntityModel(typeof(Customer));

            var properties = model.Properties;

            Assert.IsNotNull(properties);
            Assert.AreEqual(4, properties.Count);

            Assert.AreEqual("Id", properties.First().Name);
            Assert.AreEqual("Name", properties.Skip(1).First().Name);
            Assert.AreEqual("Address", properties.Skip(2).First().Name);
            Assert.AreEqual("Notes", properties.Skip(3).First().Name);
        }

        [TestMethod]
        public void GetPropertyValue()
        {
            EntityModel model = new EntityModel(typeof(Customer));

            var customer = CreateCustomer(1);

            var idProperty = model.Properties.First();
            Assert.AreEqual("Id", idProperty.Name);
            Assert.AreEqual(customer.Id, idProperty.GetValue(customer));

            var nameProperty = model.Properties.Skip(1).First();
            Assert.AreEqual("Name", nameProperty.Name);
            Assert.AreEqual(customer.Name, nameProperty.GetValue(customer));
        }

        private static Customer CreateCustomer(int n)
        {
            return new Customer()
            {
                Id = n,
                Name = string.Format("Customer " + n),
                Address = string.Format("Address " + n),
                Notes = string.Format("Notes " + n)
            };
        }
    }
}
