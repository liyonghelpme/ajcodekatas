namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjClipper.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ShouldParseName()
        {
            Lexer lexer = new Lexer("foo");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseUpperCaseName()
        {
            Lexer lexer = new Lexer("FOO");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseQuestionMarkAsName()
        {
            Lexer lexer = new Lexer("?");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("?", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseTwoQuestionMarksAsName()
        {
            Lexer lexer = new Lexer("??");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("??", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseNameWithSurroundingSpaces()
        {
            Lexer lexer = new Lexer("  foo  ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseString()
        {
            Lexer lexer = new Lexer("\"foo bar\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseStringWithEmbeddedDoubleQuotes()
        {
            Lexer lexer = new Lexer("\"foo \\\"bar\\\"\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo \"bar\"", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseMultipleLineString()
        {
            Lexer lexer = new Lexer("\"foo\r\nbar\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo\r\nbar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException), "Unclosed string")]
        public void ShouldRaiseIfStringIsUnclosed()
        {
            Lexer lexer = new Lexer("\"Unclosed");

            lexer.NextToken();
        }

        [TestMethod]
        public void ShouldParseInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldParseDelimiters()
        {
            string delimiters = "[]{},";
            Lexer lexer = new Lexer(delimiters);

            Token token;

            foreach (char ch in delimiters)
            {
                token = lexer.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Delimiter, token.TokenType);
                Assert.AreEqual(1, token.Value.Length);
                Assert.AreEqual(ch, token.Value[0]);
            }
        }

        [TestMethod]
        public void ShouldParseSingleCharOperators()
        {
            string operators = "=$%+-*/:><#^";
            Lexer lexer = new Lexer(operators);

            Token token;

            foreach (char ch in operators)
            {
                token = lexer.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Operator, token.TokenType);
                Assert.AreEqual(1, token.Value.Length);
                Assert.AreEqual(ch, token.Value[0]);
            }
        }

        [TestMethod]
        public void ShouldParseTwoCharOperators()
        {
            string operators = ":= == >= <= -> -- ++ += -= *= /= ^= %=";
            Lexer lexer = new Lexer(operators);

            Token token;

            foreach (string oper in operators.Split(' '))
            {
                token = lexer.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Operator, token.TokenType);
                Assert.AreEqual(oper, token.Value);
            }
        }
    }
}
