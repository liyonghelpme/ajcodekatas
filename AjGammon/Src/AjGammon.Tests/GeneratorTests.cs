namespace AjGammon.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGammon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void ShouldGenerateFromSimpleBoard()
        {
            BoardPosition board = new BoardPosition(new int[] { 1 }, null);

            Generator generator = new Generator(board, 3, 4);

            Assert.AreEqual(2, generator.Positions.Count);
        }
    }
}
