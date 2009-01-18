namespace AjGa.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseGenomeTest
    {
        [TestMethod]
        public void ShouldCreateWithoutParameters()
        {
            BaseGenome<int, int> genome = new BaseGenome<int, int>();

            Assert.IsNotNull(genome);
            Assert.IsNotNull(genome.Genes);
            Assert.AreEqual(0, genome.Genes.Count);
            Assert.AreEqual(0, genome.Value);
        }

        [TestMethod]
        public void ShouldCreateWithGenes()
        {
            List<int> genes = new List<int>();
            genes.Add(1);
            genes.Add(2);
            genes.Add(3);

            BaseGenome<int, int> genome = new BaseGenome<int, int>(genes);

            Assert.IsNotNull(genome);
            Assert.IsNotNull(genome.Genes);
            Assert.AreEqual(3, genome.Genes.Count);
            Assert.AreEqual(1, genome.Genes[0]);
            Assert.AreEqual(2, genome.Genes[1]);
            Assert.AreEqual(3, genome.Genes[2]);
            Assert.AreEqual(0, genome.Value);
        }
    }
}

