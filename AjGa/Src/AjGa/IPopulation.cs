using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public interface IPopulation<G,V>
    {
        List<IGenoma<G, V>> Genomas { get; }
    }
}
