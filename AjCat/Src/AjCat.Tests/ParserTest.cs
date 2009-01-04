namespace AjCat.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjCat;
    using AjCat.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void CreateWithString()
        {
            Parser parser = new Parser("text");

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void CreateWithTextReader()
        {
            Parser parser = new Parser(new StringReader("text"));

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTextIsNull()
        {
            Parser parser = new Parser((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfTextReaderIsNull()
        {
            Parser parser = new Parser((TextReader)null);
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo and bar\"");

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
            Parser parser = new Parser("\"foo\\t\\\\\\r\\n\\\"and bar\"");

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
            using (Parser parser = new Parser(separators))
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
            Parser parser = new Parser("name");

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
            Parser parser = new Parser("name");

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
            Parser parser = new Parser("text");

            parser.PushToken(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfPushTwice()
        {
            Parser parser = new Parser("text");

            Token token = parser.NextToken();

            parser.PushToken(token);
            parser.PushToken(token);
        }

        [TestMethod]
        public void ParseNameWithSpaces()
        {
            Parser parser = new Parser(" name ");

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
            Parser parser = new Parser("() ( ) ++ ");

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
            Parser parser = new Parser("123");

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
            Parser parser = new Parser(" 123 ");

            Token token;

            token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ParseNameWithSeparators()
        {
            Parser parser = new Parser("[foo]");

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
