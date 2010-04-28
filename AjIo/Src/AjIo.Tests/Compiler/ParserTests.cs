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
            Assert.IsInstanceOfType(expression, typeof(ObjectMessage));

            ObjectMessage msg = (ObjectMessage)expression;

            Assert.AreEqual("foo", msg.Object);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ObjectMessage));

            ObjectMessage msg = (ObjectMessage) expression;

            Assert.AreEqual(123, msg.Object);

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
            Assert.IsInstanceOfType(message.Arguments[0], typeof(ObjectMessage));
            Assert.IsInstanceOfType(message.Arguments[1], typeof(ObjectMessage));
            Assert.AreEqual(1, ((ObjectMessage)message.Arguments[0]).Object);
            Assert.AreEqual(2, ((ObjectMessage)message.Arguments[1]).Object);
        }

        [TestMethod]
        public void ParseTwoExpressionsWithTerminators()
        {
            Parser parser = new Parser("1;foo\r\n");

            object expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ObjectMessage));

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

            IMessage message = parser.ParseExpression();

            Assert.IsNotNull(message);
            Assert.IsInstanceOfType(message, typeof(MessageChain));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleAssigmentOperator()
        {
            Parser parser = new Parser("a := 1");

            IMessage expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(Message));

            Message message = (Message)expression;

            Assert.AreEqual(":=", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(2, message.Arguments.Count);

            object argument = message.Arguments[0];

            Assert.IsNotNull(argument);
            Assert.IsInstanceOfType(argument, typeof(string));
            Assert.AreEqual("a", argument);

            Assert.IsInstanceOfType(message.Arguments[1], typeof(ObjectMessage));
            Assert.AreEqual(1, ((ObjectMessage)message.Arguments[1]).Object);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleAddOperator()
        {
            Parser parser = new Parser("a + 1");

            IMessage expression = parser.ParseExpression();

            Assert.IsNotNull(expression);

            Assert.IsInstanceOfType(expression, typeof(MessageChain));

            MessageChain messages = (MessageChain)expression;

            Assert.IsInstanceOfType(messages.Messages[0], typeof(Message));
            Assert.AreEqual("a", ((Message)messages.Messages[0]).Symbol);

            Message message = (Message)messages.Messages[1];

            Assert.AreEqual("+", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(1, message.Arguments.Count);

            Assert.IsInstanceOfType(message.Arguments[0], typeof(ObjectMessage));
            Assert.AreEqual(1, ((ObjectMessage)message.Arguments[0]).Object);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleAddOperatorWithIntegers()
        {
            Parser parser = new Parser("1 + 2");

            IMessage expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MessageChain));

            MessageChain messages = (MessageChain)expression;

            Assert.IsInstanceOfType(messages.Messages[0], typeof(ObjectMessage));
            Assert.AreEqual(1, ((ObjectMessage)messages.Messages[0]).Object);

            Message message = (Message)messages.Messages[1];

            Assert.AreEqual("+", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(1, message.Arguments.Count);

            Assert.IsInstanceOfType(message.Arguments[0], typeof(ObjectMessage));
            Assert.AreEqual(2, ((ObjectMessage)message.Arguments[0]).Object);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleSubOperatorWithIntegers()
        {
            Parser parser = new Parser("3 - 1");

            IMessage expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MessageChain));

            MessageChain messages = (MessageChain)expression;

            Assert.IsInstanceOfType(messages.Messages[0], typeof(ObjectMessage));
            Assert.AreEqual(3, ((ObjectMessage)messages.Messages[0]).Object);

            Message message = (Message)messages.Messages[1];

            Assert.AreEqual("-", message.Symbol);
            Assert.IsNotNull(message.Arguments);
            Assert.AreEqual(1, message.Arguments.Count);

            Assert.IsInstanceOfType(message.Arguments[0], typeof(ObjectMessage));
            Assert.AreEqual(1, ((ObjectMessage)message.Arguments[0]).Object);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseTwoMessagesSeparatedByPeriod()
        {
            Parser parser = new Parser("Dog ::= Object clone clone; Dog name ::= \"Fido\"");

            Assert.IsNotNull(parser.ParseExpression());
            Assert.IsNotNull(parser.ParseExpression());
            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseMethodWithMultipleMessages()
        {
            Parser parser = new Parser("method(a:=1;b:=2;a+b)");

            IMessage result = parser.ParseExpression();

            Assert.IsNotNull(result);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
