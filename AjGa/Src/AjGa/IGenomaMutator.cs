using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public interface IGenomaMutator<G, V> : IGenomaFactory<G, V>
    {
        IGenoma<G, V> Mutate(IGenoma<G, V> genoma);
    }
}
