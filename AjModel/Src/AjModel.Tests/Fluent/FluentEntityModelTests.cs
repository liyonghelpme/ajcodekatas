using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjModel.Tests.Entities;
using AjModel.Fluent;

namespace AjModel.Tests.Fluent
{
    [TestClass]
    public class FluentEntityModelTests
    {
        [TestMethod]
        public void SetNames()
        {
            EntityModel model = new EntityModel(typeof(Customer));
            FluentEntityModel<Customer> fluentModel = new FluentEntityModel<Customer>(model);
            fluentModel.Name("BusinessCustomer")
                .SetName("BusinessCustomers")
                .Descriptor("Business Customer")
                .SetDescriptor("Business Customers");

            Assert.AreEqual("BusinessCustomer", model.Name);
            Assert.AreEqual("BusinessCustomers", model.SetName);
            Assert.AreEqual("Business Customer", model.Descriptor);
            Assert.AreEqual("Business Customers", model.SetDescriptor);
        }

        [TestMethod]
        public void ModelForEntity()
        {
            Model model = new Model();
            var entityModel = model.ForEntity<Customer>();

            Assert.IsNotNull(entityModel);

            entityModel.Descriptor("Business Customer")
                .SetDescriptor("Business Customers");

            Assert.AreEqual("Customer", entityModel.Model.Name);
            Assert.AreEqual(typeof(Customer), entityModel.Model.Type);
            Assert.AreEqual("Business Customer", entityModel.Model.Descriptor);
            Assert.AreEqual("Business Customers", entityModel.Model.SetDescriptor);
        }
    }
}

