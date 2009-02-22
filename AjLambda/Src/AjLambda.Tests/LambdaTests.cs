namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLambda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LambdaTests
    {
        [TestMethod]
        public void ShouldCreateWithSimpleBody()
        {
            Variable variable = new Variable("x");
            Lambda lambda = new Lambda(variable, variable);

            Assert.IsNotNull(lambda);
            Assert.AreEqual(@"\x.x", lambda.ToString());
        }

        [TestMethod]
        public void ShouldCreateWithLambdaInBody()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");
            Lambda lambda = new Lambda(variableX, new Lambda(variableY, new Pair(variableX, variableY)));

            Assert.IsNotNull(lambda);
            Assert.AreEqual(@"\xy.xy", lambda.ToString());
        }

        [TestMethod]
        public void ShouldGetFreeVariable()
        {
            Variable variableX = new Variable("x");
            Variable variableX2 = new Variable("x");
            Variable variableY = new Variable("y");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX2, pair);

            IEnumerable<Variable> freeVars = lambda.FreeVariables();

            Assert.IsNotNull(freeVars);
            Assert.AreEqual(1, freeVars.Count());
            Assert.AreEqual("y", freeVars.First().ToString());
        }

        [TestMethod]
        public void ShouldGetNoFreeVariable()
        {
            Variable variableX = new Variable("x");
            Variable variableX2 = new Variable("x");

            Lambda lambda = new Lambda(variableX, variableX2);

            IEnumerable<Variable> freeVars = lambda.FreeVariables();

            Assert.IsNotNull(freeVars);
            Assert.AreEqual(0, freeVars.Count());
        }

        [TestMethod]
        public void ShouldGetNoNestedFreeVariable()
        {
            Variable variableX = new Variable("x");
            Variable variableX2 = new Variable("x");
            Variable variableY = new Variable("y");

            Lambda innerLambda = new Lambda(variableY, variableX);

            Lambda lambda = new Lambda(variableX2, innerLambda);

            IEnumerable<Variable> freeVars = lambda.FreeVariables();

            Assert.IsNotNull(freeVars);
            Assert.AreEqual(0, freeVars.Count());
        }

        [TestMethod]
        public void ShouldReplaceFreeVariable()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");
            Variable variableZ = new Variable("z");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, pair);

            Expression expression = lambda.Replace(variableY, variableZ);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(@"\x.xz", expression.ToString());
        }

        [TestMethod]
        public void ShouldReplaceFreeVariableRenamingParameter()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, pair);

            Expression expression = lambda.Replace(variableY, variableX);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(@"\a.ax", expression.ToString());
        }

        [TestMethod]
        public void ShouldApplyValue()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");
            Variable variableZ = new Variable("z");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, pair);

            Expression expression = lambda.Apply(variableZ);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));
            Assert.AreEqual("zy", expression.ToString());
        }

        [TestMethod]
        public void ShouldReduce()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, pair);

            Expression expression = lambda.Reduce();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(lambda, expression);
        }

        public void ShouldGetFreeVariables()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");

            Pair pair = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, pair);

            IEnumerable<Variable> freeVariables = lambda.FreeVariables();

            Assert.IsNotNull(freeVariables);
            Assert.AreEqual(1, freeVariables.Count());
            Assert.IsTrue(freeVariables.Contains(variableY));
        }
    }
}
