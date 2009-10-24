namespace AjHask.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjHask.Compiler;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ParseName()
        {
            Lexer lexer = new Lexer("name");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithSpaces()
        {
            Lexer lexer = new Lexer(" name ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithUnderscore()
        {
            Lexer lexer = new Lexer("foo_bar");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo_bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithSingleQuote()
        {
            Lexer lexer = new Lexer("foo'bar");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo'bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseLeftParenthesisAsSeparator()
        {
            Lexer lexer = new Lexer("(");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual("(", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseReal()
        {
            Lexer lexer = new Lexer("123.45");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Real, token.TokenType);
            Assert.AreEqual("123.45", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
