namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjLambda.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ShouldCreateWithString()
        {
            Parser parser = new Parser("xyz");

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ShouldCreateWithLexer()
        {
            Parser parser = new Parser(new Lexer("xyz"));

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ShouldCreateWithReader()
        {
            Parser parser = new Parser(new Lexer(new StringReader("xyz")));

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ShouldParseVariable()
        {
            Parser parser = new Parser("x");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Variable));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ShouldParsePair()
        {
            Parser parser = new Parser("xy");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));

            Assert.IsNull(parser.ParseExpression());
            Assert.AreEqual("xy", expression.ToString());
        }

        [TestMethod]
        public void ShouldParseTwoPairs()
        {
            Parser parser = new Parser("xyz");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));

            Assert.IsNull(parser.ParseExpression());
            Assert.AreEqual("xyz", expression.ToString());
        }

        [TestMethod]
        public void ShouldParseTwoPairsWithParenthesis()
        {
            Parser parser = new Parser("x(yz)");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Pair));

            Assert.IsNull(parser.ParseExpression());
            Assert.AreEqual("x(yz)", expression.ToString());
        }

        [TestMethod]
        public void ShouldParseSimpleLambda()
        {
            Parser parser = new Parser(@"\x.x");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(@"\x.x", expression.ToString());
        }

        [TestMethod]
        public void ShouldParseLambdaWithBoundVariable()
        {
            Parser parser = new Parser(@"\x.xy");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(@"\x.xy", expression.ToString());

            Expression newExpression = ((Lambda)expression).Replace(new Variable("y"), new Variable("x"));

            Assert.AreEqual(@"\a.ax", newExpression.ToString());
        }

        [TestMethod]
        public void ShouldParseTwoLambdas()
        {
            Parser parser = new Parser(@"\xy.yx");

            Expression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Lambda));
            Assert.AreEqual(@"\xy.yx", expression.ToString());
        }
    }
}
