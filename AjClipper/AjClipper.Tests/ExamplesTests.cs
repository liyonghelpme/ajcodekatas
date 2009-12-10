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

    [TestClass]
    public class ExamplesTests
    {
        [TestMethod]
        [DeploymentItem("Examples\\SimpleAssignment.prg")]
        public void ShouldParseAndEvaluateSimpleAssignment()
        {
            Parser parser = new Parser(File.OpenText("SimpleAssignment.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleIf.prg")]
        public void ShouldParseAndEvaluateSimpleIf()
        {
            Parser parser = new Parser(File.OpenText("SimpleIf.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("positive", environment.GetValue("bar"));
        }

        [TestMethod]
        [DeploymentItem("Examples\\SimpleProcedure.prg")]
        public void ShouldParseAndEvaluateSimpleProcedure()
        {
            Parser parser = new Parser(File.OpenText("SimpleProcedure.prg"));
            ICommand command = parser.ParseCommandList();
            ValueEnvironment environment = new ValueEnvironment(ValueEnvironmentType.Public);

            command.Execute(null, environment);

            object result = environment.GetValue("setbar");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Procedure));
        }
    }
}
