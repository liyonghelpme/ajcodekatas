using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberTheory.Tests
{
    [TestClass]
    public class NumbersTests
    {
        [TestMethod]
        public void GreaterCommonDivisor()
        {
            Assert.AreEqual(1, Numbers.GreaterCommonDivisor(1, 1));
            Assert.AreEqual(1, Numbers.GreaterCommonDivisor(7, 5));
            Assert.AreEqual(1, Numbers.GreaterCommonDivisor(5, 7));
            Assert.AreEqual(1, Numbers.GreaterCommonDivisor(-5, -7));
            Assert.AreEqual(2, Numbers.GreaterCommonDivisor(6, 4));
        }

        [TestMethod]
        public void Lagrange()
        {
            Assert.AreEqual(0, Numbers.Lagrange(2, 4));
            Assert.AreEqual(1, Numbers.Lagrange(1, 7));
            Assert.AreEqual(1, Numbers.Lagrange(2, 7));
            Assert.AreEqual(-1, Numbers.Lagrange(3, 7));
        }
    }
}
