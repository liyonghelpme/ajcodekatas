namespace AjSoda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void ShouldCreateMachine()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine);
            Assert.IsNotNull(machine.Object);
            Assert.IsInstanceOfType(machine.Object, typeof(BaseObject));
            Assert.IsNotNull(machine.Behavior);
            Assert.IsInstanceOfType(machine.Behavior, typeof(BaseBehavior));
        }

        [TestMethod]
        public void ShouldLookupBehaviorMethods()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.Behavior.Lookup("lookup:"));
            Assert.IsNotNull(machine.Behavior.Lookup("delegated"));
            Assert.IsNotNull(machine.Behavior.Lookup("methodAt:put:"));
            Assert.IsNotNull(machine.Behavior.Lookup("allocate:"));
            Assert.IsNotNull(machine.Behavior.Lookup("vtable"));
        }

        [TestMethod]
        public void ShouldLookupBehaviorMethodsUsingSend()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.Behavior.Send("lookup:", "lookup:"));
            Assert.IsNotNull(machine.Behavior.Send("lookup:", "delegated"));
            Assert.IsNotNull(machine.Behavior.Send("lookup:", "methodAt:put:"));
            Assert.IsNotNull(machine.Behavior.Send("lookup:", "allocate:"));
            Assert.IsNotNull(machine.Behavior.Send("lookup:", "vtable"));
        }

        [TestMethod]
        public void ShouldLookupObjectMethods()
        {
            Machine machine = new Machine();

            IBehavior objectBehavior = (IBehavior) machine.Object.Behavior;

            Assert.IsNull(objectBehavior.Lookup("lookup:"));
            Assert.IsNotNull(objectBehavior.Lookup("delegated"));
            Assert.IsNull(objectBehavior.Lookup("methodAt:put:"));
            Assert.IsNull(objectBehavior.Lookup("allocate:"));
            Assert.IsNotNull(objectBehavior.Lookup("vtable"));
        }

        [TestMethod]
        public void ShouldGetObjectBehavior()
        {
            Machine machine = new Machine();

            Assert.AreEqual(machine.Object.Behavior, machine.Object.Send("vtable"));
        }

        [TestMethod]
        public void ShouldDelegateObject()
        {
            Machine machine = new Machine();

            IObject obj = machine.Object;

            IObject delegated = (IObject) obj.Send("delegated");

            Assert.IsNotNull(delegated);

            Assert.AreEqual(obj.Behavior, ((IBehavior)delegated.Behavior).Parent);
            Assert.AreEqual(obj.Behavior.Behavior, delegated.Behavior.Behavior);

            Assert.AreEqual(obj.Size, delegated.Size);
        }
    }
}
