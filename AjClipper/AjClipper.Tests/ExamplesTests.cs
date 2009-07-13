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
    }
}
