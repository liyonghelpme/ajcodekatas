namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IGenoma<G, V>
    {
        V Value { get; set; }
        List<G> Genes { get; }
    }
}
