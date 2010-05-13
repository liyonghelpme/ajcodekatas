using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjIo.Language;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjIo.Tests.Language
{
    [TestClass]
    public class ListObjectTests
    {
        [TestMethod]
        public void ExecuteCloneMethod()
        {
            ListObject obj = new ListObject(new TopObject());
            object result = obj.Evaluate(new Message("clone"));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListObject));
            Assert.AreEqual(0, obj.Count);
            Assert.AreEqual(0, ((ListObject)result).Count);
        }

        [TestMethod]
        public void ExecuteAppendMethod()
        {
            ListObject obj = new ListObject(new TopObject());
            obj.Evaluate(new Message("append", new object[] { 1 }));
            object result = obj.Evaluate(new Message("size"));
            Assert.AreEqual(1, result);
            Assert.AreEqual(1, obj.Count);
            obj.Evaluate(new Message("append", new object[] { 2 }));
            result = obj.Evaluate(new Message("size"));
            Assert.AreEqual(2, result);
            Assert.AreEqual(2, obj.Count);
        }

        [TestMethod]
        public void ExecuteRemoveMethod()
        {
            ListObject obj = new ListObject(new TopObject());
            obj.Evaluate(new Message("append", new object[] { 1 }));
            obj.Evaluate(new Message("append", new object[] { 2 }));
            obj.Evaluate(new Message("append", new object[] { 3 }));
            obj.Evaluate(new Message("remove", new object[] { 1 }));
            Assert.AreEqual(2, obj.Evaluate(new Message("size")));
            Assert.AreEqual(2, obj.Count);
            Assert.AreEqual(2, obj.Evaluate(new Message("at", new object[] { 0 })));
            Assert.AreEqual(2, obj[0]);
            Assert.AreEqual(3, obj.Evaluate(new Message("at", new object[] { 1 })));
            Assert.AreEqual(3, obj[1]);
        }

        [TestMethod]
        public void CreateWithElements()
        {
            ListObject obj = new ListObject(new TopObject(), new object[] { 1,2,3});
            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(1, obj[0]);
            Assert.AreEqual(2, obj[1]);
            Assert.AreEqual(3, obj[2]);
        }

        [TestMethod]
        public void AtPut()
        {
            ListObject obj = new ListObject(new TopObject(), new object[] { 1, 2, 3 });
            obj.Evaluate(new Message("atPut", new object[] { 1, 20 }));
            Assert.AreEqual(3, obj.Count);
            Assert.AreEqual(1, obj[0]);
            Assert.AreEqual(20, obj[1]);
            Assert.AreEqual(3, obj[2]);
        }

        [TestMethod]
        public void AtInsert()
        {
            ListObject obj = new ListObject(new TopObject(), new object[] { 1, 2, 3 });
            obj.Evaluate(new Message("atInsert", new object[] { 1, 20 }));
            Assert.AreEqual(4, obj.Count);
            Assert.AreEqual(1, obj[0]);
            Assert.AreEqual(20, obj[1]);
            Assert.AreEqual(2, obj[2]);
            Assert.AreEqual(3, obj[3]);
        }

        [TestMethod]
        public void PrintString()
        {
            ListObject obj = new ListObject(new TopObject(), new object[] { 1, "foo", 3 });
            string result = Machine.PrintString(obj);

            Assert.AreEqual("list(1, \"foo\", 3)", result);
        }
    }
}
