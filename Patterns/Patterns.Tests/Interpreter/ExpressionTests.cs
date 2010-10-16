using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Interpreter;

namespace Patterns.Tests.Interpreter
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateConstantExpression()
        {
            ConstantExpression expression = new ConstantExpression(1);
            Assert.AreEqual(1, expression.Value);
            Assert.AreEqual(1, expression.Evaluate(null));
        }

        [TestMethod]
        public void CreateAndEvaluateVariableExpression()
        {
            VariableExpression expression = new VariableExpression("foo");
            Assert.AreEqual("foo", expression.Name);
            Context context = new Context();
            context.SetValue("foo", "bar");
            Assert.AreEqual("bar", expression.Evaluate(context));
        }

        [TestMethod]
        public void AddTwoIntegers()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Add, new ConstantExpression(1), new ConstantExpression(2));
            Assert.AreEqual(3, expression.Evaluate(null));
        }

        [TestMethod]
        public void SubtractTwoIntegers()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Substract, new ConstantExpression(1), new ConstantExpression(2));
            Assert.AreEqual(-1, expression.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyTwoIntegers()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Multiply, new ConstantExpression(3), new ConstantExpression(2));
            Assert.AreEqual(6, expression.Evaluate(null));
        }

        [TestMethod]
        public void DivideTwoIntegers()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Divide, new ConstantExpression(3), new ConstantExpression(2));
            Assert.AreEqual(3/2, expression.Evaluate(null));
        }
    }
}
