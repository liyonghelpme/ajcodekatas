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
        public void InsertAndRemoveObject()
        {
            BasicObject obj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            this.collection.Insert(obj);

            BasicObject other = this.collection.GetObject(obj.Id);

            Assert.IsNotNull(other);

            this.collection.DeleteObject(obj.Id);

            Assert.IsNull(this.collection.GetObject(obj.Id));
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

        [TestMethod]
        public void InsertAndRetrieveByKeyTenThousandsObjects()
        {
            ICollection<Guid> ids = this.CreateNumbers(10000);

            int n = 0;

            foreach (Guid id in ids)
            {
                n++;
                BasicObject obj = this.collection.GetObject(id);
                Assert.IsNotNull(obj);
                Assert.AreEqual(n, obj["Number"]);
            }
        }

        [TestMethod]
        public void InsertAndFindAllTenThousandsObjects()
        {
            CreateNumbers(10000);

            Cursor cursor = this.collection.FindAll();

            int n = 0;

            while (cursor.MoveNext())
            {
                n++;
                BasicObject obj = cursor.Current;
                Assert.IsNotNull(obj);
                Assert.AreEqual(n, obj["Number"]);
            }
        }

        [TestMethod]
        public void InsertAndFindEvenNumbers()
        {
            CreateNumbers(10000);

            Cursor cursor = this.collection.Find(x => ((int) x["Number"]) % 2 == 0);

            int n = 0;
            int k = 0;

            while (cursor.MoveNext())
            {
                n += 2;
                k++;
                BasicObject obj = cursor.Current;
                Assert.IsNotNull(obj);
                Assert.AreEqual(n, obj["Number"]);
            }

            Assert.AreEqual(5000, k);
            Assert.AreEqual(10000, n);
        }

        [TestMethod]
        public void InsertFindAndRemoveEvenNumbers()
        {
            ICollection<Guid> ids = CreateNumbers(10000);

            Cursor cursor = this.collection.Find(x => ((int)x["Number"]) % 2 == 0);

            int j = 0;

            foreach (Guid id in ids)
            {
                j++;

                if (j % 2 == 0)
                    this.collection.DeleteObject(id);
            }

            int n = 0;
            int k = 0;

            while (cursor.MoveNext())
            {
                n += 2;
                k++;
                BasicObject obj = cursor.Current;
                Assert.IsNotNull(obj);
                Assert.AreEqual(n, obj["Number"]);
            }

            Assert.AreEqual(5000, k);
            Assert.AreEqual(10000, n);
        }

        [TestMethod]
        public void InsertAndRemoveEvenNumbers()
        {
            CreateNumbers(10000);

            this.collection.Delete(x => ((int)x["Number"]) % 2 == 0);

            Cursor cursor = this.collection.Find(x => ((int)x["Number"]) % 2 == 0);

            Assert.IsFalse(cursor.MoveNext());
        }

        [TestMethod]
        public void UpdateEvenNumbers()
        {
            CreateNumbers(10000);

            this.collection.Update(x => ((int)x["Number"]) % 2 == 0, x => { x["Number"] = -1; });

            Cursor cursor = this.collection.Find(x => ((int)x["Number"]) % 2 == 0);

            Assert.IsFalse(cursor.MoveNext());
        }

        private ICollection<Guid> CreateNumbers(int n)
        {
            IList<Guid> ids = new List<Guid>();

            for (int k = 1; k <= n; k++)
            {
                BasicObject obj = CreateNumberObject(k);
                this.collection.Insert(obj);
                Assert.IsNotNull(obj.Id);
                ids.Add(obj.Id);
            }

            return ids;
        }

        private BasicObject CreateNumberObject(int k)
        {
            return BasicObject.CreateObject("Number", k);
        }
    }
}
