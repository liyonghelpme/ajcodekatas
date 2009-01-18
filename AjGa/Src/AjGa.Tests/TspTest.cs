namespace AjGa.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;
    using AjGa.Tsp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TspTest
    {
        [TestMethod]
        public void ShouldCreateGenome()
        {
            Genome genome = new Genome(3);

            Assert.IsNotNull(genome);
            Assert.IsNotNull(genome.Genes);
            Assert.AreEqual(3, genome.Genes.Count);
            Assert.AreEqual(0, genome.Genes[0]);
            Assert.AreEqual(1, genome.Genes[1]);
            Assert.AreEqual(2, genome.Genes[2]);
        }

        [TestMethod]
        public void ShouldCreatePopulation()
        {
            Genome genome1 = new Genome(3);
            Genome genome2 = new Genome(3);

            Population population = new Population();

            Assert.IsNotNull(population);
            Assert.IsNotNull(population.Genomes);

            population.AddGenome(genome1);
            population.AddGenome(genome2);

            Assert.AreEqual(2, population.Genomes.Count);
        }

        [TestMethod]
        public void ShouldEvaluateGenomeWithTwoPositions()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(0, 1));

            Evaluator evaluator = new Evaluator(positions);

            Genome genome = new Genome(positions.Count);

            Assert.IsNotNull(genome);
            Assert.IsNotNull(genome.Genes);
            Assert.AreEqual(1, evaluator.Evaluate(genome));
        }

        [TestMethod]
        public void ShouldEvaluateGenomeWithThreePositions()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Evaluator evaluator = new Evaluator(positions);

            Genome genome = new Genome(positions.Count);

            Assert.IsNotNull(genome);
            Assert.IsNotNull(genome.Genes);
            Assert.AreEqual(4, evaluator.Evaluate(genome));
        }

        [TestMethod]
        public void ShouldCreatePopulationWithOneHundredGenomes()
        {
            Population population = new Population(100, 3);

            Assert.IsNotNull(population);
            Assert.AreEqual(100, population.Genomes.Count);
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
            Assert.AreEqual(100, newpopulation.Genomes.Count);
        }

        [TestMethod]
        public void ShouldRunGenerationWithMutators()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Population population = new Population(100, positions.Count);
            List<IGenomeFactory<int, int>> operators = new List<IGenomeFactory<int, int>>();

            for (int k = 0; k < 50; k++)
            {
                operators.Add(new Mutator());
            }

            Evolution evolution = new Evolution(new Evaluator(positions), operators);

            Population newpopulation = (Population)evolution.RunGeneration(population);

            Assert.IsNotNull(newpopulation);
            Assert.AreEqual(100, newpopulation.Genomes.Count);
        }

        [TestMethod]
        public void ShouldRunGenerationWithGradientMutators()
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(0, 0));
            positions.Add(new Position(1, 1));
            positions.Add(new Position(2, 2));

            Population population = new Population(100, positions.Count);
            List<IGenomeFactory<int, int>> operators = new List<IGenomeFactory<int, int>>();

            Evaluator evaluator = new Evaluator(positions);

            for (int k = 0; k < 50; k++)
            {
                operators.Add(new GradientMutator(evaluator));
            }

            Evolution evolution = new Evolution(evaluator, operators);

            Population newpopulation = (Population)evolution.RunGeneration(population);

            Assert.IsNotNull(newpopulation);
            Assert.AreEqual(100, newpopulation.Genomes.Count);
        }
    }
}

