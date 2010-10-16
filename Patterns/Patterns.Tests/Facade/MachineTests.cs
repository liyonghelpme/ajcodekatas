using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Facade;

namespace Patterns.Tests.Facade
{
    [TestClass]
    public class MachineTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void RunSimpleSet()
        {
            this.machine.Run("a = 1;");
            Assert.AreEqual(1, this.machine.GetValue("a"));
        }

        [TestMethod]
        public void RunTwoCommands()
        {
            this.machine.Run("a = 1;b = 2;");
            Assert.AreEqual(1, this.machine.GetValue("a"));
            Assert.AreEqual(2, this.machine.GetValue("b"));
        }

        [TestMethod]
        [DeploymentItem("Facade/SetVariables.txt")]
        public void RunFile()
        {
            this.machine.RunFile("SetVariables.txt");
            Assert.AreEqual(1, this.machine.GetValue("a"));
            Assert.AreEqual(2, this.machine.GetValue("b"));
        }
    }
}
