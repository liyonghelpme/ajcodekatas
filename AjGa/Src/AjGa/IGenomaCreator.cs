using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa
{
    public interface IGenomaCreator<G, V> : IGenomaFactory<G, V>
    {
        IGenoma<G, V> Create();
    }
}
