using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjIo.Language;
using AjIo.Methods;

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
            object result = obj.Process(null, message);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IObject));
            Assert.IsInstanceOfType(result, typeof(ClonedObject));

            ClonedObject obj2 = (ClonedObject)result;

            Assert.AreEqual(obj, obj2.Parent);
        }

        [TestMethod]
        public void SetSlot()
        {
            IoObject obj = new IoObject();
            Message message = new Message("setSlot", new object[] { "foo", "bar" });
            
            obj.Process(obj, message);

            object result = obj.GetSlot("foo");

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", result);
        }

        [TestMethod]
        public void SetSlotAndGetMessage()
        {
            IoObject obj = new IoObject();
            Message message = new Message("setSlot", new object[] { "foo", "bar" });

            obj.Process(obj, message);

            object result = obj.Process(null, new Message("foo"));

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", result);
        }

        [TestMethod]
        public void GetToString()
        {
            IoObject obj = new IoObject();
            string text = obj.ToString();
            Assert.IsTrue(text.StartsWith("Object_"));
        }

        [TestMethod]
        public void GetClonedToString()
        {
            IoObject obj = new IoObject();
            IObject cloned = new ClonedObject(obj);
            string text = cloned.ToString();
            Assert.IsTrue(text.StartsWith("Object_"));
        }

        [TestMethod]
        public void NewSlot()
        {
            IoObject obj = new IoObject();
            Message message = new Message("newSlot", new object[] { "foo", "bar" });

            obj.Process(obj, message);

            object result = obj.GetSlot("foo");

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", result);

            object result2 = obj.GetSlot("setFoo");

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(SetterMethod));

            SetterMethod method = (SetterMethod)result2;

            Assert.AreEqual("foo", method.SlotName);
        }
    }
}

