namespace AjSoda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjSoda;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseBehaviorTests
    {
        [TestMethod]
        public void CreateBaseBehavior()
        {
            BaseBehavior behavior = new BaseBehavior();

            Assert.IsNotNull(behavior);
            Assert.IsNull(behavior.Behavior);
        }

        [TestMethod]
        public void LookupUnknownMethodReturnsNull()
        {
            BaseBehavior behavior = new BaseBehavior();

            Assert.IsNull(behavior.Lookup("unknown"));
        }

        [TestMethod]
        public void AddAndLookupMethod()
        {
            BaseBehavior behavior = new BaseBehavior();
            MockMethod method = new MockMethod();

            behavior.AddMethod("aMethod", method);

            Assert.AreEqual(method, behavior.Lookup("aMethod"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfSelectorIsNullWhenLookup()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.Lookup(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfSelectorIsNullWhenAddMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.AddMethod(null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfMethodIsNullWhenAddMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.AddMethod("aMethod", null);
        }
    }
}
