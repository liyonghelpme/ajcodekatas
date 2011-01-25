using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Predicates;

namespace Interpreter.Tests
{
    [TestClass]
    public class PredicatesTests
    {
        [TestMethod]
        public void IsFalse()
        {
            Assert.IsTrue(BooleanPredicates.IsFalse(null));
            Assert.IsTrue(BooleanPredicates.IsFalse(string.Empty));
            Assert.IsTrue(BooleanPredicates.IsFalse(0));
            Assert.IsTrue(BooleanPredicates.IsFalse((short)0));
            Assert.IsTrue(BooleanPredicates.IsFalse((long)0));
            Assert.IsTrue(BooleanPredicates.IsFalse(0.0));
            Assert.IsTrue(BooleanPredicates.IsFalse((float)0.0));

            Assert.IsFalse(BooleanPredicates.IsFalse(new object()));
            Assert.IsFalse(BooleanPredicates.IsFalse("foo"));
            Assert.IsFalse(BooleanPredicates.IsFalse(1));
            Assert.IsFalse(BooleanPredicates.IsFalse((short)2));
            Assert.IsFalse(BooleanPredicates.IsFalse((long)3));
            Assert.IsFalse(BooleanPredicates.IsFalse(4.0));
            Assert.IsFalse(BooleanPredicates.IsFalse((float)5.0));
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.IsFalse(BooleanPredicates.IsTrue(null));
            Assert.IsFalse(BooleanPredicates.IsTrue(string.Empty));
            Assert.IsFalse(BooleanPredicates.IsTrue(0));
            Assert.IsFalse(BooleanPredicates.IsTrue((short)0));
            Assert.IsFalse(BooleanPredicates.IsTrue((long)0));
            Assert.IsFalse(BooleanPredicates.IsTrue(0.0));
            Assert.IsFalse(BooleanPredicates.IsTrue((float)0.0));

            Assert.IsTrue(BooleanPredicates.IsTrue(new object()));
            Assert.IsTrue(BooleanPredicates.IsTrue("foo"));
            Assert.IsTrue(BooleanPredicates.IsTrue(1));
            Assert.IsTrue(BooleanPredicates.IsTrue((short)2));
            Assert.IsTrue(BooleanPredicates.IsTrue((long)3));
            Assert.IsTrue(BooleanPredicates.IsTrue(4.0));
            Assert.IsTrue(BooleanPredicates.IsTrue((float)5.0));
        }
    }
}
