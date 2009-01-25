namespace AjGammon.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGammon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BoardPositionTests
    {
        [TestMethod]
        public void ShouldCreate()
        {
            BoardPosition position = new BoardPosition();

            Assert.IsNotNull(position);
        }

        [TestMethod]
        public void ShouldCreateEmpty()
        {
            BoardPosition position = new BoardPosition(null, null);

            Assert.IsNotNull(position);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfInvalidInitialPosition()
        {
            BoardPosition position = new BoardPosition(new int[] { 1 }, new int[] { 24 });
        }

        [TestMethod]
        public void ShouldClone()
        {
            BoardPosition position = new BoardPosition();
            BoardPosition position2 = position.Clone();

            Assert.IsNotNull(position2);
            Assert.AreNotEqual(position, position2);
            Assert.AreEqual(position.Color, position2.Color);

            for (int pos = 0; pos < BoardPosition.Size; pos++)
            {
                Assert.AreEqual(position.GetColors(pos), position2.GetColors(pos));
            }
        }

        [TestMethod]
        public void ShouldDetectValidMoves()
        {
            BoardPosition position = new BoardPosition(new int[] { 1, 1, 2, 20, 20, 21 }, new int[] { 20, 20, 21, 1, 1, 2 });

            Assert.IsTrue(position.CanMove(1, 1));
            Assert.IsTrue(position.CanMove(2, 1));
            Assert.IsFalse(position.CanMove(1, 4));
            Assert.IsTrue(position.CanMove(1, 3));

            Assert.IsTrue(position.CanMove(20, 1));
            Assert.IsTrue(position.CanMove(21, 1));
            Assert.IsFalse(position.CanMove(20, 4));
            Assert.IsTrue(position.CanMove(20, 3));
            Assert.IsTrue(position.CanMove(20, 6));
            Assert.IsFalse(position.CanMove(24, 1));

            position.Color = Color.Red;

            Assert.IsTrue(position.CanMove(1, 1));
            Assert.IsTrue(position.CanMove(2, 1));
            Assert.IsFalse(position.CanMove(1, 4));
            Assert.IsTrue(position.CanMove(1, 3));

            Assert.IsTrue(position.CanMove(20, 1));
            Assert.IsTrue(position.CanMove(21, 1));
            Assert.IsFalse(position.CanMove(20, 4));
            Assert.IsTrue(position.CanMove(20, 3));
            Assert.IsTrue(position.CanMove(20, 6));
            Assert.IsFalse(position.CanMove(24, 1));
        }
    }
}
