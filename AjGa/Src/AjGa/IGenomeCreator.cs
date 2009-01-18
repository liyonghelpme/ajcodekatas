namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IGenomeCreator<G, V> : IGenomeFactory<G, V>
    {
        IGenome<G, V> Create();
    }
}
