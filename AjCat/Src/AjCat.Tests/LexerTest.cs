namespace AjCat.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjCat;
    using AjCat.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void CreateWithString()
        {
            Lexer parser = new Lexer("text");

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void CreateWithTextReader()
        {
            Lexer parser = new Lexer(new StringReader("text"));

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTextIsNull()
        {
            Lexer parser = new Lexer((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTextReaderIsNull()
        {
            Lexer parser = new Lexer((TextReader)null);
        }

        [TestMethod]
        public void ParseString()
        {
            Lexer parser = new Lexer("\"foo and bar\"");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo and bar", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseStringWithEscapedCharacters()
        {
            Lexer parser = new Lexer("\"foo\\t\\\\\\r\\n\\\"and bar\"");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.TokenType);
            Assert.AreEqual("foo\t\\\r\n\"and bar", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseSeparators()
        {
            string separators = "[]{}";
            using (Lexer parser = new Lexer(separators))
            {
                Token token;

                foreach (char ch in separators)
                {
                    token = parser.NextToken();

                    Assert.IsNotNull(token);
                    Assert.AreEqual(TokenType.Separator, token.TokenType);
                    Assert.AreEqual(ch.ToString(), token.Value);
                }

                token = parser.NextToken();

                Assert.IsNull(token);
            }
        }

        [TestMethod]
        public void ParseName()
        {
            Lexer parser = new Lexer("name");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseAndPushToken()
        {
            Lexer parser = new Lexer("name");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            parser.PushToken(token);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTokenIsNull()
        {
            Lexer parser = new Lexer("text");

            parser.PushToken(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfPushTwice()
        {
            Lexer parser = new Lexer("text");

            Token token = parser.NextToken();

            parser.PushToken(token);
            parser.PushToken(token);
        }

        [TestMethod]
        public void ParseNameWithSpaces()
        {
            Lexer parser = new Lexer(" name ");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseNamesWithSpecialChars()
        {
            Lexer parser = new Lexer("() ( ) ++ ");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("()", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("(", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual(")", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("++", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseInteger()
        {
            Lexer parser = new Lexer("123");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseIntegerWithSpaces()
        {
            Lexer parser = new Lexer(" 123 ");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseDouble()
        {
            Lexer parser = new Lexer("123.45");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Double, token.TokenType);
            Assert.AreEqual("123.45", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseNameWithSeparators()
        {
            Lexer parser = new Lexer("[foo]");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual("[", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("foo", token.Value);

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual("]", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }
    }
}
