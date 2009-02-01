namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLambda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PairTests
    {
        [TestMethod]
        public void ShouldCreateWithOneVariable()
        {
            Variable variable = new Variable("x");
            Pair pair = new Pair(variable, variable);

            Assert.IsNotNull(pair);
            Assert.AreEqual("xx", pair.ToString());
        }

        [TestMethod]
        public void ShouldCreateWithTwoVariables()
        {
            Pair pair = new Pair(new Variable("x"), new Variable("y"));

            Assert.IsNotNull(pair);
            Assert.AreEqual("xy", pair.ToString());
        }

        [TestMethod]
        public void ShouldCreateWithLeftPair()
        {
            Pair left = new Pair(new Variable("x"), new Variable("y"));
            Pair pair = new Pair(left, new Variable("z"));

            Assert.IsNotNull(pair);
            Assert.AreEqual("xyz", pair.ToString());
        }

        [TestMethod]
        public void ShouldCreateWithRightPair()
        {
            Pair right = new Pair(new Variable("x"), new Variable("y"));
            Pair pair = new Pair(new Variable("z"), right);

            Assert.IsNotNull(pair);
            Assert.AreEqual("z(xy)", pair.ToString());
        }

        [TestMethod]
        public void ShouldCreateWithLeftLambda()
        {
            Variable variable = new Variable("x");
            Lambda left = new Lambda(variable, variable);
            Pair pair = new Pair(left, new Variable("y"));

            Assert.IsNotNull(pair);
            Assert.AreEqual(@"(\x.x)y", pair.ToString());
        }

        [TestMethod]
        public void ShouldReplaceVariable()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");
            Variable variableZ = new Variable("z");

            Pair pair = new Pair(variableX, variableY);

            Expression expression = pair.Replace(variableY, variableZ);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));
            Assert.AreEqual("xz", expression.ToString());
        }

        [TestMethod]
        public void ShouldReduce()
        {
            Variable variableX = new Variable("x");
            Variable variableY = new Variable("y");
            Variable variableZ = new Variable("z");

            Pair body = new Pair(variableX, variableY);

            Lambda lambda = new Lambda(variableX, body);

            Pair pair = new Pair(lambda, variableZ);

            Expression expression = pair.Reduce();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));
            Assert.AreEqual("zy", expression.ToString());
        }
    }
}
