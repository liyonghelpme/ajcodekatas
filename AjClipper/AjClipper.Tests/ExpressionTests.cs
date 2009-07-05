namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjClipper.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void ShouldEvaluateIntegerExpression()
        {
            ConstantExpression expression = new ConstantExpression(123);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(123, (int) value);
        }

        [TestMethod]
        public void ShouldEvaluateStringExpression()
        {
            ConstantExpression expression = new ConstantExpression("foo");

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(string));
            Assert.AreEqual("foo", (string)value);
        }

        [TestMethod]
        public void ShouldEvaluateDateExpression()
        {
            DateTime date = new DateTime();
            ConstantExpression expression = new ConstantExpression(date);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(date, (DateTime)value);
        }

        [TestMethod]
        public void ShouldAddTwoIntegerNumbers()
        {
            IExpression expression = new AddExpression(1, 2);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(3, (int) value);
        }

        [TestMethod]
        public void ShouldAddTwoRealNumbers()
        {
            IExpression expression = new AddExpression(1.2, 3.4);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(double));
            Assert.AreEqual(4.6, (double) value);
        }

        [TestMethod]
        public void ShouldAddTwoDecimalNumbers()
        {
            IExpression expression = new AddExpression((decimal)1.2, (decimal) 3.4);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(decimal));
            Assert.AreEqual((decimal)4.6, (decimal)value);
        }

        [TestMethod]
        public void ShouldConcatenateStrings()
        {
            IExpression expression = new AddExpression("foo", "bar");

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(string));
            Assert.AreEqual("foobar", (string)value);
        }

        [TestMethod]
        public void ShouldSubtractTwoIntegerNumbers()
        {
            IExpression expression = new SubtractExpression(2, 1);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(1, (int)value);
        }

        [TestMethod]
        public void ShouldSubtractTwoRealNumbers()
        {
            IExpression expression = new SubtractExpression(3.4, 1.2);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(double));
            Assert.AreEqual(2.2, (double)value);
        }

        [TestMethod]
        public void ShouldSubtractTwoDecimalNumbers()
        {
            IExpression expression = new SubtractExpression((decimal)3.4, (decimal)1.2);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(decimal));
            Assert.AreEqual((decimal)2.2, (decimal)value);
        }

        [TestMethod]
        public void ShouldConcatenateStringsWithTrimming()
        {
            IExpression expression = new SubtractExpression("foo  ", "bar");

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(string));
            Assert.AreEqual("foobar", (string)value);
        }
    }
}
