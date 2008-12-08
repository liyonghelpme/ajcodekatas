using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public interface IEvolution<G, V>
    {
        IPopulation<G, V> RunGeneration(IPopulation<G, V> population);
    }
}


