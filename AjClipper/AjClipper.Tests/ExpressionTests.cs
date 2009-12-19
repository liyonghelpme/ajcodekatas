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

        [TestMethod]
        public void ShouldEvaluateName()
        {
            IExpression expression = new NameExpression("foo");
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("foo", "bar");

            Assert.AreEqual("bar", expression.Evaluate(environment));
        }

        [TestMethod]
        public void ShouldMultiplyTwoIntegerNumbers()
        {
            IExpression expression = new MultiplyExpression(3, 2);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(6, (int)value);
        }

        [TestMethod]
        public void ShouldMultiplyTwoRealNumbers()
        {
            IExpression expression = new MultiplyExpression(3.0, 2.0);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(double));
            Assert.AreEqual(6.0, (double)value);
        }

        [TestMethod]
        public void ShouldDivideTwoIntegerNumbers()
        {
            IExpression expression = new DivideExpression(4, 2);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(double));
            Assert.AreEqual(2.0, (double)value);
        }

        [TestMethod]
        public void ShouldDivideTwoRealNumbers()
        {
            IExpression expression = new DivideExpression(3.0, 2.0);

            object value = expression.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(double));
            Assert.AreEqual(1.5, (double)value);
        }

        [TestMethod]
        public void EvaluateCompareEqualExpressions()
        {
            Assert.IsTrue((bool)(new CompareExpression(0, 0, CompareOperator.Equal)).Evaluate(null));
            Assert.IsFalse((bool)(new CompareExpression(1, 0, CompareOperator.Equal)).Evaluate(null));
            Assert.IsFalse((bool)(new CompareExpression(0, 1, CompareOperator.Equal)).Evaluate(null));
            Assert.IsTrue((bool)(new CompareExpression("foo", "foo", CompareOperator.Equal)).Evaluate(null));
            Assert.IsFalse((bool)(new CompareExpression("foo", "bar", CompareOperator.Equal)).Evaluate(null));
            Assert.IsFalse((bool)(new CompareExpression("bar", "foo", CompareOperator.Equal)).Evaluate(null));
        }

        [TestMethod]
        public void EvaluateCompareNotEqualExpressions()
        {
            Assert.IsFalse((bool)(new CompareExpression(0, 0, CompareOperator.NotEqual)).Evaluate(null));
            Assert.IsTrue((bool)(new CompareExpression(1, 0, CompareOperator.NotEqual)).Evaluate(null));
            Assert.IsTrue((bool)(new CompareExpression(0, 1, CompareOperator.NotEqual)).Evaluate(null));
            Assert.IsFalse((bool)(new CompareExpression("foo", "foo", CompareOperator.NotEqual)).Evaluate(null));
            Assert.IsTrue((bool)(new CompareExpression("foo", "bar", CompareOperator.NotEqual)).Evaluate(null));
            Assert.IsTrue((bool)(new CompareExpression("bar", "foo", CompareOperator.NotEqual)).Evaluate(null));
        }

        [TestMethod]
        public void EvaluateNewExpression()
        {
            NewExpression expression = new NewExpression(new ConstantExpression("System.IO.FileInfo"), new IExpression[] { new ConstantExpression("myfile.txt") });

            object result = expression.Evaluate(new ValueEnvironment());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo));
        }

        [TestMethod]
        public void EvaluateDotExpressionOnInteger()
        {
            IExpression expression = new DotExpression(new ConstantExpression(1), "ToString", new List<IExpression>());

            Assert.AreEqual("1", expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateDotExpressionOnString()
        {
            IExpression expression = new DotExpression(new ConstantExpression("foo"), "Length");

            Assert.AreEqual(3, expression.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateDotExpressionAsTypeInvocation()
        {
            DotExpression dot = new DotExpression(new DotExpression(new DotExpression(new NameExpression("System"), "IO"), "File"), "Exists", new IExpression[] { new ConstantExpression("unknown.txt") });

            Assert.IsFalse((bool)dot.Evaluate(new ValueEnvironment()));
        }
    }
}
