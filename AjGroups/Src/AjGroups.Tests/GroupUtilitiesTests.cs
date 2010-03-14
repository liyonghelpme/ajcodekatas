namespace AjGroups.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GroupUtilitiesTests
    {
        [TestMethod]
        public void MultiplyBeIdempotent()
        {
            IGroup group = new SymmetricGroup(3);

            IGroup group2 = GroupUtilities.Multiply(group, group);

            Assert.AreEqual(group.Order, group2.Order);
        }

        [TestMethod]
        public void MultiplyGenerateSymmetricGroup()
        {
            IGroup group1 = new GeneratedGroup(Element.CreateRotation(4));
            IGroup group2 = new GeneratedGroup(Element.CreateSwap(4));

            IGroup group3 = GroupUtilities.Multiply(group1, group2);

            Assert.AreEqual(24, group3.Order);

            IGroup symm = new SymmetricGroup(4);

            Assert.IsTrue(GroupUtilities.AreEquals(group3, symm));
            Assert.IsTrue(group3.Equals(symm));
        }

        [TestMethod]
        public void GetSubgroupsOfMinimalGroup()
        {
            IGroup group = new SymmetricGroup(2);

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(2, subgroups.Count());
        }

        [TestMethod]
        public void GetSubgroupsOfSymetricGroupThree()
        {
            IGroup group = new SymmetricGroup(3);

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(6, subgroups.Count());
        }

        [TestMethod]
        public void GetSubgroupsOfSymetricGroupFour()
        {
            IGroup group = new SymmetricGroup(4);

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(30, subgroups.Count());
        }

        [TestMethod]
        public void GetSubgroupsOfCyclicGroupFour()
        {
            IGroup group = new GeneratedGroup(Element.CreateRotation(4));

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(3, subgroups.Count());
        }

        [TestMethod]
        public void GetSubgroupsOfCyclicGroupFive()
        {
            IGroup group = new GeneratedGroup(Element.CreateRotation(5));

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(2, subgroups.Count());
        }

        [TestMethod]
        public void GetSubgroupsOfCyclicGroupSeven()
        {
            IGroup group = new GeneratedGroup(Element.CreateRotation(7));

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);

            Assert.IsNotNull(subgroups);
            Assert.AreEqual(2, subgroups.Count());
        }
    }
}
