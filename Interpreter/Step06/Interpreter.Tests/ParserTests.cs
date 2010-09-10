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
        public void ParseStringExpression()
        {
            Parser parser = new Parser("\"foo\"");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.AreEqual("foo", expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException), "Unclosed string")]
        public void RaiseIfUnclosedStringExpression()
        {
            Parser parser = new Parser("\"foo");

            IExpression expression = parser.ParseExpression();
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

        [TestMethod]
        public void ParseAndEvaluateAddExpression()
        {
            Parser parser = new Parser("1+2");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            BinaryArithmeticExpression addexpr = (BinaryArithmeticExpression)expression;

            Assert.AreEqual(ArithmeticOperator.Add, addexpr.Operator);

            Assert.AreEqual(3, addexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateMultiplyExpression()
        {
            Parser parser = new Parser("2*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            BinaryArithmeticExpression multexpr = (BinaryArithmeticExpression)expression;

            Assert.AreEqual(ArithmeticOperator.Multiply, multexpr.Operator);

            Assert.AreEqual(6, multexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateAddAndMultiplyExpression()
        {
            Parser parser = new Parser("1+2*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            Assert.AreEqual(7, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateAddAndMultiplyExpressionWithParenthesis()
        {
            Parser parser = new Parser("(1+2)*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            Assert.AreEqual(9, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
