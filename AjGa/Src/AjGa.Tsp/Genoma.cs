using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjGa;

namespace AjGa.Tsp
{
    public class Genoma : BaseGenoma<int, int>
    {        
        public Genoma(int size)
        {
            for (int k = 0; k < size; k++)
                AddGene(k);
        }

        public Genoma(List<int> genes)
            : base(genes)
        {
        }
    }
}

