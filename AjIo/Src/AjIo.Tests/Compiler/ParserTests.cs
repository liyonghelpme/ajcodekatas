using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjIo.Compiler;
using AjIo.Language;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjIo.Tests.Compiler
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseIdentifier()
        {
            Parser parser = new Parser("foo");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Message));

            Message message = (Message)expression;

            Assert.AreEqual("foo", message.Symbol);
            Assert.IsNull(message.Arguments);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo\"");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(string));

            string text = (string)expression;

            Assert.AreEqual("foo", text);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(int));

            int number = (int) expression;

            Assert.AreEqual(123, number);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseIdentifierWithArguments()
        {
            Parser parser = new Parser("foo(1, 2)");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Message));

            Message message = (Message)expression;

            Assert.AreEqual("foo", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(2, message.Arguments.Count);
            Assert.AreEqual(1, message.Arguments[0]);
            Assert.AreEqual(2, message.Arguments[1]);
        }
    }
}
