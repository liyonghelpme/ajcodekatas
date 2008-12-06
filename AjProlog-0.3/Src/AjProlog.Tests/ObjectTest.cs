using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjProlog.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjProlog.Tests
{
    /// <summary>
    /// Summary description for ObjectTest
    /// </summary>
    [TestClass]
    public class ObjectTest
    {
        [TestMethod]
        public void IntegerObjectsShouldBeEquals()
        {
            IntegerObject io1 = new IntegerObject(1);
            IntegerObject io2 = new IntegerObject(1);

            Assert.AreEqual(io1, io2);
            Assert.AreEqual(io1.GetHashCode(), io2.GetHashCode());
        }

        [TestMethod]
        public void IntegerObjectsShouldNotBeEquals()
        {
            IntegerObject io1 = new IntegerObject(1);
            IntegerObject io2 = new IntegerObject(2);

            Assert.AreNotEqual(io1, io2);
            Assert.AreNotEqual(io1.GetHashCode(), io2.GetHashCode());
        }

        [TestMethod]
        public void StringObjectsShouldBeEquals()
        {
            StringObject so1 = new StringObject("a");
            StringObject so2 = new StringObject("a");

            Assert.AreEqual(so1, so2);
            Assert.AreEqual(so1.GetHashCode(), so2.GetHashCode());
        }

        [TestMethod]
        public void StringObjectsShouldNotBeEquals()
        {
            StringObject so1 = new StringObject("a");
            StringObject so2 = new StringObject("2");

            Assert.AreNotEqual(so1, so2);
            Assert.AreNotEqual(so1.GetHashCode(), so2.GetHashCode());
        }

        [TestMethod]
        public void ShouldCreatePrologObjectsFromOne()
        {
            PrologObject po1;
            PrologObject po2;

            po1 = Utilities.ToPrologObject(1);
            po2 = Utilities.ToPrologObject(1);

            Assert.AreEqual(po1, po2);
            Assert.AreEqual(po1.GetHashCode(), po2.GetHashCode());

            Assert.AreEqual("1", po1.ToString());
            Assert.AreEqual("1", po2.ToString());
        }

        [TestMethod]
        public void ShouldCreatePrologObjectsFromIntegers()
        {
            PrologObject po1;
            PrologObject po2;

            po1 = Utilities.ToPrologObject(1);
            po2 = Utilities.ToPrologObject(2);

            Assert.AreNotEqual(po1, po2);
            Assert.AreNotEqual(po1.GetHashCode(), po2.GetHashCode());

            Assert.AreEqual("1", po1.ToString());
            Assert.AreEqual("2", po2.ToString());
        }


        [TestMethod]
        public void ShouldCreatePrologObjectsFromStrings()
        {
            PrologObject po1;
            PrologObject po2;

            po1 = Utilities.ToPrologObject("a");
            po2 = Utilities.ToPrologObject("b");

            Assert.AreNotEqual(po1, po2);
            Assert.AreNotEqual(po1.GetHashCode(), po2.GetHashCode());

            Assert.AreEqual("a", po1.ToString());
            Assert.AreEqual("b", po2.ToString());
        }
    }
}
