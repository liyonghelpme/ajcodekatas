using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjGroups.Tests
{
    [TestClass]
    public class NamedElementTests
    {
        [TestMethod]
        public void SameNameAreEqual()
        {
            NamedElement element1 = new NamedElement('a');
            NamedElement element2 = new NamedElement('a');

            Assert.AreEqual(element1, element2);
            Assert.AreEqual(element1.GetHashCode(), element2.GetHashCode());
        }

        [TestMethod]
        public void DifferentNameAreNotEqual()
        {
            NamedElement element1 = new NamedElement('a');
            NamedElement element2 = new NamedElement('b');

            Assert.AreNotEqual(element1, element2);
            Assert.AreNotEqual(element1.GetHashCode(), element2.GetHashCode());
        }

        [TestMethod]
        public void GetNamesByNumber()
        {
            // e is identity
            Assert.AreEqual('e', (new NamedElement(0)).Name);
            Assert.AreEqual('a', (new NamedElement(1)).Name);
            Assert.AreEqual('b', (new NamedElement(2)).Name);
            Assert.AreEqual('c', (new NamedElement(3)).Name);
            Assert.AreEqual('d', (new NamedElement(4)).Name);
            Assert.AreEqual('f', (new NamedElement(5)).Name);
            Assert.AreEqual('g', (new NamedElement(6)).Name);
        }
    }
}
