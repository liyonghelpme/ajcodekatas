using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Interpreter;

namespace Patterns.Tests.Interpreter
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();
            context.SetValue("foo", "bar");

            Assert.AreEqual("bar", context.GetValue("foo"));
        }

        [TestMethod]
        public void GetNullIfUndefinedValue()
        {
            Context context = new Context();
            Assert.IsNull(context.GetValue("Undefined"));
        }
    }
}
