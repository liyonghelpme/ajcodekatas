using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjAla.Compiler;
using AjAla.Language;
using AjAla.Expressions;

namespace AjAla.Tests.Compiler
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseInteger()
        {
            IExpression expression = ParseExpression("123");
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)expression;

            Assert.AreEqual(123, cexpr.Value);
        }

        [TestMethod]
        public void ParseString()
        {
            IExpression expression = ParseExpression("\"foo\"");
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)expression;

            Assert.AreEqual("foo", cexpr.Value);
        }

        private static IExpression ParseExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return expression;
        }
    }
}
