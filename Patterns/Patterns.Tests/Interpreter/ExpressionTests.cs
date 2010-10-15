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
    }
}
