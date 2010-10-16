using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Interpreter;
using Patterns.Decorator;
using Patterns.Composite;

namespace Patterns.Tests.Decorator
{
    [TestClass]
    public class DecoratorTests
    {
        [TestMethod]
        public void DebugContextRaisingSettingValueEvent()
        {
            IContext context = new Context();
            DebugContext debug = new DebugContext(context);
            
            string varname = null;
            object varvalue = null;

            debug.SettingValue += (name, value) => { varname = name; varvalue = value; };

            ICommand command = new SetCommand("foo", new ConstantExpression("bar"));
            command.Execute(debug);

            Assert.AreEqual("foo", varname);
            Assert.AreEqual("bar", varvalue);
        }

        [TestMethod]
        public void DebugContextRaisingGettingValueEvent()
        {
            IContext context = new Context();
            context.SetValue("foo", "bar");
            DebugContext debug = new DebugContext(context);

            string varname = null;
            object varvalue = null;

            debug.GettingValue += (name, value) => { varname = name; varvalue = value; };

            IExpression expression = new VariableExpression("foo");
            expression.Evaluate(debug);

            Assert.AreEqual("foo", varname);
            Assert.AreEqual("bar", varvalue);
        }
    }
}
