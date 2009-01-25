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
            behavior.Send("methodAt:put:", "aMethod", method);

            Assert.IsNull(obj.Send("aMethod", null));

            Assert.IsTrue(method.Executed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfSelectorIsNullWhenSend()
        {
            BaseObject obj = new BaseObject();

            obj.Send(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfBehaviorIsNullWhenSend()
        {
            BaseObject obj = new BaseObject();

            obj.Send("aMethod", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfSelectorIsUnknownWhenSend()
        {
            BaseObject obj = new BaseObject();
            BaseBehavior behavior = new BaseBehavior();
            obj.Behavior = behavior;

            obj.Send("aMethod", null);
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            BaseObject obj = new BaseObject(1);

            obj.SetValueAt(0, "anyValue");

            object value = obj.GetValueAt(0);

            Assert.IsNotNull(value);
            Assert.AreEqual("anyValue", value);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShouldRaiseIfGetOutOfRange()
        {
            BaseObject obj = new BaseObject(1);

            obj.GetValueAt(1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShouldRaiseIfSetOutOfRange()
        {
            BaseObject obj = new BaseObject(1);

            obj.SetValueAt(1, "anyValue");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldRaiseIfNoValues()
        {
            BaseObject obj = new BaseObject();
            obj.SetValueAt(0, "anyValue");
        }
    }
}
