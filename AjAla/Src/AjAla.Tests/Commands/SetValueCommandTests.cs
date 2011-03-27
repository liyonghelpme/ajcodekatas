using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjAla.Language;
using AjAla.Expressions;
using AjAla.Commands;

namespace AjAla.Tests.Commands
{
    [TestClass]
    public class SetValueCommandTests
    {
        [TestMethod]
        public void CreateAndExecuteSetValueCommand()
        {
            IExpression expression = new ConstantExpression(1);
            SetValueCommand cmd = new SetValueCommand("one", expression);
            Assert.AreEqual(expression, cmd.Expression);
            Assert.AreEqual("one", cmd.Name);
            Context context = new Context();
            cmd.Execute(context);
            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}

