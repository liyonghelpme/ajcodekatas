using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjIo.Language;

namespace AjIo.Tests
{
    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void PrintString()
        {
            Assert.AreEqual("nil", Machine.PrintString(null));
            Assert.AreEqual("\"foo\"", Machine.PrintString("foo"));
            Assert.AreEqual("123", Machine.PrintString(123));
            Assert.AreEqual("true", Machine.PrintString(true));
            Assert.AreEqual("false", Machine.PrintString(false));
            Assert.IsTrue(Machine.PrintString(new TopObject()).StartsWith("Object_"));
        }

        [TestMethod]
        public void InitialSlots()
        {
            Machine machine = new Machine();

            object obj = machine.GetSlot("Object");

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(TopObject));

            object list = machine.GetSlot("List");

            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(list, typeof(ListObject));

            Assert.AreEqual(obj, ((ListObject)list).Parent);
        }
    }
}
