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
        public void CreateWithIdentity()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateIdentity(4));

            Assert.IsNotNull(group);
            Assert.AreEqual(1, group.Order);
        }

        [TestMethod]
        public void CreateWithSwapOrderTwo()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(2));

            Assert.IsNotNull(group);
            Assert.AreEqual(2, group.Order);
        }

        [TestMethod]
        public void CreateWithSwapOrderSix()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(6));

            Assert.IsNotNull(group);
            Assert.AreEqual(2, group.Order);
        }

        [TestMethod]
        public void CreateSymetricGroupSizeThree()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(3), Element.CreateRotation(3));

            Assert.IsNotNull(group);
            Assert.AreEqual(6, group.Order);
        }

        [TestMethod]
        public void CreateSymetricGroupSizeFour()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(4), Element.CreateRotation(4));

            Assert.IsNotNull(group);
            Assert.AreEqual(24, group.Order);
        }

        [TestMethod]
        public void CreateSymetricGroups()
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
        [TestMethod]
        public void SymetricGroupSizeFourAreEqualToSymetrycGroup()
        {
            GeneratedGroup group = new GeneratedGroup(Element.CreateSwap(4), Element.CreateRotation(4));
            SymmetricGroup group2 = new SymmetricGroup(4);

            Assert.AreEqual(group.Order, group2.Order);
            Assert.AreEqual(group.GetHashCode(), group2.GetHashCode());
            Assert.AreEqual(group, group2);
        }
    }
}
