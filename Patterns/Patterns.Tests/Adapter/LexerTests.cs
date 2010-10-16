using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Adapter;

namespace Patterns.Tests.Adapter
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("name");

            Token token = lexer.ReadToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.ReadToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer(" name  ");

            Token token = lexer.ReadToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.ReadToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.ReadToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual(123, token.Value);

            Assert.IsNull(lexer.ReadToken());
        }

        [TestMethod]
        public void GetIntegerWithSpaces()
        {
            Lexer lexer = new Lexer(" 123 ");

            Token token = lexer.ReadToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual(123, token.Value);

            Assert.IsNull(lexer.ReadToken());
        }

        [TestMethod]
        public void GetOperators()
        {
            Lexer lexer = new Lexer("=+-*/");

            ReadToken(lexer, TokenType.Operator, "=");
            ReadToken(lexer, TokenType.Operator, "+");
            ReadToken(lexer, TokenType.Operator, "-");
            ReadToken(lexer, TokenType.Operator, "*");
            ReadToken(lexer, TokenType.Operator, "/");

            Assert.IsNull(lexer.ReadToken());
        }

        [TestMethod]
        public void GetSeparators()
        {
            Lexer lexer = new Lexer(";()");

            ReadToken(lexer, TokenType.Separator, ";");
            ReadToken(lexer, TokenType.Separator, "(");
            ReadToken(lexer, TokenType.Separator, ")");

            Assert.IsNull(lexer.ReadToken());
        }

        private void ReadToken(Lexer lexer, TokenType type, object value)
        {
            Token token = lexer.ReadToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(token.TokenType, type);
            Assert.AreEqual(token.Value, value);
        }
    }
}
