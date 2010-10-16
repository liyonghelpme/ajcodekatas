using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Adapter;
using Patterns.Interpreter;
using Patterns.Composite;

namespace Patterns.Tests.Adapter
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("1");
            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            ConstantExpression cexpr = (ConstantExpression)expression;
            Assert.AreEqual(1, cexpr.Value);
            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseName()
        {
            Parser parser = new Parser("foo");
            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(VariableExpression));
            VariableExpression vexpr = (VariableExpression)expression;
            Assert.AreEqual("foo", vexpr.Name);
            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSetCommand()
        {
            Parser parser = new Parser("one = 1;");
            ICommand command = parser.ParseCommand();
            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetCommand));
            SetCommand scmd = (SetCommand)command;
            Assert.AreEqual("one", scmd.Name);
            Assert.IsNotNull(scmd.Expression);
            Assert.IsInstanceOfType(scmd.Expression, typeof(ConstantExpression));
            ConstantExpression cexpr = (ConstantExpression)scmd.Expression;
            Assert.AreEqual(1, cexpr.Value);
            Assert.IsNull(parser.ParseCommand());
        }
    }
}
