namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BasePopulation<G, V> : IPopulation<G, V>
    {
        private List<IGenome<G, V>> genomes;

        public BasePopulation()
        {
            this.genomes = new List<IGenome<G, V>>();
        }

        public BasePopulation(List<IGenome<G, V>> genomes)
        {
            this.genomes = genomes;
        }

        public BasePopulation(IPopulation<G, V> population)
        {
            this.genomes = population.Genomes;
        }

        public List<IGenome<G, V>> Genomes
        {
            get { return this.genomes; }
        }

        public void AddGenome(IGenome<G, V> genome)
        {
            this.genomes.Add(genome);
        }
    }
}

