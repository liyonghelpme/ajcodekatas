using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjCat;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjCat.Tests
{
    [TestClass]
    public class MachineTest
    {
        [TestMethod]
        public void CreateWithoutParameters()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine);
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void PushAndPopAValue()
        {
            Machine machine = new Machine();

            machine.Push("foo");

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual("foo", machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void PushPeekAndPopAValue()
        {
            Machine machine = new Machine();

            machine.Push("foo");

            Assert.AreEqual("foo", machine.Top());
            Assert.AreEqual("foo", machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void PushAndPopTwoValues()
        {
            Machine machine = new Machine();

            machine.Push("foo");
            machine.Push("bar");

            Assert.AreEqual(2, machine.StackCount);
            Assert.AreEqual("bar", machine.Pop());
            Assert.AreEqual("foo", machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfPopAndStackIsEmpty()
        {
            Machine machine = new Machine();

            machine.Pop();
        }
    }
}
