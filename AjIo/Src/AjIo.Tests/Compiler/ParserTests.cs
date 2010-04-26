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

        [TestMethod]
        public void ParseTwoExpressionsWithTerminators()
        {
            Parser parser = new Parser("1;foo\r\n");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(int));

            expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Message));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseTerminators()
        {
            Parser parser = new Parser(";\n\r\n");

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseCloneExpression()
        {
            Parser parser = new Parser("Object clone");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(IList<Message>));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleAssigmentOperator()
        {
            Parser parser = new Parser("a := 1");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Message));

            Message message = (Message)expression;

            Assert.AreEqual(":=", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(2, message.Arguments.Count);

            expression = message.Arguments[0];

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(string));
            Assert.AreEqual("a", expression);

            Assert.AreEqual(1, message.Arguments[1]);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
