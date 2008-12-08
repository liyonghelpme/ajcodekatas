using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public interface IGenomaCrossover<G, V> : IGenomaFactory<G, V>
    {
        IGenoma<G, V> Crossover(IGenoma<G, V> genoma1, IGenoma<G, V> genoma2);
    }
}

