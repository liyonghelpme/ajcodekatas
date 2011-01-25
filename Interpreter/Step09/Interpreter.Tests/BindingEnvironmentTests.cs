namespace Interpreter.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Interpreter.Expressions;

    [TestClass]
    public class BindingEnvironmentTests
    {
        [TestMethod]
        public void SetAndGetValue()
        {
            BindingEnvironment environment = new BindingEnvironment();

            environment.SetValue("one", 1);
            Assert.AreEqual(1, environment.GetValue("one"));
        }

        [TestMethod]
        public void GetNullIfUndefinedName()
        {
            BindingEnvironment environment = new BindingEnvironment();

            Assert.IsNull(environment.GetValue("undefined"));
        }
    }
}
