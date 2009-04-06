namespace AjOslo.MGrammar.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

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
        }
    }
}
