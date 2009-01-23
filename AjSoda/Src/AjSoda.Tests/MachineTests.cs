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
    }
}
