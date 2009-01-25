namespace AjGammon.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGammon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StaticEvaluatorTests
    {
        [TestMethod]
        public void ShouldEvaluateSimpleBoard()
        {
            BoardPosition board = new BoardPosition(new int[] { 1 }, null);
            StaticEvaluator evaluator = new StaticEvaluator();

            Assert.AreEqual(24, evaluator.Evaluate(board));
        }

        [TestMethod]
        public void ShouldEvaluateSimpleBoardWithTwoPieces()
        {
            BoardPosition board = new BoardPosition(new int[] { 1, 1 }, null);
            StaticEvaluator evaluator = new StaticEvaluator();

            Assert.AreEqual(48, evaluator.Evaluate(board));
        }

        [TestMethod]
        public void ShouldEvaluateSimpleSymmetricBoard()
        {
            BoardPosition board = new BoardPosition(new int[] { 1, 2 }, new int[] { 1, 2 });
            StaticEvaluator evaluator = new StaticEvaluator();

            Assert.AreEqual(0, evaluator.Evaluate(board));
        }

        [TestMethod]
        public void ShouldEvaluateInitialBoard()
        {
            BoardPosition board = new BoardPosition();
            StaticEvaluator evaluator = new StaticEvaluator();

            Assert.AreEqual(0, evaluator.Evaluate(board));
        }
    }
}
