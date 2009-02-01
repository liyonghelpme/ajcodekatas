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

        [TestMethod]
        public void ShouldBeReplaced()
        {
            Variable variable = new Variable("x");
            Variable newVariable = new Variable("y");

            Expression expression = variable.Replace(variable, newVariable);

            Assert.AreEqual(newVariable, expression);
        }

        [TestMethod]
        public void ShouldNotBeReplaced()
        {
            Variable variable = new Variable("x");
            Variable newVariable = new Variable("y");

            Expression expression = variable.Replace(newVariable, newVariable);

            Assert.AreEqual(variable, expression);
        }
    }
}
