namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjClipper.Expressions;
    using AjClipper.Commands;
    using AjClipper.Compiler;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ShouldParsePrintLineCommand()
        {
            Parser parser = new Parser("? \"Hello World\"");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(PrintLineCommand));
        }

        [TestMethod]
        public void ShouldParseAndExecutePrintLineCommand()
        {
            Parser parser = new Parser("? \"Hello World\"");

            ICommand command = parser.ParseCommand();

            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello World\r\n", writer.ToString());
        }

        [TestMethod]
        public void ShouldParseAndExecutePrintLineCommandWithListOfExpressions()
        {
            Parser parser = new Parser("? \"Hello\", \" \", \"World\"");

            ICommand command = parser.ParseCommand();

            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello World\r\n", writer.ToString());
        }

        [TestMethod]
        public void ShouldParseAndExecuteSetVariableCommand()
        {
            Parser parser = new Parser("foo := \"bar\"");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void ShouldParseSimpleIfCommand()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteSimpleIfCommand()
        {
            Parser parser = new Parser("if 1\r\n  a:=1\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(1, (int)value);
        }

        [TestMethod]
        public void ShouldParseIfCommandWithMultipleCommands()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\n  b:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithMultipleCommands()
        {
            Parser parser = new Parser("if 1\r\n  a:=1\r\n  b:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(1, (int)value);

            value = environment.GetValue("b");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }

        [TestMethod]
        public void ShouldSkipIfCommandsIfFalse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\n  b:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNull(value);

            value = environment.GetValue("b");

            Assert.IsNull(value);
        }

        [TestMethod]
        public void ShouldParseIfCommandWithElse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelse\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithElse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelse\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }

        [TestMethod]
        public void ShouldParseIfCommandWithElseIf()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelseif 1\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithElseIf()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelseif 1\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }
    }
}
