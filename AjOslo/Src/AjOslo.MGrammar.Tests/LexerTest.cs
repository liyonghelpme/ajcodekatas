namespace AjOslo.MGrammar.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjOslo.MGrammar.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void ShouldParseName()
        {
            Lexer lexer = new Lexer("name");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("name", token.Value);

            token = lexer.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ShouldParseInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.TokenType);
            Assert.AreEqual("123", token.Value);

            token = lexer.NextToken();

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ShouldParseSeparators()
        {
            string separators = "()[]{},:;";
            Lexer lexer = new Lexer(separators);

            Token token;

            foreach (char ch in separators)
            {
                token = lexer.NextToken();
                Assert.IsNotNull(token);
                Assert.AreEqual(TokenType.Separator, token.TokenType);
                Assert.AreEqual(ch.ToString(), token.Value);
            }

            token = lexer.NextToken();

            Assert.IsNull(token);
        }
    }
}
