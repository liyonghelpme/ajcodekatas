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

            Assert.IsTrue(GroupUtilities.AreEqual(group3, symm));
            Assert.IsTrue(group3.Equals(symm));
        }

        [TestMethod]
        public void PrimeRotationGroupsAreEqual()
        {
            IElement elementrot = Element.CreateRotation(5);
            IElement elementrot2 = elementrot.Multiply(elementrot);

            IGroup group1 = new GeneratedGroup(elementrot);
            IGroup group2 = new GeneratedGroup(elementrot2);

            Assert.IsTrue(GroupUtilities.AreEqual(group1, group2));
        }

        [TestMethod]
        public void GroupsGeneratedByDiffSizedSwapsAreEqual()
        {
            IGroup group1 = new GeneratedGroup(Element.CreateSwap(3));
            IGroup group2 = new GeneratedGroup(Element.CreateSwap(4));

            Assert.IsTrue(GroupUtilities.AreEqual(group1, group2));
        }

        [TestMethod]
        public void GroupsGeneratedByDiffPositionSwapsAreNotEqual()
        {
            IElement swap1 = Element.CreateSwap(3);
            IElement swap2 = Element.CreateRotation(4).Multiply(Element.CreateSwap(4));
            IGroup group1 = new GeneratedGroup(swap1);
            IGroup group2 = new GeneratedGroup(swap2);

            Assert.IsFalse(GroupUtilities.AreEqual(group1, group2));
        }

        [TestMethod]
        public void GroupsGeneratedByDiffPositionSwapsAreIsomorphic()
        {
            IElement swap1 = Element.CreateSwap(3);
            IElement swap2 = Element.CreateSwap(3, 1);
            IGroup group1 = new GeneratedGroup(swap1);
            IGroup group2 = new GeneratedGroup(swap2);

            Assert.IsTrue(GroupUtilities.AreIsomorphic(group1, group2));
        }

        [TestMethod]
        public void GroupsGeneratedByDiffSizedSwapsAreIsomorphic()
        {
            IGroup group1 = new GeneratedGroup(Element.CreateSwap(3));
            IGroup group2 = new GeneratedGroup(Element.CreateSwap(4));

            Assert.IsTrue(GroupUtilities.AreIsomorphic(group1, group2));
        }

        [TestMethod]
        public void TwoSymetricGroupsAreIsomorphic()
        {
            IGroup group1 = new GeneratedGroup(Element.CreateSwap(4), Element.CreateRotation(4));
            IGroup group2 = new GeneratedGroup(Element.CreateSwap(4,2), Element.CreateRotation(4));

            Assert.IsTrue(GroupUtilities.AreIsomorphic(group1, group2));
        }

        [TestMethod]
        public void TwoGroupsGeneratedByTwoNonOverlappingSwapsAreIsomorphic()
        {
            IGroup group1 = new GeneratedGroup(Element.CreateSwap(4), Element.CreateSwap(4, 2));
            IGroup group2 = new GeneratedGroup(Element.CreateSwap(6, 2), Element.CreateSwap(6, 4));

            Assert.IsTrue(GroupUtilities.AreIsomorphic(group1, group2));
        }

        [TestMethod]
        public void GroupAndIdentityAreSubnormalGroups()
        {
            IGroup group = new SymmetricGroup(3);
            IGroup id = new GeneratedGroup(Element.CreateIdentity(3));

            GroupUtilities.IsNormalSubgroup(group, group);
            GroupUtilities.IsNormalSubgroup(id, group);
        }

        [TestMethod]
        public void SubgroupsIncludesIdentityAndTotalGroup()
        {
            IGroup group = new SymmetricGroup(3);
            IGroup id = new GeneratedGroup(Element.CreateIdentity(3));

            IEnumerable<IGroup> subgroups = GroupUtilities.GetSubgroups(group);
            Assert.IsTrue(subgroups.Contains(id));
            Assert.IsTrue(subgroups.Contains(group));
        }

        [TestMethod]
        public void IdentityIsNormalSubgroup()
        {
            IGroup group = new SymmetricGroup(3);
            IGroup id = new GeneratedGroup(Element.CreateIdentity(3));

            Assert.IsTrue(GroupUtilities.IsNormalSubgroup(id, group));
        }

        [TestMethod]
        public void GroupIsNormalSubgroup()
        {
            IGroup group = new SymmetricGroup(3);

            Assert.IsTrue(GroupUtilities.IsNormalSubgroup(group, group));
        }

        [TestMethod]
        public void GetSubnormalGroups()
        {
            IGroup group = new SymmetricGroup(3);
            IEnumerable<IGroup> normalsg = GroupUtilities.GetNormalSubgroups(group);

            Assert.IsTrue(normalsg.Count() >= 2);
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

        [TestMethod]
        public void RotationsAreCyclic()
        {
            for (int k = 1; k <= 7; k++)
            {
                IGroup group = new GeneratedGroup(Element.CreateRotation(k));
                Assert.IsTrue(GroupUtilities.IsCyclic(group));
            }
        }

        [TestMethod]
        public void SwapsAreCyclic()
        {
            for (int k = 2; k <= 10; k++)
            {
                IGroup group = new GeneratedGroup(Element.CreateSwap(k));
                Assert.IsTrue(GroupUtilities.IsCyclic(group));
            }
        }

        [TestMethod]
        public void SymmetricAreNotCyclic()
        {
            for (int k = 3; k <= 5; k++)
            {
                IGroup group = new SymmetricGroup(k);
                Assert.IsFalse(GroupUtilities.IsCyclic(group));
            }
        }
    }
}
