namespace AjClipper.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionUtilitiesTests
    {
        [TestMethod]
        public void ShouldEvaluateFalse()
        {
            Assert.IsTrue(ExpressionUtilities.IsFalse(null));
            Assert.IsTrue(ExpressionUtilities.IsFalse(string.Empty));
            Assert.IsTrue(ExpressionUtilities.IsFalse((int)0));
            Assert.IsTrue(ExpressionUtilities.IsFalse((short)0));
            Assert.IsTrue(ExpressionUtilities.IsFalse((long)0));
            Assert.IsTrue(ExpressionUtilities.IsFalse((decimal)0));
            Assert.IsTrue(ExpressionUtilities.IsFalse((double)0));
            Assert.IsTrue(ExpressionUtilities.IsFalse((float)0));
        }

        [TestMethod]
        public void ShouldEvaluateTrue()
        {
            Assert.IsTrue(ExpressionUtilities.IsTrue("1"));
            Assert.IsTrue(ExpressionUtilities.IsTrue((int)1));
            Assert.IsTrue(ExpressionUtilities.IsTrue((short)1));
            Assert.IsTrue(ExpressionUtilities.IsTrue((long)1));
            Assert.IsTrue(ExpressionUtilities.IsTrue((decimal)1));
            Assert.IsTrue(ExpressionUtilities.IsTrue((double)1));
            Assert.IsTrue(ExpressionUtilities.IsTrue((float)1));
        }
    }
}
