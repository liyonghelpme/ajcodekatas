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
    }
}
