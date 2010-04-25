namespace AjIo.Tests.Compiler
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjIo.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetIdentifier()
        {
            Lexer lexer = new Lexer("name");
            Token token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Identifier, token.TokenType);
            Assert.AreEqual("name", token.Value);
            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIdentifierWithSpaces()
        {
            Lexer lexer = new Lexer(" name ");
            Token token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Identifier, token.TokenType);
            Assert.AreEqual("name", token.Value);
            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoIdentifiers()
        {
            Lexer lexer = new Lexer("foo bar");

            Token token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Identifier, token.TokenType);
            Assert.AreEqual("foo", token.Value);
            
            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Identifier, token.TokenType);
            Assert.AreEqual("bar", token.Value);
            
            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");
            Token token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);
            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetString()
        {
            Lexer lexer = new Lexer("\"foo\"");
            Token token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo", token.Value);
            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void RaiseIfUnclosedString()
        {
            Lexer lexer = new Lexer("\"foo");
            lexer.NextToken();
        }

        [TestMethod]
        public void GetSeparators()
        {
            Lexer lexer = new Lexer("(,)");
            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.LeftPar, token.TokenType);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Comma, token.TokenType);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.RightPar, token.TokenType);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTerminators()
        {
            Lexer lexer = new Lexer(";\n\r\n");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Terminator, token.TokenType);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Terminator, token.TokenType);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Terminator, token.TokenType);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetArithmeticOperators()
        {
            Lexer lexer = new Lexer("+ - * /");
            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("+", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("-", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("*", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("/", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetAssigmentOperators()
        {
            Lexer lexer = new Lexer("::= := =");
            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("::=", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(":=", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
