namespace AjSoda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjSoda;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseObjectTests
    {
        [TestMethod]
        public void CreateBaseObject()
        {
            BaseObject obj = new BaseObject();

            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Behavior);
        }

        [TestMethod]
        public void CreateBaseObjectAndSetBehavior()
        {
            BaseObject obj = new BaseObject();
            BaseBehavior behavior = new BaseBehavior();

            obj.Behavior = behavior;

            Assert.AreEqual(behavior, obj.Behavior);
        }

        [TestMethod]
        public void SendMessage()
        {
            BaseObject obj = new BaseObject();
            BaseBehavior behavior = new BaseBehavior();

            obj.Behavior = behavior;

            MockMethod method = new MockMethod();

            Assert.IsFalse(method.Executed);
            behavior.AddMethod("aMethod", method);

            Assert.IsNull(obj.Send("aMethod", null));

            Assert.IsTrue(method.Executed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfSelectorIsNullWhenSend()
        {
            BaseObject obj = new BaseObject();

            obj.Send(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfBehaviorIsNullWhenSend()
        {
            BaseObject obj = new BaseObject();

            obj.Send("aMethod", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfSelectorIsUnknownWhenSend()
        {
            BaseObject obj = new BaseObject();
            BaseBehavior behavior = new BaseBehavior();
            obj.Behavior = behavior;

            obj.Send("aMethod", null);
        }
    }
}
