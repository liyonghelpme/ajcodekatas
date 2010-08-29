using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Compiler;
using Interpreter.Expressions;

namespace Interpreter.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseIntegerExpression()
        {
            Parser parser = new Parser("1");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.AreEqual(1, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseVariableExpression()
        {
            Parser parser = new Parser("one");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(VariableExpression));

            VariableExpression varexpr = (VariableExpression)expression;

            Assert.AreEqual("one", varexpr.Name);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
