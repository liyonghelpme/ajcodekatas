namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using AjClipper.Commands;
    using AjClipper.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void ShouldPrintHello()
        {
            ICommand command = new PrintCommand(new ConstantExpression("Hello"));
            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello", writer.ToString());
        }

        [TestMethod]
        public void ShouldPrintLineHello()
        {
            ICommand command = new PrintLineCommand(new ConstantExpression("Hello"));
            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello\r\n", writer.ToString());
        }

        [TestMethod]
        public void ShouldPrintHelloWorldUsingCompositeCommand()
        {
            ICommand firstCommand = new PrintCommand(new ConstantExpression("Hello "));
            ICommand secondCommand = new PrintLineCommand(new ConstantExpression("World"));
            CompositeCommand command = new CompositeCommand();
            command.AddCommand(firstCommand);
            command.AddCommand(secondCommand);

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
    }
}
