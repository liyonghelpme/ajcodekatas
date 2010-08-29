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
    }
}
