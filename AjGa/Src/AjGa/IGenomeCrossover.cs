namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IGenomeCrossover<G, V> : IGenomeFactory<G, V>
    {
        IGenome<G, V> Crossover(IGenome<G, V> genome1, IGenome<G, V> genome2);
    }
}

