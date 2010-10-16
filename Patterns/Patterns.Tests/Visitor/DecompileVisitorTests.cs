using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Patterns.Composite;
using Patterns.Visitor;
using Patterns.Interpreter;

namespace Patterns.Tests.Visitor
{
    [TestClass]
    public class DecompileVisitorTests
    {
        [TestMethod]
        public void DecompileSetCommand()
        {
            Assert.AreEqual("a = 1;\r\n", this.Decompile(new SetCommand("a", new ConstantExpression(1))));
        }

        [TestMethod]
        public void DecompileSetCommandWithArithmeticOperation()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Add, new ConstantExpression(1), new ConstantExpression(2));
            ICommand command = new SetCommand("a", expression);
            Assert.AreEqual("a = (1 + 2);\r\n", this.Decompile(command));
        }

        [TestMethod]
        public void DecompileSetCommandWithArithmeticOperationAndVariables()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Add, new VariableExpression("b"), new VariableExpression("c"));
            ICommand command = new SetCommand("a", expression);
            Assert.AreEqual("a = (b + c);\r\n", this.Decompile(command));
        }

        [TestMethod]
        public void DecompileNull()
        {
            IExpression expression = new ConstantExpression(null);
            Assert.AreEqual("null", this.Decompile(expression));
        }

        [TestMethod]
        public void DecompileString()
        {
            IExpression expression = new ConstantExpression("foo");
            Assert.AreEqual("\"foo\"", this.Decompile(expression));
        }

        [TestMethod]
        public void DecompileCompositeCommand()
        {
            IExpression expression = new ArithmeticBinaryExpression(ArithmeticOperation.Add, new ConstantExpression(1), new ConstantExpression(2));
            ICommand command1 = new SetCommand("a", expression);
            ICommand command2 = new SetCommand("b", expression);
            ICommand command = new CompositeCommand(new ICommand[] { command1, command2 });
            Assert.AreEqual("{\r\na = (1 + 2);\r\nb = (1 + 2);\r\n}\r\n", this.Decompile(command));
        }

        private string Decompile(ICommand command)
        {
            StringWriter writer = new StringWriter();
            IVisitor decompiler = new DecompileVisitor(writer);
            command.Accept(decompiler);

            writer.Close();

            return writer.ToString();
        }

        private string Decompile(IExpression expression)
        {
            StringWriter writer = new StringWriter();
            IVisitor decompiler = new DecompileVisitor(writer);
            expression.Accept(decompiler);

            writer.Close();

            return writer.ToString();
        }
    }
}
