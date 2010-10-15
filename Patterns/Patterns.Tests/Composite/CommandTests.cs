using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Interpreter;
using Patterns.Composite;

namespace Patterns.Tests.Composite
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void SetCommand()
        {
            Context context = new Context();
            Assert.IsNull(context.GetValue("foo"));
            SetCommand command = new SetCommand("foo", new ConstantExpression("bar"));
            command.Execute(context);
            Assert.AreEqual("bar", context.GetValue("foo"));
        }
    }
}
