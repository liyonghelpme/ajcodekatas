using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public class BasePopulation<G, V> : IPopulation<G,V>
    {
        private List<IGenoma<G,V>> genomas;

        public BasePopulation()
        {
            this.genomas = new List<IGenoma<G, V>>();
        }

        public BasePopulation(List<IGenoma<G, V>> genomas)
        {
            this.genomas = genomas;
        }

        public BasePopulation(IPopulation<G, V> population)
        {
            this.genomas = population.Genomas;
        }

        public List<IGenoma<G, V>> Genomas
        {
            get { return this.genomas; }
        }

        public void AddGenoma(IGenoma<G, V> genoma)
        {
            this.genomas.Add(genoma);
        }
    }
}

