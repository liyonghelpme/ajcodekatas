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
            Assert.IsNotNull(behavior.Behavior);
            Assert.IsNull(behavior.Parent);
            Assert.IsNotNull(behavior.Methods);
            Assert.AreEqual(0, behavior.Size);
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

            behavior.Send("addMethod:at:", "aMethod", method);

            Assert.AreEqual(method, behavior.Lookup("aMethod"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenLookup()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.Lookup(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenAddMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.Send("addMethod:at:", null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseExceptionIfMethodIsNullWhenAddMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.Send("addMethod:at:", "aMethod", null);
        }

        [TestMethod]
        public void HasLookupMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            IMethod lookupMethod = (IMethod) behavior.Send("lookup:", "lookup:");

            Assert.IsNotNull(lookupMethod);
            Assert.IsInstanceOfType(lookupMethod, typeof(BaseLookupMethod));
        }

        [TestMethod]
        public void HasAddMethodMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            IMethod addMethodMethod = (IMethod) behavior.Send("lookup:", "addMethod:at:");

            Assert.IsNotNull(addMethodMethod);
            Assert.IsInstanceOfType(addMethodMethod, typeof(BaseAddMethodMethod));
        }
    }
}
