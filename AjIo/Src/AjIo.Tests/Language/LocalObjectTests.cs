using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjIo.Language;

namespace AjIo.Tests.Language
{
    [TestClass]
    public class LocalObjectTests
    {
        private TopObject obj;
        private LocalObject local;

        [TestInitialize]
        public void Setup()
        {
            this.obj = new TopObject();
            this.obj.SetSlot("Foo", "Bar");
            this.local = new LocalObject(obj);
            this.local.SetLocalSlot("a", "localvalue");
        }

        [TestMethod]
        public void ValueIsLocal()
        {
            Assert.AreEqual("localvalue", this.local.GetSlot("a"));
            Assert.IsNull(this.obj.GetSlot("a"));
        }

        [TestMethod]
        public void SetValueIsNotLocal()
        {
            this.local.SetSlot("b", "globalvalue");
            Assert.AreEqual("globalvalue", this.obj.GetSlot("b"));
        }

        [TestMethod]
        public void UpdateLocalValue()
        {
            this.local.UpdateSlot("a", "newlocalvalue");
            Assert.IsNull(this.obj.GetSlot("a"));
            Assert.AreEqual("newlocalvalue", this.local.GetSlot("a"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfUpdateUndefinedSlot()
        {
            this.local.UpdateSlot("b", "bar");
        }

        [TestMethod]
        public void GetSelf()
        {
            Assert.AreSame(this.obj, this.local.Self);
        }
    }
}
