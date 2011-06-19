using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjModel.Tests.Entities;

namespace AjModel.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void CreateEmptyModel()
        {
            Model model = new Model();

            Assert.IsNull(model.GetEntityModel("Customer"));
            Assert.AreEqual(0, model.EntityModels.Count());
        }

        [TestMethod]
        public void CreateModelWithOneEntity()
        {
            Model model = new Model();

            model.AddEntityModel(new EntityModel(typeof(Customer)));

            Assert.AreEqual(1, model.EntityModels.Count());

            EntityModel entityModel = model.GetEntityModel("Customer");
            Assert.IsNotNull(entityModel);
            Assert.AreEqual(typeof(Customer), entityModel.Type);
            Assert.AreEqual("Customer", entityModel.Name);
        }

        [TestMethod]
        public void GetEntityModelByType()
        {
            Model model = new Model();

            model.AddEntityModel(new EntityModel<Customer>());

            EntityModel entityModel = model.GetEntityModel<Customer>();
            Assert.IsNotNull(entityModel);
            Assert.AreEqual(typeof(Customer), entityModel.Type);
            Assert.AreEqual("Customer", entityModel.Name);
        }

        [TestMethod]
        public void GetModelFromSimpleDomain()
        {
            var model = new Model(typeof(SimpleDomain));

            var entityModel = model.GetEntityModel("Customer");

            Assert.IsNotNull(entityModel);
        }

        [TestMethod]
        public void GetProductModelFromSimpleDomain()
        {
            var model = new Model(typeof(SimpleDomain));

            var entityModel = model.GetEntityModel("Product");

            Assert.IsNotNull(entityModel);
        }
    }
}
