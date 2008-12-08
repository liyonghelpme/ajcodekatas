using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public abstract class BaseEvolution<G, V> : IEvolution<G, V>
    {
        private static Comparer<G, V> comparer = new Comparer<G, V>();
        private IEvaluator<G, V> evaluator;
        private List<IGenomaFactory<G, V>> operators;
        private Random rnd = new Random();

        public BaseEvolution(IEvaluator<G, V> evaluator, List<IGenomaFactory<G,V>> operators)
        {
            this.evaluator = evaluator;
            this.operators = operators;
        }

        protected abstract IPopulation<G, V> CreateNewPopulation();

        public IPopulation<G, V> RunGeneration(IPopulation<G, V> population)
        {
            foreach (IGenoma<G, V> genoma in population.Genomas)
                genoma.Value = evaluator.Evaluate(genoma);

            population.Genomas.Sort(comparer);

            IPopulation<G, V> newpopulation = this.CreateNewPopulation();

            for (int k = 0; k < population.Genomas.Count - operators.Count; k++)
                newpopulation.Genomas.Add(population.Genomas[k]);

            foreach (IGenomaFactory<G, V> gf in operators)
                newpopulation.Genomas.Add(ApplyOperator(gf, population));

            return newpopulation;
        }

        private IGenoma<G, V> ApplyOperator(IGenomaFactory<G, V> gf, IPopulation<G, V> population)
        {
            if (gf is IGenomaCreator<G,V>)
                return ((IGenomaCreator<G,V>) gf).Create();

            if (gf is IGenomaMutator<G,V>)
                return ((IGenomaMutator<G,V>) gf).Mutate(ChooseGenoma(population));

            if (gf is IGenomaCrossover<G,V>)
                return ((IGenomaCrossover<G,V>) gf).Crossover(ChooseGenoma(population), ChooseGenoma(population));

            throw new ArgumentException();
        }

        private IGenoma<G, V> ChooseGenoma(IPopulation<G, V> population)
        {
            return population.Genomas[rnd.Next(population.Genomas.Count)];
        }
    }

    class Comparer<G, V> : IComparer<IGenoma<G, V>>
    {
        public int Compare(IGenoma<G, V> x, IGenoma<G, V> y)
        {
            return ((IComparable)x.Value).CompareTo(y.Value);
        }
    }
}

