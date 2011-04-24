using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberTheory.Tests
{
    [TestClass]
    public class ModulusTests
    {
        [TestMethod]
        public void GetElements()
        {
            Modulus modulus = new Modulus(17);

            int k=0;

            foreach (int x in modulus.Elements())
                Assert.AreEqual(k++, x);

            Assert.AreEqual(17, k);
        }

        [TestMethod]
        public void GetInverse()
        {
            Modulus modulus = new Modulus(7);

            Assert.AreEqual(0, modulus.Inverse(0));
            Assert.AreEqual(1, modulus.Inverse(1));
            Assert.AreEqual(4, modulus.Inverse(2));
            Assert.AreEqual(5, modulus.Inverse(3));
            Assert.AreEqual(2, modulus.Inverse(4));
            Assert.AreEqual(3, modulus.Inverse(5));
            Assert.AreEqual(6, modulus.Inverse(6));
        }

        [TestMethod]
        public void Add()
        {
            Modulus modulus = new Modulus(7);

            Assert.AreEqual(2, modulus.Add(1, 1));
            Assert.AreEqual(4, modulus.Add(2, 2));
            Assert.AreEqual(1, modulus.Add(2, 6));
        }

        [TestMethod]
        public void Multiply()
        {
            Modulus modulus = new Modulus(7);

            Assert.AreEqual(1, modulus.Multiply(1, 1));
            Assert.AreEqual(4, modulus.Multiply(2, 2));
            Assert.AreEqual(5, modulus.Multiply(2, 6));
        }

        [TestMethod]
        public void Power()
        {
            Modulus modulus = new Modulus(7);

            Assert.AreEqual(1, modulus.Power(2, 0));
            Assert.AreEqual(1, modulus.Power(1, 1));
            Assert.AreEqual(4, modulus.Power(2, 2));
            Assert.AreEqual(1, modulus.Power(2, 6));

            Assert.AreEqual(modulus.Power(5, 3), modulus.Power(3, -3));

            foreach (int x in modulus.Elements().Where(x => x>0))
                Assert.AreEqual(1, modulus.Power(x, 6));
        }

        [TestMethod]
        public void QuadraticResidues()
        {
            Modulus modulus = new Modulus(7);
            IList<int> residues = modulus.QuadraticResidues().ToList();

            Assert.AreEqual(3, residues.Count);

            Assert.AreEqual(1, residues[0]);
            Assert.AreEqual(2, residues[1]);
            Assert.AreEqual(4, residues[2]);
        }

        [TestMethod]
        public void Powers()
        {
            Modulus modulus = new Modulus(7);
            IList<int> powers = modulus.Powers(2).ToList();

            Assert.AreEqual(3, powers.Count);
            Assert.AreEqual(2, powers[0]);
            Assert.AreEqual(4, powers[1]);
            Assert.AreEqual(1, powers[2]);
        }
    }
}
