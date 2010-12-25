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
    }
}
