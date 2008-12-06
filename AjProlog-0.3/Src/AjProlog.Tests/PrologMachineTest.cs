using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjProlog.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjProlog.Tests
{
    /// <summary>
    /// Summary description for PrologMachineTest
    /// </summary>
    [TestClass]
    public class PrologMachineTest
    {
        [TestMethod]
        public void ShouldCreateVariable()
        {
            PrologMachine pm = new PrologMachine();

            Variable v1 = pm.GetVariable(0);

            Assert.IsNotNull(v1);
        }

        [TestMethod]
        public void ShouldCreateVariables()
        {
            PrologMachine pm = new PrologMachine();

            Variable v1 = pm.GetVariable(0);
            Variable v2 = pm.GetVariable(0);

            Assert.IsTrue(v1.Equals(v2));
            Assert.AreEqual("_0", v1.ToString());
        }

        [TestMethod]
        public void ShouldCreateTwoVariables()
        {
            PrologMachine pm = new PrologMachine();

            Variable v1 = pm.GetVariable(0);
            Variable v2 = pm.GetVariable(1);

            Assert.IsFalse(v1.Equals(v2));
            Assert.AreEqual("_0", v1.ToString());
            Assert.AreEqual("_1", v2.ToString());
        }
    }
}
