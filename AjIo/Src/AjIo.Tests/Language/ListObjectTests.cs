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
            ListObject obj = new ListObject(new IoObject());
            object result = obj.Evaluate(new Message("clone"));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ListObject));
            Assert.AreEqual(0, obj.Count);
            Assert.AreEqual(0, ((ListObject)result).Count);
        }

        [TestMethod]
        public void ExecuteAddMethod()
        {
            ListObject obj = new ListObject(new IoObject());
            obj.Evaluate(new Message("add", new object[] { 1 }));
            object result = obj.Evaluate(new Message("count"));
            Assert.AreEqual(1, result);
            Assert.AreEqual(1, obj.Count);
            obj.Evaluate(new Message("add", new object[] { 2 }));
            result = obj.Evaluate(new Message("count"));
            Assert.AreEqual(2, result);
            Assert.AreEqual(2, obj.Count);
        }

        [TestMethod]
        public void ExecuteRemoveMethod()
        {
            ListObject obj = new ListObject(new IoObject());
            obj.Evaluate(new Message("add", new object[] { 1 }));
            obj.Evaluate(new Message("add", new object[] { 2 }));
            obj.Evaluate(new Message("add", new object[] { 3 }));
            obj.Evaluate(new Message("remove", new object[] { 1 }));
            Assert.AreEqual(2, obj.Evaluate(new Message("count")));
            Assert.AreEqual(2, obj.Count);
            Assert.AreEqual(2, obj.Evaluate(new Message("at", new object[] { 0 })));
            Assert.AreEqual(2, obj[0]);
            Assert.AreEqual(3, obj.Evaluate(new Message("at", new object[] { 1 })));
            Assert.AreEqual(3, obj[1]);
        }
    }
}
