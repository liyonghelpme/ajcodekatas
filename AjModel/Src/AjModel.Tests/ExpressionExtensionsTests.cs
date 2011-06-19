using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using AjModel.Tests.Entities;
using System.Reflection;

namespace AjModel.Tests
{
    [TestClass]
    public class ExpressionExtensionsTests
    {
        [TestMethod]
        public void ToPropertyInfo()
        {
            Expression<Func<Customer, string>> expr = c => c.Name;
            PropertyInfo info = expr.ToPropertyInfo();
            Assert.IsNotNull(info);
            Assert.AreEqual("Name", info.Name);
        }
    }
}
