namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjLambda.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ShouldCreateWithString()
        {
            Lexer lexer = new Lexer("xyz");

            Assert.IsNotNull(lexer);
        }

        [TestMethod]
        public void ShouldCreateWithTextReader()
        {
            Lexer lexer = new Lexer(new StringReader("xyz"));

            Assert.IsNotNull(lexer);
        }

        [TestMethod]
        public void ShouldGetVariables()
        {
            Lexer lexer = new Lexer("xyz");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("y", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("z", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldGetGroupedVariables()
        {
            Lexer lexer = new Lexer("x(yz)");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual("(", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("y", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("z", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual(")", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldGetNames()
        {
            Lexer lexer = new Lexer("K True 4 3");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("K", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("True", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("4", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("3", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldGetTokensWithComment()
        {
            Lexer lexer = new Lexer("xy True ; a comment");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("y", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("True", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldGetTokensInLinesWithComment()
        {
            Lexer lexer = new Lexer("xy ; a comment\n\rTrue");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("y", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.TokenType);
            Assert.AreEqual("True", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ShouldGetTokensFromSimpleLambda()
        {
            Lexer lexer = new Lexer(@"\x.x");

            Token token;

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(@"\", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.TokenType);
            Assert.AreEqual(@".", token.Value);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.TokenType);
            Assert.AreEqual("x", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
