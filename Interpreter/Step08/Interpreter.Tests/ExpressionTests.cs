using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Interpreter.Expressions;

namespace Interpreter.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void EvaluateIntegerConstantExpression()
        {
            IExpression expression = new ConstantExpression(1);

            Assert.AreEqual(1, expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateStringConstantExpression()
        {
            IExpression expression = new ConstantExpression("foo");

            Assert.AreEqual("foo", expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateIntegerVariable()
        {
            BindingEnvironment environment = new BindingEnvironment();
            IExpression expression = new VariableExpression("one");

            environment.SetValue("one", 1);

            Assert.AreEqual(1, expression.Evaluate(environment));
        }

        [TestMethod]
        public void EvaluateUndefinedVariableAsNull()
        {
            BindingEnvironment environment = new BindingEnvironment();
            IExpression expression = new VariableExpression("undefined");

            Assert.IsNull(expression.Evaluate(environment));
        }

        [TestMethod]
        public void CreateBinaryArithmeticExpression()
        {
            ConstantExpression leftExpression = new ConstantExpression(1);
            ConstantExpression rightExpression = new ConstantExpression(2);

            BinaryArithmeticExpression expression = new BinaryArithmeticExpression(leftExpression, rightExpression, ArithmeticOperator.Add);

            Assert.AreEqual(leftExpression, expression.LeftExpression);
            Assert.AreEqual(rightExpression, expression.RightExpression);
            Assert.AreEqual(ArithmeticOperator.Add, expression.Operator);
        }

        [TestMethod]
        public void EvaluateAddExpression()
        {
            ConstantExpression leftExpression = new ConstantExpression(1);
            ConstantExpression rightExpression = new ConstantExpression(2);

            BinaryArithmeticExpression expression = new BinaryArithmeticExpression(leftExpression, rightExpression, ArithmeticOperator.Add);

            Assert.AreEqual(3, expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateSubtractExpression()
        {
            ConstantExpression leftExpression = new ConstantExpression(1);
            ConstantExpression rightExpression = new ConstantExpression(2);

            BinaryArithmeticExpression expression = new BinaryArithmeticExpression(leftExpression, rightExpression, ArithmeticOperator.Subtract);

            Assert.AreEqual(-1, expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateMultiplyExpression()
        {
            ConstantExpression leftExpression = new ConstantExpression(3);
            ConstantExpression rightExpression = new ConstantExpression(2);

            BinaryArithmeticExpression expression = new BinaryArithmeticExpression(leftExpression, rightExpression, ArithmeticOperator.Multiply);

            Assert.AreEqual(6, expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateDivideExpression()
        {
            ConstantExpression leftExpression = new ConstantExpression(4);
            ConstantExpression rightExpression = new ConstantExpression(2);

            BinaryArithmeticExpression expression = new BinaryArithmeticExpression(leftExpression, rightExpression, ArithmeticOperator.Divide);

            Assert.AreEqual(2.0, expression.Evaluate(null));
        }
    }
}
