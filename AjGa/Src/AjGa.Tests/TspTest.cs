using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjGa;
using AjGa.Tsp;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjGa.Tests
{
    /// <summary>
    /// Summary description for TpsTest
    /// </summary>
    [TestClass]
    public class TspTest
    {
        [TestMethod]
        public void ShouldCreateGenoma()
        {
            Genoma genoma = new Genoma(3);

            Assert.IsNotNull(genoma);
            Assert.IsNotNull(genoma.Genes);
            Assert.AreEqual(3, genoma.Genes.Count);
            Assert.AreEqual(0, genoma.Genes[0]);
            Assert.AreEqual(1, genoma.Genes[1]);
            Assert.AreEqual(2, genoma.Genes[2]);
        }

        [TestMethod]
        public void ShouldCreatePopulation()
        {
            Genoma genoma1 = new Genoma(3);
            Genoma genoma2 = new Genoma(3);

            Population population = new Population();

            Assert.IsNotNull(population);
            Assert.IsNotNull(population.Genomas);

            population.AddGenoma(genoma1);
            population.AddGenoma(genoma2);

            Assert.AreEqual(2, population.Genomas.Count);
        }

        [TestMethod]
        public void ShouldEvaluateGenomaWithTwoPositions()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(0, 1));

            Evaluator evaluator = new Evaluator(positions);

            Genoma genoma = new Genoma(positions.Count);

            Assert.IsNotNull(genoma);
            Assert.IsNotNull(genoma.Genes);
            Assert.AreEqual(-1, evaluator.Evaluate(genoma));
        }

        [TestMethod]
        public void ShouldEvaluateGenomaWithThreePositions()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Evaluator evaluator = new Evaluator(positions);

            Genoma genoma = new Genoma(positions.Count);

            Assert.IsNotNull(genoma);
            Assert.IsNotNull(genoma.Genes);
            Assert.AreEqual(-4, evaluator.Evaluate(genoma));
        }

        [TestMethod]
        public void ShouldCreatePopulationWithOneHundredGenomas()
        {
            Population population = new Population(100, 3);

            Assert.IsNotNull(population);
            Assert.AreEqual(100, population.Genomas.Count);
        }

        [TestMethod]
        public void ShouldRunGenerationWithoutOperators()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Population population = new Population(100, positions.Count);
            Evolution evolution = new Evolution(new Evaluator(positions));

            Population newpopulation = (Population) evolution.RunGeneration(population);

            Assert.IsNotNull(newpopulation);
            Assert.AreEqual(100, newpopulation.Genomas.Count);
        }

        [TestMethod]
        public void ShouldRunGenerationWithMutators()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Population population = new Population(100, positions.Count);
            List<IGenomaFactory<int, int>> operators = new List<IGenomaFactory<int, int>>();

            for (int k = 0; k < 50; k++)
            {
                operators.Add(new Mutator());
            }

            Evolution evolution = new Evolution(new Evaluator(positions), operators);

            Population newpopulation = (Population)evolution.RunGeneration(population);

            Assert.IsNotNull(newpopulation);
            Assert.AreEqual(100, newpopulation.Genomas.Count);
        }
    }
}

