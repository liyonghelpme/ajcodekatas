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
        public void LookupMethods()
        {
            BaseBehavior behavior = new BaseBehavior();

            Assert.IsNotNull(behavior.Lookup("lookup:"));
            Assert.IsNotNull(behavior.Lookup("allocate:"));
            Assert.IsNotNull(behavior.Lookup("delegated"));
            Assert.IsNotNull(behavior.Lookup("methodAt:put:"));
            Assert.IsNotNull(behavior.Lookup("vtable"));
        }

        [TestMethod]
        public void LookupUnknownMethodUsingSendReturnsNull()
        {
            BaseBehavior behavior = new BaseBehavior();

            Assert.IsNull(behavior.Send("lookup:","unknown"));
        }

        [TestMethod]
        public void AllocateObjectWithNoSize()
        {
            BaseBehavior behavior = new BaseBehavior();

            IObject obj = behavior.Allocate(0);

            Assert.IsNotNull(obj);
            Assert.AreEqual(0, obj.Size);
        }

        [TestMethod]
        public void AllocateObjectWithNoSizeUsingSend()
        {
            BaseBehavior behavior = new BaseBehavior();

            IObject obj = (IObject) behavior.Send("allocate:", 0);

            Assert.IsNotNull(obj);
            Assert.AreEqual(0, obj.Size);
        }

        [TestMethod]
        public void AllocateObjectWithSize()
        {
            BaseBehavior behavior = new BaseBehavior();

            IObject obj = behavior.Allocate(10);

            Assert.IsNotNull(obj);
            Assert.AreEqual(10, obj.Size);
        }

        [TestMethod]
        public void AllocateObjectWithSizeUsingSend()
        {
            BaseBehavior behavior = new BaseBehavior();

            IObject obj = (IObject) behavior.Send("allocate:", 10);

            Assert.IsNotNull(obj);
            Assert.AreEqual(10, obj.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfSizeIsNegative()
        {
            BaseBehavior behavior = new BaseBehavior();

            IObject obj = behavior.Allocate(-1);
        }

        [TestMethod]
        public void AddAndLookupMethod()
        {
            BaseBehavior behavior = new BaseBehavior();
            MockMethod method = new MockMethod();

            behavior.Send("methodAt:put:", "aMethod", method);

            Assert.AreEqual(method, behavior.Lookup("aMethod"));
            Assert.AreEqual(method, behavior.Send("lookup:", "aMethod"));
        }

        [TestMethod]
        public void ShouldGetBehavior()
        {
            BaseBehavior behavior = new BaseBehavior();

            Assert.AreEqual(behavior.Behavior, behavior.Send("vtable"));
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

            behavior.Send("methodAt:put:", null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseExceptionIfMethodIsNullWhenAddMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            behavior.Send("methodAt:put:", "aMethod", null);
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

            IMethod addMethodMethod = (IMethod) behavior.Send("lookup:", "methodAt:put:");

            Assert.IsNotNull(addMethodMethod);
            Assert.IsInstanceOfType(addMethodMethod, typeof(BaseAddMethodMethod));
        }

        [TestMethod]
        public void HasBehaviorMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            IMethod behaviorMethod = (IMethod)behavior.Send("lookup:", "vtable");

            Assert.IsNotNull(behaviorMethod);
            Assert.IsInstanceOfType(behaviorMethod, typeof(BaseBehaviorMethod));
        }

        [TestMethod]
        public void HasDelegateMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            IMethod delegateMethod = (IMethod)behavior.Send("lookup:", "delegated");

            Assert.IsNotNull(delegateMethod);
            Assert.IsInstanceOfType(delegateMethod, typeof(BaseDelegateMethod));
        }

        [TestMethod]
        public void HasAllocateMethod()
        {
            BaseBehavior behavior = new BaseBehavior();

            IMethod allocateMethod = (IMethod)behavior.Send("lookup:", "allocate:");

            Assert.IsNotNull(allocateMethod);
            Assert.IsInstanceOfType(allocateMethod, typeof(BaseAllocateMethod));
        }
    }
}
