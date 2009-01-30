namespace AjGroups.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeneratedGroupTests
    {
        [TestMethod]
        public void ShouldCreateWithIdentity()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateIdentity(4));

            Assert.IsNotNull(group);
            Assert.AreEqual(1, group.Order);
        }

        [TestMethod]
        public void ShouldCreateWithSwapOrderTwo()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(2));

            Assert.IsNotNull(group);
            Assert.AreEqual(2, group.Order);
        }

        [TestMethod]
        public void ShouldCreateWithSwapOrderSix()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(6));

            Assert.IsNotNull(group);
            Assert.AreEqual(2, group.Order);
        }

        [TestMethod]
        public void ShouldCreateSymetricGroupSizeThree()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(3), Element.CreateRotation(3));

            Assert.IsNotNull(group);
            Assert.AreEqual(6, group.Order);
        }

        [TestMethod]
        public void ShouldCreateSymetricGroupSizeFour()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(4), Element.CreateRotation(4));

            Assert.IsNotNull(group);
            Assert.AreEqual(24, group.Order);
        }

        [TestMethod]
        public void ShouldCreateSymetricGroups()
        {
            int n = 1;

            for (int k = 2; k <= 5; k++)
            {
                GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(k), Element.CreateRotation(k));
                n *= k;

                Assert.IsNotNull(group);
                Assert.AreEqual(n, group.Order);
            }
        }
    }
}
