namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjClipper;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void ShouldCreateWithValueEnvironment()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.Environment);
        }
    }
}
