using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjDraw.Tests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void DiagonalTranslate()
        {
            Point p = new Point(1, 2);
            Point trans = new Point(3, 4);
            p = p.Translate(trans);

            Assert.AreEqual(4, p.X);
            Assert.AreEqual(6, p.Y);
        }

        [TestMethod]
        public void ResizeByTwo()
        {
            Point p = new Point(1, 2);
            p = p.Resize(2);

            Assert.AreEqual(2, p.X);
            Assert.AreEqual(4, p.Y);
        }

        [TestMethod]
        public void HorizontalResizeByThree()
        {
            Point p = new Point(1, 2);
            p = p.HorizontalResize(3);

            Assert.AreEqual(3, p.X);
            Assert.AreEqual(2, p.Y);
        }

        [TestMethod]
        public void HorizontalResizeByHalf()
        {
            Point p = new Point(1, 2);
            p = p.VerticalResize(0.5);

            Assert.AreEqual(1, p.X);
            Assert.AreEqual(1, p.Y);
        }

        [TestMethod]
        public void Rotate0Degrees()
        {
            Point p = new Point(1, 2);
            p = p.Rotate(0);

            Assert.AreEqual(1, p.X);
            Assert.AreEqual(2, p.Y);
        }

        [TestMethod]
        public void Rotate90Degrees()
        {
            Point p = new Point(1, 2);
            p = p.Rotate(90);

            Assert.AreEqual(-2, p.X);
            Assert.AreEqual(1, p.Y);
        }

        [TestMethod]
        public void Rotate180Degrees()
        {
            Point p = new Point(1, 2);
            p = p.Rotate(180);

            Assert.AreEqual(-1, p.X);
            Assert.AreEqual(-2, p.Y);
        }

        [TestMethod]
        public void Rotate270Degrees()
        {
            Point p = new Point(1, 2);
            p = p.Rotate(270);

            Assert.AreEqual(2, p.X);
            Assert.AreEqual(-1, p.Y);
        }

        [TestMethod]
        public void Rotate45Degrees()
        {
            Point p = new Point(1, 0);
            p = p.Rotate(45);

            Assert.AreEqual(p.X, p.Y, 0.0000001);
            Assert.IsTrue(p.X > 0);
        }

        [TestMethod]
        public void Rotate135Degrees()
        {
            Point p = new Point(1, 0);
            p = p.Rotate(135);

            Assert.AreEqual(-p.X, p.Y, 0.0000001);
            Assert.IsTrue(p.Y > 0);
        }
    }
}

