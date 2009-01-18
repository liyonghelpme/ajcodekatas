namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseGenome<G, V> : IGenome<G, V>
    {
        private List<G> genes;
        private V value;

        public BaseGenome() 
        {
            this.genes = new List<G>();
            this.value = default(V);
        }

        public BaseGenome(List<G> genes)
        {
            this.genes = genes;
            this.value = default(V);
        }

        public V Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public List<G> Genes
        {
            get { return this.genes; }
        }

        protected void AddGene(G gene)
        {
            this.genes.Add(gene);
        }
    }
}
