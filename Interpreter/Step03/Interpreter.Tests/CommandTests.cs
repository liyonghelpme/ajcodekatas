using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interpreter.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void CreateSetCommand()
        {
            SetCommand command = new SetCommand("foo", new ConstantExpression(1));

            Assert.AreEqual("foo", command.Name);
            Assert.IsInstanceOfType(command.Expression, typeof(ConstantExpression));
        }

        [TestMethod]
        public void ExecuteSetCommand()
        {
            BindingEnvironment environment = new BindingEnvironment();
            ICommand command = new SetCommand("one", new ConstantExpression(1));

            command.Execute(environment);

            Assert.AreEqual(1, environment.GetValue("one"));
        }
    }
}
