using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjObjects.Tests
{
    [TestClass]
    public class BasicObjectTests
    {
        private BasicObject obj;

        [TestInitialize]
        public void CreateObject()
        {
            this.obj = new BasicObject();
        }

        [TestMethod]
        public void SetAndGetValues()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            Assert.AreEqual("Adam", this.obj["Name"]);
            Assert.AreEqual(800, this.obj["Age"]);
        }

        [TestMethod]
        public void UndefinedPropertyIsNull()
        {
            Assert.IsNull(this.obj["UndefinedName"]);
        }

        [TestMethod]
        public void GetPropertyNames()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            ICollection<string> names = this.obj.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("Name", names.First());
            Assert.AreEqual("Age", names.Skip(1).First());
        }

        [TestMethod]
        public void RemoveProperty()
        {
            this.obj["Name"] = "Adam";
            this.obj["Family"] = "Genesis";
            this.obj["Age"] = 800;

            ICollection<string> names = this.obj.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(3, names.Count);
            Assert.AreEqual("Name", names.First());
            Assert.AreEqual("Family", names.Skip(1).First());
            Assert.AreEqual("Age", names.Skip(2).First());

            this.obj["Family"] = null;

            names = this.obj.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("Name", names.First());
            Assert.AreEqual("Age", names.Skip(1).First());
        }

        [TestMethod]
        public void CreateObjectUsingNameValuePairs()
        {
            BasicObject newobj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            Assert.AreEqual("Adam", newobj["Name"]);
            Assert.AreEqual(800, newobj["Age"]);

            ICollection<string> names = newobj.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("Name", names.First());
            Assert.AreEqual("Age", names.Skip(1).First());
        }

        [TestMethod]
        public void IsEqualToItself()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            Assert.IsTrue(this.obj.Equals(this.obj));
        }

        [TestMethod]
        public void EmptyObjectsAreEqual()
        {
            BasicObject newobj = new BasicObject();

            Assert.IsTrue(newobj.Equals(this.obj));
            Assert.IsTrue(this.obj.Equals(newobj));
            Assert.AreEqual(this.obj.GetHashCode(), newobj.GetHashCode());
        }

        [TestMethod]
        public void ObjectsWithSameNamesValuesAreEqual()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            BasicObject newobj = new BasicObject();
            newobj["Age"] = 800;
            newobj["Name"] = "Adam";

            Assert.IsTrue(newobj.Equals(this.obj));
            Assert.IsTrue(this.obj.Equals(newobj));
            Assert.AreEqual(this.obj.GetHashCode(), newobj.GetHashCode());
        }

        [TestMethod]
        public void MakeDeepCopyOfSimpleObject()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            BasicObject newobj = this.obj.MakeDeepCopy();

            Assert.IsTrue(newobj.Equals(this.obj));

            ICollection<string> names = newobj.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("Name", names.First());
            Assert.AreEqual("Age", names.Skip(1).First());
        }

        [TestMethod]
        public void MakeDeepCopyOfComplexObject()
        {
            BasicObject caine = this.MakeCaineAndFamily();
            BasicObject clone = caine.MakeDeepCopy();

            Assert.AreEqual(caine, clone);

            Assert.AreEqual(caine["Father"], clone["Father"]);
            Assert.AreEqual(caine["Mother"], clone["Mother"]);
            Assert.AreNotSame(caine["Father"], clone["Father"]);
            Assert.AreNotSame(caine["Mother"], clone["Mother"]);
        }

        [TestMethod]
        public void IdIsAName()
        {
            this.obj.Id = Guid.NewGuid();

            Assert.AreEqual(this.obj.Id, this.obj["_id"]);
        }

        private BasicObject MakeCaineAndFamily()
        {
            this.obj["Name"] = "Adam";
            this.obj["Age"] = 800;

            BasicObject caine = BasicObject.CreateObject("Name", "Caine", "Father", this.obj, "Mother", BasicObject.CreateObject("Name", "Eve", "Age", 700));

            return caine;
        }
    }
}
