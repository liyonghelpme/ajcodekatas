using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjGa;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjGa.Tests
{
    /// <summary>
    /// Summary description for BaseGenomaTest
    /// </summary>
    [TestClass]
    public class BaseGenomaTest
    {
        [TestMethod]
        public void ShouldCreateWithoutParameters()
        {
            BaseGenoma<int, int> genoma = new BaseGenoma<int, int>();

            Assert.IsNotNull(genoma);
            Assert.IsNotNull(genoma.Genes);
            Assert.AreEqual(0, genoma.Genes.Count);
            Assert.AreEqual(0, genoma.Value);
        }

        [TestMethod]
        public void ShouldCreateWithGenes()
        {
            List<int> genes = new List<int>();
            genes.Add(1);
            genes.Add(2);
            genes.Add(3);

            BaseGenoma<int, int> genoma = new BaseGenoma<int, int>(genes);

            Assert.IsNotNull(genoma);
            Assert.IsNotNull(genoma.Genes);
            Assert.AreEqual(3, genoma.Genes.Count);
            Assert.AreEqual(1, genoma.Genes[0]);
            Assert.AreEqual(2, genoma.Genes[1]);
            Assert.AreEqual(3, genoma.Genes[2]);
            Assert.AreEqual(0, genoma.Value);
        }
    }
}

