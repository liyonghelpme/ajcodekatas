using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjIo.Language;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjIo.Tests.Language
{
    [TestClass]
    public class IoObjectTests
    {
        [TestMethod]
        public void SetAndGetSlot()
        {
            IoObject obj = new IoObject();
            obj.SetSlot("foo", "bar");
            Assert.AreEqual("bar", obj.GetSlot("foo"));
        }

        [TestMethod]
        public void GetUndefinedSlotAsNull()
        {
            IoObject obj = new IoObject();
            Assert.IsNull(obj.GetSlot("foo"));
        }

        [TestMethod]
        public void CloneObject()
        {
            IoObject obj = new IoObject();
            Message message = new Message("clone");
            object result = obj.Process(message);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IObject));
            Assert.IsInstanceOfType(result, typeof(ClonedObject));

            ClonedObject obj2 = (ClonedObject)result;

            Assert.AreEqual(obj, obj2.Parent);
        }
    }
}

