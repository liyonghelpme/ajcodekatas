namespace AjGroups.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGroups;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ElementTests
    {
        [TestMethod]
        public void ShouldCreate()
        {
            Element element = new Element(0, 1, 2);

            Assert.IsNotNull(element);
            Assert.AreEqual(3, element.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfValueIsRepeated()
        {
            Element element = new Element(0, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfValueOutOfRange()
        {
            Element element = new Element(0, 1, 10);
        }

        [TestMethod]
        public void ShouldBeEqual()
        {
            Element element1 = new Element(0, 1, 2);
            Element element2 = new Element(0, 1, 2);

            Assert.IsTrue(element1.Equals(element2));
            Assert.AreEqual(element1.GetHashCode(), element2.GetHashCode());
        }

        [TestMethod]
        public void ShouldBeNotEqual()
        {
            Element element1 = new Element(0, 1, 2);
            Element element2 = new Element(2, 1, 0);

            Assert.IsFalse(element1.Equals(element2));
        }

        [TestMethod]
        public void ShouldMultiplyIdentity()
        {
            Element element = new Element(0, 1, 2);

            Assert.AreEqual(element, element.Multiply(element));
        }

        [TestMethod]
        public void ShouldMultiplySwitch()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(1, 0, 2);

            Assert.AreEqual(identity, element.Multiply(element));
        }

        [TestMethod]
        public void ShouldMultiplyRotateLeft()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(1, 2, 0);
            Element element2 = new Element(2, 0, 1);

            Assert.AreEqual(element2, element.Multiply(element));
            Assert.AreEqual(identity, element2.Multiply(element));
        }

        [TestMethod]
        public void ShouldMultiplyRotateRight()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(2, 0, 1);
            Element element2 = new Element(1, 2, 0);

            Assert.AreEqual(element2, element.Multiply(element));
            Assert.AreEqual(identity, element2.Multiply(element));
        }

        [TestMethod]
        public void ShouldCreateIdentity()
        {
            Element element = Element.CreateIdentity(4);
            Element identity = new Element(0, 1, 2, 3);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(identity));
            Assert.AreEqual(1, element.Order);
        }

        [TestMethod]
        public void ShouldCreateSwap()
        {
            Element element = Element.CreateSwap(4);
            Element swap = new Element(1, 0, 2, 3);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(swap));
            Assert.AreEqual(2, element.Order);
        }

        [TestMethod]
        public void ShouldCreateRotation()
        {
            Element element = Element.CreateRotation(4);
            Element rotation = new Element(1, 2, 3, 0);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(rotation));
            Assert.AreEqual(4, element.Order);
        }
    }
}

