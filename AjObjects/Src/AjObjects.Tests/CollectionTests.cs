using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjObjects.Tests
{
    [TestClass]
    public class CollectionTests
    {
        private Collection collection;

        [TestInitialize]
        public void Setup()
        {
            this.collection = new Collection();
        }

        [TestMethod]
        public void InsertObject()
        {
            BasicObject obj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            Assert.IsNull(obj["_id"]);

            this.collection.Insert(obj);

            Assert.IsNotNull(obj["_id"]);
            Assert.IsInstanceOfType(obj["_id"], typeof(Guid));
        }

        [TestMethod]
        public void InsertAndRetrieveObject()
        {
            BasicObject obj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            this.collection.Insert(obj);

            BasicObject other = this.collection.GetObject((Guid)obj["_id"]);

            Assert.AreEqual(obj, other);
            Assert.AreNotSame(obj, other);
        }

        [TestMethod]
        public void RetrieveUndefinedObjectAsNull()
        {
            Assert.IsNull(this.collection.GetObject(Guid.NewGuid()));
        }

        [TestMethod]
        public void InsertedObjectIsACopy()
        {
            BasicObject obj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            this.collection.Insert(obj);

            BasicObject inserted = obj.MakeDeepCopy();

            obj["Family"] = "Genesis";
            obj["Wife"] = "Eve";

            BasicObject retrieved = this.collection.GetObject((Guid)obj["_id"]);

            Assert.AreEqual(inserted, retrieved);
            Assert.AreEqual(retrieved, inserted);
            Assert.AreNotSame(inserted, retrieved);
            Assert.AreNotEqual(obj, retrieved);
            Assert.AreNotEqual(obj, inserted);
        }
    }
}
