using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interpreter.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void EvaluateIntegerConstantExpression()
        {
            IExpression expression = new ConstantExpression(1);

            Assert.AreEqual(1, expression.Evaluate());
        }

        [TestMethod]
        public void EvaluateStringConstantExpression()
        {
            IExpression expression = new ConstantExpression("foo");

            Assert.AreEqual("foo", expression.Evaluate());
        }
    }
}
