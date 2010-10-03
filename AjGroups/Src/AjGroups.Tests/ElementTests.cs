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
        public void Create()
        {
            Element element = new Element(0, 1, 2);

            Assert.IsNotNull(element);
            Assert.AreEqual(3, element.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfValueIsRepeated()
        {
            Element element = new Element(0, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfValueOutOfRange()
        {
            Element element = new Element(0, 1, 10);
        }

        [TestMethod]
        public void BeEqual()
        {
            Element element1 = new Element(0, 1, 2);
            Element element2 = new Element(0, 1, 2);

            Assert.IsTrue(element1.Equals(element2));
            Assert.AreEqual(element1.GetHashCode(), element2.GetHashCode());
        }

        [TestMethod]
        public void BeNotEqual()
        {
            Element element1 = new Element(0, 1, 2);
            Element element2 = new Element(2, 1, 0);

            Assert.IsFalse(element1.Equals(element2));
        }

        [TestMethod]
        public void MultiplyIdentity()
        {
            Element element = new Element(0, 1, 2);

            Assert.AreEqual(element, element.Multiply(element));
        }

        [TestMethod]
        public void MultiplySwitch()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(1, 0, 2);

            Assert.AreEqual(identity, element.Multiply(element));
        }

        [TestMethod]
        public void MultiplyRotateLeft()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(1, 2, 0);
            Element element2 = new Element(2, 0, 1);

            Assert.AreEqual(element2, element.Multiply(element));
            Assert.AreEqual(identity, element2.Multiply(element));
        }

        [TestMethod]
        public void MultiplyRotateRight()
        {
            Element identity = new Element(0, 1, 2);
            Element element = new Element(2, 0, 1);
            Element element2 = new Element(1, 2, 0);

            Assert.AreEqual(element2, element.Multiply(element));
            Assert.AreEqual(identity, element2.Multiply(element));
        }

        [TestMethod]
        public void MultiplyDifferentSizes()
        {
            Element swap3 = Element.CreateSwap(3);
            Element swap7 = Element.CreateSwap(7);
            Element identity7 = Element.CreateIdentity(7);

            Element element = swap3.Multiply(swap7);

            Assert.IsNotNull(element);
            Assert.AreEqual(7, element.Size);
            Assert.AreEqual(1, element.Order);
            Assert.IsTrue(element.Equals(identity7));

            element = swap7.Multiply(swap3);

            Assert.IsNotNull(element);
            Assert.AreEqual(7, element.Size);
            Assert.AreEqual(1, element.Order);
            Assert.IsTrue(element.Equals(identity7));
        }

        [TestMethod]
        public void CreateIdentity()
        {
            Element element = Element.CreateIdentity(4);
            Element identity = new Element(0, 1, 2, 3);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(identity));
            Assert.AreEqual(1, element.Order);
        }

        [TestMethod]
        public void TwoIdentitiesAreEqual()
        {
            Element id3 = Element.CreateIdentity(3);
            Element id4 = Element.CreateIdentity(4);

            Assert.AreEqual(id3.GetHashCode(), id4.GetHashCode());
            Assert.IsTrue(id3.Equals(id4));
            Assert.IsTrue(id4.Equals(id3));
        }

        [TestMethod]
        public void TwoSwapsAreEqual()
        {
            Element sw3 = Element.CreateSwap(3);
            Element sw6 = Element.CreateSwap(6);

            Assert.AreEqual(sw3.GetHashCode(), sw6.GetHashCode());
            Assert.IsTrue(sw3.Equals(sw6));
            Assert.IsTrue(sw6.Equals(sw3));
        }

        [TestMethod]
        public void CreateSwap()
        {
            Element element = Element.CreateSwap(4);
            Element swap = new Element(1, 0, 2, 3);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(swap));
            Assert.AreEqual(2, element.Order);
        }

        [TestMethod]
        public void CreateRotation()
        {
            Element element = Element.CreateRotation(4);
            Element rotation = new Element(1, 2, 3, 0);

            Assert.IsNotNull(element);
            Assert.IsTrue(element.Equals(rotation));
            Assert.AreEqual(4, element.Order);
        }
    }
}

