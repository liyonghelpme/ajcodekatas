namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IEvaluator<G, V>
    {
        V Evaluate(IGenome<G, V> genome);
    }
}
