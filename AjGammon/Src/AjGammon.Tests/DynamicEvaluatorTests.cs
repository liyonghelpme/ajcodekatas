namespace AjGammon.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGammon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DynamicEvaluatorTests
    {
        [TestMethod]
        public void ShouldEvaluateSimpleBoardOneWhite()
        {
            BoardPosition board = new BoardPosition(new int[] { 1 }, null);
            DynamicEvaluator evaluator = new DynamicEvaluator();

            Assert.IsTrue(evaluator.Evaluate(board, 1, 1, 0) > 0);
        }

        [TestMethod]
        public void ShouldEvaluateSimpleBoardOneRed()
        {
            BoardPosition board = new BoardPosition(null, new int[] { 1 });
            board.Color = Color.Red;

            DynamicEvaluator evaluator = new DynamicEvaluator();

            Assert.IsTrue(evaluator.Evaluate(board, 1, 1, 0) < 0);
        }

        [TestMethod]
        public void ShouldEvaluateSimpleBoardOneWhiteTwoLevels()
        {
            BoardPosition board = new BoardPosition(new int[] { 1 }, null);
            DynamicEvaluator evaluator = new DynamicEvaluator();

            Assert.IsTrue(evaluator.Evaluate(board, 1, 1, 1) > 0);
        }

        [TestMethod]
        public void ShouldEvaluateSimpleBoardOneRedTwoLevels()
        {
            BoardPosition board = new BoardPosition(null, new int[] { 1 });
            board.Color = Color.Red;

            DynamicEvaluator evaluator = new DynamicEvaluator();

            Assert.IsTrue(evaluator.Evaluate(board, 1, 1, 1) < 0);
        }
    }
}
