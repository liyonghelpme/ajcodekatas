using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjDraw.Tests
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void ResizeByTwo()
        {
            Line line = new Line(new Point(-1, 1), new Point(2, 3));
            line = line.Resize(2);

            Assert.AreEqual(-2, line.From.X);
            Assert.AreEqual(2, line.From.Y);
            Assert.AreEqual(4, line.To.X);
            Assert.AreEqual(6, line.To.Y);
        }

        [TestMethod]
        public void GenericResizeByTwo()
        {
            Line line = new Line(new Point(-1, 1), new Point(2, 3));
            line = (Line) ((IDrawObject) line).Resize(2);

            Assert.AreEqual(-2, line.From.X);
            Assert.AreEqual(2, line.From.Y);
            Assert.AreEqual(4, line.To.X);
            Assert.AreEqual(6, line.To.Y);
        }
    }
}
