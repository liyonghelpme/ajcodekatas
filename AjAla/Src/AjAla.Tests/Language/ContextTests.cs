namespace AjAla.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjAla.Language;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();
            context.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void SetAndGetVariable()
        {
            Context context = new Context();
            context.SetVariable("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void SetResetAndGetVariable()
        {
            Context context = new Context();
            context.SetVariable("one", 1);
            context.SetVariable("one", 2);
            Assert.AreEqual(2, context.GetValue("one"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenResetValue()
        {
            Context context = new Context();
            context.SetValue("one", 1);
            context.SetValue("one", 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenTryingAssignAnIncompatibleValueToVariable()
        {
            Context context = new Context();
            context.SetVariable("one", 1);
            context.SetVariable("one", "one");
        }
    }
}

