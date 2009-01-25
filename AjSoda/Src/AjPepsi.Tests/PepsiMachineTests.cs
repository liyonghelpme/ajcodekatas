namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AjSoda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PepsiMachineTests
    {
        [TestMethod]
        public void ShouldBeCreated()
        {
            PepsiMachine machine = new PepsiMachine();

            Assert.IsNotNull(machine);
        }

        [TestMethod]
        public void ShouldHasGlobals()
        {
            PepsiMachine machine = new PepsiMachine();

            Assert.IsNotNull(machine.GetGlobalObject("Object"));
        }

        [TestMethod]
        public void ShouldCreateClass()
        {
            PepsiMachine machine = new PepsiMachine();
            IObject proto = machine.CreatePrototype("TestPrototype");

            Assert.IsNotNull(proto);

            object obj = machine.GetGlobalObject("TestPrototype");

            Assert.AreEqual(proto, obj);

            Assert.IsInstanceOfType(proto.Behavior, typeof(IClass));

            IClass cls = (IClass) proto.Behavior;
            Assert.AreEqual(-1, cls.GetInstanceVariableOffset("x"));
            Assert.IsNull(cls.Lookup("x"));
            Assert.IsNull(cls.Send("lookup:", "x"));
            Assert.IsNotNull(cls.Parent);
            Assert.IsInstanceOfType(cls.Parent, typeof(BaseClass));
            Assert.IsNotNull(cls.Machine);
            Assert.AreEqual(machine, cls.Machine);
        }

        [TestMethod]
        public void ShouldSetGlobalVariable()
        {
            PepsiMachine machine = new PepsiMachine();

            machine.SetGlobalObject("One", 1);

            Assert.AreEqual(1, machine.GetGlobalObject("One"));
        }

        [TestMethod]
        public void ShouldGetNullIfGlobalVariableDoesNotExists()
        {
            PepsiMachine machine = new PepsiMachine();

            Assert.IsNull(machine.GetGlobalObject("InexistenteGlobal"));
        }
    }
}

