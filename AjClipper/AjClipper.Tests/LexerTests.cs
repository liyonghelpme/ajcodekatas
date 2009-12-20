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
        public void ParseName()
        {
            Lexer lexer = new Lexer("foo");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseUpperCaseName()
        {
            Lexer lexer = new Lexer("FOO");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("FOO", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseQuestionMarkAsName()
        {
            Lexer lexer = new Lexer("?");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("?", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseTwoQuestionMarksAsName()
        {
            Lexer lexer = new Lexer("??");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("??", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithSurroundingSpaces()
        {
            Lexer lexer = new Lexer("  foo  ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseString()
        {
            Lexer lexer = new Lexer("\"foo bar\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseStringWithEmbeddedDoubleQuotes()
        {
            Lexer lexer = new Lexer("\"foo \\\"bar\\\"\"");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo \"bar\"", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseMultipleLineString()
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
        public void RaiseIfStringIsUnclosed()
        {
            Lexer lexer = new Lexer("\"Unclosed");

            lexer.NextToken();
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
        public void ParseDelimiters()
        {
            string delimiters = "[]{},()";
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
        public void ParseSingleCharOperators()
        {
            string operators = "=$%+-*/:><#^.";
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
        public void ParseTwoCharOperators()
        {
            string operators = ":= == >= <= -> -- ++ += -= *= /= ^= %= <> !=";
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

        [TestMethod]
        public void ParseComplexName()
        {
            Lexer lexer = new Lexer("System.Int32");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("System", token.Value);
            Assert.AreEqual(TokenType.Name, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Operator, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("Int32", token.Value);
            Assert.AreEqual(TokenType.Name, token.TokenType);
        }

        [TestMethod]
        public void ParseComplexNameWithTwoLevelsNamespace()
        {
            Lexer lexer = new Lexer("System.IO.File");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("System", token.Value);
            Assert.AreEqual(TokenType.Name, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Operator, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("IO", token.Value);
            Assert.AreEqual(TokenType.Name, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Operator, token.TokenType);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("File", token.Value);
            Assert.AreEqual(TokenType.Name, token.TokenType);
        }

        [TestMethod]
        public void ParseEndOfLine()
        {
            Lexer lexer = new Lexer("\r\n");

            Token token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseTwoEndOfLines()
        {
            Lexer lexer = new Lexer("\r\n\r\n");

            Token token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseEndOfLineNewLine()
        {
            Lexer lexer = new Lexer("\r\n\n");

            Token token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseNewLine()
        {
            Lexer lexer = new Lexer("\n");

            Token token = lexer.NextToken();

            Assert.AreEqual(TokenType.EndOfLine, token.TokenType);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
