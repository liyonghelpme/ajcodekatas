namespace AjGroups.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OperationTableTests
    {
        [TestMethod]
        public void CalculateSymmetricGroupTable()
        {
            IGroup group = new SymmetricGroup(3);

            OperationTable table = new OperationTable(group.Elements);

            table.Calculate();

            foreach (IElement left in group.Elements)
                foreach (IElement right in group.Elements)
                    Assert.AreEqual(left.Multiply(right), table.GetValue(left, right));
        }

        [TestMethod]
        public void SymmetricGroupThreeOperationIsAssociative()
        {
            OperationTable table = new OperationTable((new SymmetricGroup(3)).Elements);
            table.Calculate();

            Assert.IsTrue(table.IsAssociative);
        }

        [TestMethod]
        public void SymmetricGroupThreeOperationIsClosed()
        {
            OperationTable table = new OperationTable((new SymmetricGroup(3)).Elements);
            table.Calculate();

            Assert.IsTrue(table.IsClosed);
        }

        [TestMethod]
        public void SymmetricGroupThreeOperationHasIdentity()
        {
            OperationTable table = new OperationTable((new SymmetricGroup(3)).Elements);
            table.Calculate();

            Assert.IsTrue(table.HasIdentity);
        }

        [TestMethod]
        public void SymmetricGroupThreeOperationIsNotCommutative()
        {
            OperationTable table = new OperationTable((new SymmetricGroup(3)).Elements);
            table.Calculate();

            Assert.IsFalse(table.IsCommutative);
        }

        [TestMethod]
        public void RotationGroupThreeOperationIsCommutative()
        {
            OperationTable table = new OperationTable((new GeneratedGroup(Element.CreateRotation(3))).Elements);
            table.Calculate();

            Assert.IsTrue(table.IsCommutative);
        }

        [TestMethod]
        public void CreateWithNamedIdentity()
        {
            NamedElement identity = new NamedElement('e');
            OperationTable table = new OperationTable(new List<IElement>() { identity }, true);

            identity.OperationTable = table;

            Assert.IsTrue(table.HasIdentity);
            Assert.IsTrue(table.IsAssociative);
            Assert.IsTrue(table.IsClosed);
            Assert.IsTrue(table.IsCommutative);

            Assert.AreEqual(identity, table.GetValue(identity, identity));

            Assert.AreEqual(1, identity.Order);
        }

        [TestMethod]
        public void CreateWithNamedIdentityAndOneElement()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            OperationTable table = new OperationTable(new List<IElement>() { identity , aelement}, true);

            identity.OperationTable = table;
            aelement.OperationTable = table;

            table.SetValue(aelement, aelement, identity);

            Assert.IsTrue(table.HasIdentity);
            Assert.IsTrue(table.IsAssociative);
            Assert.IsTrue(table.IsClosed);
            Assert.IsTrue(table.IsCommutative);

            Assert.AreEqual(2, table.Elements.Count);

            Assert.AreEqual(identity, table.GetValue(identity, identity));
            Assert.AreEqual(aelement, table.GetValue(aelement, identity));
            Assert.AreEqual(aelement, table.GetValue(identity, aelement));
            Assert.AreEqual(identity, table.GetValue(aelement, aelement));

            Assert.AreEqual(1, identity.Order);
            Assert.AreEqual(2, aelement.Order);
        }

        [TestMethod]
        public void GetIncompatibleOperationTableIfValueIsAlreadyDefined()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement }, true);

            identity.OperationTable = table;
            aelement.OperationTable = table;

            table.SetValue(aelement, aelement, identity);

            Assert.IsNull(table.GetCompatibleTable(aelement, aelement, aelement));
        }

        [TestMethod]
        public void GetIncompatibleOperationTableIfValueIsAlreadyInRowOrColumn()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement }, true);

            identity.OperationTable = table;
            aelement.OperationTable = table;

            Assert.IsNull(table.GetCompatibleTable(aelement, aelement, aelement));
        }

        [TestMethod]
        public void GetCompatibleOperationTable()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement }, true);

            identity.OperationTable = table;
            aelement.OperationTable = table;

            OperationTable table2 = table.GetCompatibleTable(aelement, aelement, identity);
            Assert.IsNotNull(table2);
            Assert.AreNotSame(table2, table);
        }

        [TestMethod]
        public void GetCompatibleOperationTableUsingAssociationExpansion()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            NamedElement belement = new NamedElement('b');
            NamedElement celement = new NamedElement('c');

            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement, belement, celement }, true);

            identity.OperationTable = table;
            aelement.OperationTable = table;
            belement.OperationTable = table;
            celement.OperationTable = table;

            table = table.GetCompatibleTable(aelement, belement, celement);
            Assert.IsNotNull(table);

            table = table.GetCompatibleTable(belement, aelement, celement);
            Assert.IsNotNull(table);

            // bc undefined
            Assert.IsNull(table.GetValue(belement, celement));

            table = table.GetCompatibleTable(belement, celement, identity);

            Assert.IsNotNull(table);

            // bc = e, ba = c = ab -> cb = e
            Assert.AreEqual(identity, table.GetValue(celement, belement));

            // but is not complete, yet
            Assert.IsFalse(table.IsClosed);

            IList<OperationTable> solutions = table.GetSolutions().ToList();

            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count);
            Assert.IsTrue(solutions[0].IsClosed);
        }

        [TestMethod]
        public void GetGroupsOfOrderTwo()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement }, true);

            IList<OperationTable> solutions = table.GetSolutions().ToList();

            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count);
            Assert.IsTrue(solutions[0].IsClosed);
            Assert.IsTrue(solutions[0].IsAssociative);
        }

        [TestMethod]
        public void GetGroupsOfOrderThree()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            NamedElement belement = new NamedElement('b');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement, belement }, true);

            IList<OperationTable> solutions = table.GetSolutions().ToList();

            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count);
            Assert.IsTrue(solutions[0].IsClosed);
        }

        // See http://en.wikipedia.org/wiki/Finite_group
        [TestMethod]
        public void GetGroupsOfOrderFour()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            NamedElement belement = new NamedElement('b');
            NamedElement celement = new NamedElement('c');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement, belement, celement }, true);

            IList<OperationTable> solutions = table.GetSolutions().ToList();

            Assert.IsNotNull(solutions);
            Assert.AreEqual(4, solutions.Count);

            foreach (OperationTable solution in solutions)
            {
                Assert.IsTrue(solution.IsClosed);
                Assert.IsTrue(solution.IsCommutative);
            }

            IList<IGroup> groups = new List<IGroup>();

            foreach (OperationTable ot in solutions)
                groups.Add(new TableGroup(ot));

            IList<IGroup> dgroups = GroupUtilities.GetNonIsomorphic(groups);

            Assert.IsNotNull(dgroups);
            Assert.AreEqual(2, dgroups.Count);
        }

        // See http://en.wikipedia.org/wiki/Finite_group
        [TestMethod]
        public void GetGroupsOfOrderFive()
        {
            NamedElement identity = new NamedElement('e');
            NamedElement aelement = new NamedElement('a');
            NamedElement belement = new NamedElement('b');
            NamedElement celement = new NamedElement('c');
            NamedElement delement = new NamedElement('d');
            OperationTable table = new OperationTable(new List<IElement>() { identity, aelement, belement, celement, delement }, true);

            IList<OperationTable> solutions = table.GetSolutions().ToList();

            Assert.IsNotNull(solutions);
            Assert.AreEqual(6, solutions.Count);

            foreach (OperationTable solution in solutions)
            {
                Assert.IsTrue(solution.IsClosed);
                Assert.IsTrue(solution.IsCommutative);
            }

            IList<IGroup> groups = new List<IGroup>();

            foreach (OperationTable ot in solutions)
                groups.Add(new TableGroup(ot));

            foreach (IGroup group in groups)
                foreach (IElement element in group.Elements)
                    if (!element.IsIdentity)
                        Assert.AreEqual(5, element.Order);

            IList<IGroup> dgroups = GroupUtilities.GetNonIsomorphic(groups);

            Assert.IsNotNull(dgroups);
            Assert.AreEqual(1, dgroups.Count);
        }

        [TestMethod]
        public void GetGroupsOfOrderSix()
        {
            IList<IGroup> dgroups = GetGroupsOfOrder(6);

            Assert.IsNotNull(dgroups);
            Assert.AreEqual(2, dgroups.Count);

            int nconmutatives = 0;
            int nnonconmutatives = 0;

            foreach (IGroup group in dgroups)
            {
                TableGroup tgroup = (TableGroup)group;
                if (tgroup.Table.IsCommutative)
                    nconmutatives++;
                else
                    nnonconmutatives++;
            }

            Assert.AreEqual(1, nconmutatives);
            Assert.AreEqual(1, nnonconmutatives);
        }

        [TestMethod]
        public void GetGroupsOfOrderEight()
        {
            IList<IGroup> dgroups = GetGroupsOfOrder(8);

            Assert.IsNotNull(dgroups);
            Assert.AreEqual(5, dgroups.Count);

            int nconmutatives = 0;
            int nnonconmutatives = 0;

            foreach (IGroup group in dgroups)
            {
                TableGroup tgroup = (TableGroup)group;
                if (tgroup.Table.IsCommutative)
                    nconmutatives++;
                else
                    nnonconmutatives++;
            }

            Assert.AreEqual(3, nconmutatives);
            Assert.AreEqual(2, nnonconmutatives);
        }

        private static IList<IGroup> GetGroupsOfOrder(int order)
        {
            IList<IElement> elements = new List<IElement>();

            for (int k = 0; k < order; k++)
                elements.Add(new NamedElement(k));

            OperationTable table = new OperationTable(elements, true);
            IList<OperationTable> solutions = table.GetSolutions().ToList();

            foreach (OperationTable solution in solutions)
            {
                Assert.IsTrue(solution.HasIdentity);
                Assert.IsTrue(solution.IsAssociative);
                Assert.IsTrue(solution.IsClosed);
            }

            IList<IGroup> groups = new List<IGroup>();

            foreach (OperationTable ot in solutions)
                groups.Add(new TableGroup(ot));

            IList<IGroup> dgroups = GroupUtilities.GetNonIsomorphic(groups);

            return dgroups;
        }
    }
}
