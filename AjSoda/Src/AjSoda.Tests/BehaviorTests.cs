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
            Behavior behavior = new Behavior();

            Assert.IsNotNull(behavior);
            Assert.IsNull(behavior.GetValueAt(0));
            Assert.IsNotNull(behavior.GetValueAt(1));
            Assert.IsInstanceOfType(behavior.GetValueAt(1), typeof(IDictionary<string, IMethod>));
        }

        [TestMethod]
        public void CreateBehaviorWithBaseBehavior()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);

            Assert.IsNotNull(behavior);
            Assert.IsNotNull(behavior.Behavior);
            Assert.IsInstanceOfType(behavior.Behavior, typeof(BaseBehavior));
            Assert.AreEqual(baseBehavior, behavior.Behavior);
            Assert.IsNotNull(behavior.GetValueAt(1));
            Assert.IsInstanceOfType(behavior.GetValueAt(1), typeof(IDictionary<string, IMethod>));
        }

        [TestMethod]
        public void AddMethodAndLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);

            IMethod method = new MockMethod();

            behavior.AddMethod("aMethod", method);

            IMethod retrievedMethod = behavior.Lookup("aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void LookupWithParent()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);
            Behavior childBehavior = new Behavior(baseBehavior);

            childBehavior.SetValueAt(0, behavior);

            IMethod method = new MockMethod();

            behavior.AddMethod("aMethod", method);

            IMethod retrievedMethod = childBehavior.Lookup("aMethod");

            Assert.IsNotNull(retrievedMethod);
            Assert.AreEqual(method, retrievedMethod);
        }

        [TestMethod]
        public void NullIfUnknownMethodInLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);

            IMethod retrievedMethod = behavior.Lookup("unknownMethod");

            Assert.IsNull(retrievedMethod);
        }

        [TestMethod]
        public void RedefineLookup()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);

            IMethod method = new BaseLookupMethod();

            behavior.AddMethod("lookup:", method);

            IMethod newMethod = behavior.Lookup("lookup:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);
        }

        [TestMethod]
        public void RedefineAddMethod()
        {
            BaseBehavior baseBehavior = new BaseBehavior();
            Behavior behavior = new Behavior(baseBehavior);

            IMethod method = new BaseAddMethodMethod();

            behavior.AddMethod("addMethod:at:", method);

            IMethod newMethod = behavior.Lookup("addMethod:at:");

            Assert.IsNotNull(newMethod);
            Assert.AreEqual(method, newMethod);

            IMethod anotherMethod = new MockMethod();

            behavior.AddMethod("anotherMethod", anotherMethod);

            newMethod = behavior.Lookup("anotherMethod");

            Assert.AreEqual(anotherMethod, newMethod);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenLookup()
        {
            Behavior behavior = new Behavior();

            behavior.Lookup(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenAddMethod()
        {
            Behavior behavior = new Behavior();

            behavior.AddMethod(null, new MockMethod());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfMethodIsNullWhenAddMethod()
        {
            Behavior behavior = new Behavior();

            behavior.AddMethod("aMethod", null);
        }
    }
}
