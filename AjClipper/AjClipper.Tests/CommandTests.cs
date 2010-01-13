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
    using AjClipper.Data;

    [TestClass]
    public class CommandTests
    {
        private const string OleDbProviderFactoryName = "System.Data.OleDb";
        private const string OleDbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;";

        [TestMethod]
        public void PrintHello()
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
        public void PrintLineHello()
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
        public void PrintHelloWorldUsingCompositeCommand()
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

        [TestMethod]
        public void SetVariable()
        {
            ICommand command = new SetVariableCommand("foo", new ConstantExpression("bar"));
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void ExecuteLocalCommand()
        {
            ICommand command = new LocalCommand(new string[] { "foo" });

            ValueEnvironment parent = new ValueEnvironment();
            ValueEnvironment localenv = new ValueEnvironment(parent, ValueEnvironmentType.Local);
            ValueEnvironment childenv = new ValueEnvironment(localenv);

            command.Execute(null, childenv);

            Assert.IsFalse(childenv.ContainsValue("foo"));
            Assert.IsFalse(parent.ContainsValue("foo"));
            Assert.IsTrue(localenv.ContainsValue("foo"));
        }

        [TestMethod]
        public void ExecutePublicCommand()
        {
            ICommand command = new PublicCommand(new string[] { "foo" });

            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment localenv = new ValueEnvironment(parent, ValueEnvironmentType.Local);
            ValueEnvironment childenv = new ValueEnvironment(localenv);

            command.Execute(null, childenv);

            Assert.IsFalse(childenv.ContainsValue("foo"));
            Assert.IsTrue(parent.ContainsValue("foo"));
            Assert.IsFalse(localenv.ContainsValue("foo"));
        }

        [TestMethod]
        public void ExecutePrivateCommand()
        {
            ICommand command = new PrivateCommand(new string[] { "foo" });

            ValueEnvironment parent = new ValueEnvironment(ValueEnvironmentType.Public);
            ValueEnvironment privateenv = new ValueEnvironment(parent);
            ValueEnvironment localenv = new ValueEnvironment(privateenv, ValueEnvironmentType.Local);

            command.Execute(null, localenv);

            Assert.IsFalse(localenv.ContainsValue("foo"));
            Assert.IsFalse(parent.ContainsValue("foo"));
            Assert.IsTrue(privateenv.ContainsValue("foo"));
        }

        [TestMethod]
        public void ExecuteUseDatabaseCommand()
        {
            OpenDatabaseCommand command = new OpenDatabaseCommand(new NameExpression("testdb"), new ConstantExpression(OleDbConnectionString), new ConstantExpression(OleDbProviderFactoryName));
            Machine machine = new Machine();

            command.Execute(machine, machine.Environment);

            object result = machine.Environment.GetValue(ValueEnvironment.CurrentDatabase);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Database));
            
            Database database = (Database) result;

            Assert.AreEqual(OleDbConnectionString, database.ConnectionString);
            Assert.IsInstanceOfType(database.ProviderFactory, typeof(System.Data.OleDb.OleDbFactory));

            object result2 = machine.Environment.GetValue("testdb");

            Assert.IsTrue(result == result2);
        }

        [TestMethod]
        public void ExecuteUseWorkAreaCommand()
        {
            OpenDatabaseCommand dbcommand = new OpenDatabaseCommand(new NameExpression("testdb"), new ConstantExpression(OleDbConnectionString), new ConstantExpression(OleDbProviderFactoryName));
            Machine machine = new Machine();

            dbcommand.Execute(machine, machine.Environment);

            UseWorkAreaCommand command = new UseWorkAreaCommand(new NameExpression("testwa"), null);

            command.Execute(machine, machine.Environment);

            object result = machine.Environment.GetValue(ValueEnvironment.CurrentWorkArea);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(WorkArea));

            WorkArea workarea = (WorkArea)result;

            Assert.AreEqual("testwa", workarea.Name);

            object result2 = machine.Environment.GetValue("testwa");

            Assert.IsTrue(result == result2);
        }

        [TestMethod]
        public void EvaluateReturnCommand()
        {
            ReturnCommand retcmd = new ReturnCommand(new ConstantExpression(1));

            Assert.AreEqual(1, retcmd.Evaluate(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ReturnException))]
        public void ExecuteReturnCommand()
        {
            ReturnCommand retcmd = new ReturnCommand(new ConstantExpression(1));

            retcmd.Execute(null, null);
        }
    }
}
