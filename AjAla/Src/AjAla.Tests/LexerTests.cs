namespace AjAla.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjAla;
    using AjAla.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ShouldParseSimpleName()
        {
            Lexer lexer = new Lexer("foo");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.Name);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseSimpleNamePreservingCase()
        {
            Lexer lexer = new Lexer("Foo");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.Name);
            Assert.AreEqual("Foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseSimpleNameWithSpaces()
        {
            Lexer lexer = new Lexer(" foo ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.Name);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseSimpleString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.String);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseStringWithEscapedDoubleQuote()
        {
            Lexer lexer = new Lexer("\"foo\\\"bar\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.String);
            Assert.AreEqual("foo\"bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.Integer);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldPushToken()
        {
            Lexer lexer = new Lexer("foo");

            Token token = lexer.NextToken();

            lexer.PushToken(token);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, TokenType.Name);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseSeparators()
        {
            string separators = "()[]{},.:";
            Lexer lexer = new Lexer(separators);

            Token token;

            foreach (char ch in separators)
            {
                token = lexer.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Separator, token.TokenType);
                Assert.AreEqual(ch.ToString(), token.Value);
            }

            Assert.IsNull(lexer.NextToken());
        }
    }
}
