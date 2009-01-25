namespace AjSoda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjSoda;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BehaviorTests
    {
        [TestMethod]
        public void CreateBehavior()
        {
            IObject behavior = new BaseObject(2);

            Assert.IsNotNull(behavior);
            Assert.IsNull(behavior.GetValueAt(0));
            Assert.IsNull(behavior.GetValueAt(1));
        }

        [TestMethod]
        public void CreateBehaviorWithBaseBehavior()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            Assert.IsNotNull(behavior);
            Assert.IsNotNull(behavior.Behavior);
            Assert.IsInstanceOfType(behavior, typeof(BaseBehavior));
            Assert.IsInstanceOfType(behavior.Behavior, typeof(BaseBehavior));
            Assert.AreEqual(baseBehavior, behavior.Behavior);
            Assert.IsNotNull(behavior.Parent);
            Assert.IsNotNull(behavior.Methods);
            Assert.AreEqual(behavior.Parent, baseBehavior);
        }

        [TestMethod]
        public void CreateBehaviorSendingDelegateMessage()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = (IBehavior) baseBehavior.Send("delegated");

            Assert.IsNotNull(behavior);
            Assert.IsNotNull(behavior.Behavior);
            Assert.IsInstanceOfType(behavior, typeof(BaseBehavior));
            Assert.IsInstanceOfType(behavior.Behavior, typeof(BaseBehavior));
            Assert.AreEqual(baseBehavior, behavior.Behavior);
            Assert.IsNotNull(behavior.Parent);
            Assert.IsNotNull(behavior.Methods);
            Assert.AreEqual(behavior.Parent, baseBehavior);
        }

        [TestMethod]
        public void AddMethodAndLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            IMethod method = new MockMethod();

            behavior.Send("methodAt:put:", "aMethod", method);

            IMethod retrievedMethod = (IMethod) behavior.Send("lookup:", "aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void LookupWithParent()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();
            IObject childBehavior = behavior.CreateDelegated();

            IMethod method = new MockMethod();

            behavior.Send("methodAt:put:", "aMethod", method);

            IMethod retrievedMethod = (IMethod) childBehavior.Send("lookup:", "aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void NullIfUnknownMethodInLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            IMethod retrievedMethod = (IMethod) behavior.Send("lookup:", "unknownMethod");

            Assert.IsNull(retrievedMethod);
        }

        [TestMethod]
        public void RedefineLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            IMethod method = new BaseLookupMethod();

            behavior.Send("methodAt:put:", "lookup:", method);

            IMethod newMethod = (IMethod) behavior.Send("lookup:", "lookup:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);
        }

        [TestMethod]
        public void RedefineAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            IMethod method = new BaseAddMethodMethod();

            behavior.Send("methodAt:put:", "methodAt:put:", method);

            IMethod newMethod = (IMethod) behavior.Send("lookup:", "methodAt:put:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);

            IMethod anotherMethod = new MockMethod();

            behavior.Send("methodAt:put:", "anotherMethod", anotherMethod);

            newMethod = (IMethod) behavior.Send("lookup:", "anotherMethod");

            Assert.AreEqual(anotherMethod, newMethod);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            behavior.Send("lookup:", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            behavior.Send("methodAt:put:", null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfMethodIsNullWhenAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IBehavior behavior = baseBehavior.CreateDelegated();

            behavior.Send("methodAt:put:", "aMethod", null);
        }
    }
}
