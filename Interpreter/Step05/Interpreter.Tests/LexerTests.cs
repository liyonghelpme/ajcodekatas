using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Compiler;

namespace Interpreter.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ProcessName()
        {
            Lexer lexer = new Lexer("one");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("one", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessNameWithWhitespaces()
        {
            Lexer lexer = new Lexer("  one  ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("one", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessTwoNames()
        {
            Lexer lexer = new Lexer("one two");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("one", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("two", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessNameAndSeparator()
        {
            Lexer lexer = new Lexer("one;");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("one", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual(";", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessInteger()
        {
            Lexer lexer = new Lexer("123");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual(123, token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessAssignment()
        {
            Lexer lexer = new Lexer("=");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
