namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IGenomeMutator<G, V> : IGenomeFactory<G, V>
    {
        IGenome<G, V> Mutate(IGenome<G, V> genome);
    }
}
