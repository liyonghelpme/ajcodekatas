using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjProlog.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjProlog.Tests
{
    /// <summary>
    /// Summary description for ParserTest
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ShouldCreateParserWithTextReader()
        {
            Parser parser = new Parser(new StringReader("a"));

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ShouldCreateParserWithText()
        {
            Parser parser = new Parser("a");

            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ShouldParseAtom()
        {
            Parser parser = new Parser("a");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);
        }

        [TestMethod]
        public void ShouldParseTwoAtoms()
        {
            Parser parser = new Parser("a b");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);

            Token token2 = parser.NextToken();

            Assert.IsNotNull(token2);
            Assert.AreEqual(TokenType.Atom, token2.Type);
            Assert.AreEqual("b", token2.Value);
        }

        [TestMethod]
        public void ShouldParseAtomWithBlanks()
        {
            Parser parser = new Parser(" a ");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);
        }

        [TestMethod]
        public void ShouldParseInteger()
        {
            Parser parser = new Parser("123");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);
        }

        [TestMethod]
        public void ShouldParseAtomAndInteger()
        {
            Parser parser = new Parser("a 123");

            Token token = parser.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);

            Token token2 = parser.NextToken();

            Assert.IsNotNull(token2);
            Assert.AreEqual(TokenType.Integer, token2.Type);
            Assert.AreEqual("123", token2.Value);
        }

        [TestMethod]
        public void ShouldParseSeparators()
        {
            string separators = ",.()[]";
            Parser parser = new Parser(",.()[]");

            Token token;

            for (int k = 0; k < separators.Length; k++)
            {
                token = parser.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Separator, token.Type);
                Assert.AreEqual(separators[k].ToString(), token.Value);
            }

            token = parser.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ShouldParseRule()
        {
            Parser parser = new Parser("a(Y) :- f(a),g(X)");

            Token token;

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("Y", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual(":-", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("f", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("a", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(",", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("g", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("X", token.Value);

            token = parser.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            token = parser.NextToken();
            Assert.IsNull(token);
        }
    }
}

