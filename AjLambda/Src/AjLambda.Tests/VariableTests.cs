namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLambda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void ShouldCreateWithName()
        {
            Variable variable = new Variable("x");

            Assert.IsNotNull(variable);
            Assert.AreEqual("x", variable.Name);
            Assert.AreEqual("x", variable.ToString());
        }
    }
}
