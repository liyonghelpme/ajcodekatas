namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseEvolution<G, V> : IEvolution<G, V>
    {
        private static Comparer<G, V> comparer = new Comparer<G, V>();
        private IEvaluator<G, V> evaluator;
        private List<IGenomeFactory<G, V>> operators;
        private Random rnd = new Random();

        public BaseEvolution(IEvaluator<G, V> evaluator, List<IGenomeFactory<G, V>> operators)
        {
            this.evaluator = evaluator;
            this.operators = operators;
        }

        public IPopulation<G, V> RunGeneration(IPopulation<G, V> population)
        {
            foreach (IGenome<G, V> genome in population.Genomes)
            {
                genome.Value = this.evaluator.Evaluate(genome);
            }

            population.Genomes.Sort(comparer);

            IPopulation<G, V> newpopulation = this.CreateNewPopulation();

            for (int k = 0; k < population.Genomes.Count - this.operators.Count; k++)
            {
                newpopulation.Genomes.Add(population.Genomes[k]);
            }

            foreach (IGenomeFactory<G, V> gf in this.operators)
            {
                newpopulation.Genomes.Add(this.ApplyOperator(gf, population));
            }

            return newpopulation;
        }

        protected abstract IPopulation<G, V> CreateNewPopulation();

        private IGenome<G, V> ApplyOperator(IGenomeFactory<G, V> gf, IPopulation<G, V> population)
        {
            if (gf is IGenomeCreator<G, V>)
            {
                return ((IGenomeCreator<G, V>)gf).Create();
            }

            if (gf is IGenomeMutator<G, V>)
            {
                return ((IGenomeMutator<G, V>)gf).Mutate(this.ChooseGenome(population));
            }

            if (gf is IGenomeCrossover<G, V>)
            {
                return ((IGenomeCrossover<G, V>)gf).Crossover(this.ChooseGenome(population), this.ChooseGenome(population));
            }

            throw new ArgumentException();
        }

        private IGenome<G, V> ChooseGenome(IPopulation<G, V> population)
        {
            return population.Genomes[this.rnd.Next(population.Genomes.Count)];
        }
    }

    internal class Comparer<G, V> : IComparer<IGenome<G, V>>
    {
        public int Compare(IGenome<G, V> x, IGenome<G, V> y)
        {
            return ((IComparable)x.Value).CompareTo(y.Value);
        }
    }
}

