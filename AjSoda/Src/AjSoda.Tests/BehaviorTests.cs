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
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            Assert.IsNotNull(behavior);
            Assert.IsNotNull(behavior.Behavior);
            Assert.IsInstanceOfType(behavior.Behavior, typeof(BaseBehavior));
            Assert.AreEqual(baseBehavior, behavior.Behavior);
            Assert.IsNull(behavior.GetValueAt(0));
            Assert.IsNull(behavior.GetValueAt(1));
        }

        [TestMethod]
        public void AddMethodAndLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            IMethod method = new MockMethod();

            behavior.Send("addMethod:at:", "aMethod", method);

            IMethod retrievedMethod = (IMethod) behavior.Send("lookup:", "aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void LookupWithParent()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;
            IObject childBehavior = new BaseObject(2);
            childBehavior.Behavior = baseBehavior;

            childBehavior.SetValueAt(0, behavior);

            IMethod method = new MockMethod();

            behavior.Send("addMethod:at:", "aMethod", method);

            IMethod retrievedMethod = (IMethod) childBehavior.Send("lookup:", "aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void NullIfUnknownMethodInLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            IMethod retrievedMethod = (IMethod) behavior.Send("lookup:", "unknownMethod");

            Assert.IsNull(retrievedMethod);
        }

        [TestMethod]
        public void RedefineLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            IMethod method = new BaseLookupMethod();

            behavior.Send("addMethod:at:", "lookup:", method);

            IMethod newMethod = (IMethod) behavior.Send("lookup:", "lookup:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);
        }

        [TestMethod]
        public void RedefineAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            IMethod method = new BaseAddMethodMethod();

            behavior.Send("addMethod:at:", "addMethod:at:", method);

            IMethod newMethod = (IMethod) behavior.Send("lookup:", "addMethod:at:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);

            IMethod anotherMethod = new MockMethod();

            behavior.Send("addMethod:at:", "anotherMethod", anotherMethod);

            newMethod = (IMethod) behavior.Send("lookup:", "anotherMethod");

            Assert.AreEqual(anotherMethod, newMethod);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            behavior.Send("lookup:", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            behavior.Send("addMethod:at:", null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfMethodIsNullWhenAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            IObject behavior = new BaseObject(2);
            behavior.Behavior = baseBehavior;

            behavior.Send("addMethod:at:", "aMethod", null);
        }
    }
}
