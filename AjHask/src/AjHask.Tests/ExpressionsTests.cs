namespace AjHask.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjHask.Expressions;
    using AjHask.Language;
    using AjHask.Tests.Functions;

    [TestClass]
    public class ExpressionsTests
    {
        [TestMethod]
        public void EvaluateConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(1);

            Assert.AreEqual(1, expr.Evaluate());
        }

        [TestMethod]
        public void EvaluateApplyExpression() 
        {
            ApplyExpression expr = new ApplyExpression(new ConstantExpression(new IncrementFunction()), new ConstantExpression(1));

            Assert.AreEqual(2, expr.Evaluate());
        }

        [TestMethod]
        public void AddOneInteger()
        {
            ApplyExpression expr = new ApplyExpression(new ConstantExpression(new AddIntegerFunction()), new ConstantExpression(1));

            object result = expr.Evaluate();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialFunction));
        }

        [TestMethod]
        public void AddTwoIntegers()
        {
            ApplyExpression expr = new ApplyExpression(new ApplyExpression(new ConstantExpression(new AddIntegerFunction()), new ConstantExpression(1)), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate());
        }
    }
}
