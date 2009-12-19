namespace AjClipper.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjClipper;
    using AjClipper.Expressions;
    using AjClipper.Commands;
    using AjClipper.Compiler;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjClipper.Language;
    using AjClipper.Data;

    [TestClass]
    public class ExamplesTests
    {
        [TestMethod]
        [DeploymentItem("Examples\\SimpleAssignment.prg")]
        public void ParseAndEvaluateSimpleAssignment()
        {
            Parser parser = new Parser(File.OpenText("SimpleAssignment.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleIf.prg")]
        public void ParseAndEvaluateSimpleIf()
        {
            Parser parser = new Parser(File.OpenText("SimpleIf.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("positive", environment.GetValue("bar"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleProcedure.prg")]
        public void ParseAndEvaluateSimpleProcedure()
        {
            Parser parser = new Parser(File.OpenText("SimpleProcedure.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            object result = environment.GetValue("setbar");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Procedure));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimplePublicVariable.prg")]
        public void ParseAndEvaluateSimplePublicVariable()
        {
            Parser parser = new Parser(File.OpenText("SimplePublicVariable.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            Assert.AreEqual("foo", environment.GetValue("bar"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleLocalVariable.prg")]
        public void ParseAndEvaluateSimpleLocalVariable()
        {
            Parser parser = new Parser(File.OpenText("SimpleLocalVariable.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            Assert.AreEqual("publicbar", environment.GetValue("bar"));
            Assert.AreEqual("localbar", environment.GetValue("foo"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimplePrivateVariable.prg")]
        public void ParseAndEvaluateSimplePrivateVariable()
        {
            Parser parser = new Parser(File.OpenText("SimplePrivateVariable.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            Assert.AreEqual("privatebar", environment.GetValue("foo"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleNewObject.prg")]
        public void ParseAndEvaluateSimpleNewObject()
        {
            Parser parser = new Parser(File.OpenText("SimpleNewObject.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            object result = environment.GetValue("foo");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo));
        }

        [TestMethod]
        [DeploymentItem("Examples\\DataUseDatabase.prg")]
        public void ParseAndEvaluateDataUseDatabase()
        {
            Parser parser = new Parser(File.OpenText("DataUseDatabase.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            object result = environment.GetValue("testdb");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Database));

            object result2 = environment.GetValue(ValueEnvironment.CurrentDatabase);

            Assert.IsTrue(result == result2);
        }

        [TestMethod]
        [DeploymentItem("Examples\\DataUseWorkArea.prg")]
        public void ParseAndEvaluateDataUseWorkArea()
        {
            Parser parser = new Parser(File.OpenText("DataUseWorkArea.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            object result = environment.GetValue("test");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(WorkArea));

            object result2 = environment.GetValue(ValueEnvironment.CurrentWorkArea);

            Assert.IsTrue(result == result2);
        }

        [TestMethod]
        [DeploymentItem("Examples\\DataGetField.prg")]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void ParseAndEvaluateDataGetField()
        {
            Parser parser = new Parser(File.OpenText("DataGetField.prg"));
            ICommand command = parser.ParseCommandList();
            Machine machine = new Machine();

            command.Execute(machine, machine.Environment);

            object result = machine.Environment.GetValue("code");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));
            Assert.AreEqual(1.0, result);
        }
    }
}
