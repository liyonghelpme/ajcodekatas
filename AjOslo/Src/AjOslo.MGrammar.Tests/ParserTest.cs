namespace AjOslo.MGrammar.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjOslo.MGrammar.Ast;
    using AjOslo.MGrammar.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ShouldParseTextLiteral()
        {
            Parser parser = new Parser("\"literal\"");

            TextLiteral literal = parser.ParseTextLiteral();

            Assert.IsNotNull(literal);
            Assert.AreEqual("literal", literal.Value);
        }

        [TestMethod]
        public void ShouldParseEmptyModule()
        {
            Parser parser = new Parser("module HelloWorld { }");

            ModuleNode module = parser.ParseModule();

            Assert.IsNotNull(module);
            Assert.AreEqual("HelloWorld", module.Name);
        }

        [TestMethod]
        public void ShouldParseEmptyLanguage()
        {
            Parser parser = new Parser("language HelloWorld { }");

            LanguageNode language = parser.ParseLanguage();

            Assert.IsNotNull(language);
            Assert.AreEqual("HelloWorld", language.Name);
        }

        [TestMethod]
        public void ShouldParseModuleWithEmptyLanguage()
        {
            Parser parser = new Parser("module HelloWorld { language HelloLanguage {} }");

            ModuleNode module = parser.ParseModule();

            Assert.IsNotNull(module);
            Assert.AreEqual("HelloWorld", module.Name);

            ICollection<LanguageNode> languages = module.Languages;

            Assert.IsNotNull(languages);
            Assert.AreEqual(1, languages.Count);

            Assert.AreEqual("HelloLanguage", languages.First().Name);
        }

        [TestMethod]
        public void ShouldParseModuleWithThreeEmptyLanguages()
        {
            Parser parser = new Parser("module HelloWorld { language HelloLanguage1 {} language HelloLanguage2 {} language HelloLanguage3 {}}");

            ModuleNode module = parser.ParseModule();

            Assert.IsNotNull(module);
            Assert.AreEqual("HelloWorld", module.Name);

            ICollection<LanguageNode> languages = module.Languages;

            Assert.IsNotNull(languages);
            Assert.AreEqual(3, languages.Count);

            int k = 1;

            foreach (LanguageNode language in languages)
                Assert.AreEqual("HelloLanguage" + (k++), language.Name);
        }

        [TestMethod]
        public void ShouldParseTokenWithOneIdentifier()
        {
            Parser parser = new Parser("token Main = Hello;");

            TokenElement token = parser.ParseToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("Main", token.Name);
            Assert.AreEqual(1, token.Expressions.Count);

            Assert.IsInstanceOfType(token.Expressions.First(), typeof(Identifier));

            Identifier identifier = (Identifier)token.Expressions.First();

            Assert.AreEqual("Hello", identifier.Name);
        }

        [TestMethod]
        public void ShouldParseTokenWithTwoIdentifiers()
        {
            Parser parser = new Parser("token Main = Hello World;");

            TokenElement token = parser.ParseToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("Main", token.Name);
            Assert.AreEqual(2, token.Expressions.Count);

            foreach (PrimaryExpression expression in token.Expressions)
            {
                Assert.IsInstanceOfType(expression, typeof(Identifier));
            }
        }

        [TestMethod]
        public void ShouldParseTokenWithOneTextLiteral()
        {
            Parser parser = new Parser("token Main = \"Hello\";");

            TokenElement token = parser.ParseToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("Main", token.Name);
            Assert.AreEqual(1, token.Expressions.Count);

            Assert.IsInstanceOfType(token.Expressions.First(), typeof(TextLiteral));

            TextLiteral literal = (TextLiteral)token.Expressions.First();

            Assert.AreEqual("Hello", literal.Value);
        }

        [TestMethod]
        public void ShouldParseTokenWithTwoTextLiterals()
        {
            Parser parser = new Parser("token Main = \"Hello\" \"World\";");

            TokenElement token = parser.ParseToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("Main", token.Name);
            Assert.AreEqual(2, token.Expressions.Count);

            foreach (PrimaryExpression expression in token.Expressions)
                Assert.IsInstanceOfType(expression, typeof(TextLiteral));
        }

        [TestMethod]
        public void ShouldParseSyntaxWithOneIdentifier()
        {
            Parser parser = new Parser("syntax Main = Hello;");

            SyntaxElement syntax = parser.ParseSyntax();

            Assert.IsNotNull(syntax);
            Assert.AreEqual("Main", syntax.Name);
            Assert.AreEqual(1, syntax.Expressions.Count);

            Assert.IsInstanceOfType(syntax.Expressions.First(), typeof(Identifier));

            Identifier identifier = (Identifier)syntax.Expressions.First();

            Assert.AreEqual("Hello", identifier.Name);
        }

        [TestMethod]
        public void ShouldParseSyntaxWithTwoIdentifiers()
        {
            Parser parser = new Parser("syntax Main = Hello World;");

            SyntaxElement syntax = parser.ParseSyntax();

            Assert.IsNotNull(syntax);
            Assert.AreEqual("Main", syntax.Name);
            Assert.AreEqual(2, syntax.Expressions.Count);

            foreach (PrimaryExpression expression in syntax.Expressions)
            {
                Assert.IsInstanceOfType(expression, typeof(Identifier));
            }
        }

        [TestMethod]
        public void ShouldParseSyntaxWithOneTextLiteral()
        {
            Parser parser = new Parser("syntax Main = \"Hello\";");

            SyntaxElement syntax = parser.ParseSyntax();

            Assert.IsNotNull(syntax);
            Assert.AreEqual("Main", syntax.Name);
            Assert.AreEqual(1, syntax.Expressions.Count);

            Assert.IsInstanceOfType(syntax.Expressions.First(), typeof(TextLiteral));

            TextLiteral literal = (TextLiteral) syntax.Expressions.First();

            Assert.AreEqual("Hello", literal.Value);
        }

        [TestMethod]
        public void ShouldParseSyntaxWithTwoTextLiterals()
        {
            Parser parser = new Parser("syntax Main = \"Hello\" \"World\";");

            SyntaxElement syntax = parser.ParseSyntax();

            Assert.IsNotNull(syntax);
            Assert.AreEqual("Main", syntax.Name);
            Assert.AreEqual(2, syntax.Expressions.Count);

            foreach (PrimaryExpression expression in syntax.Expressions)
                Assert.IsInstanceOfType(expression, typeof(TextLiteral));
        }
    }
}
