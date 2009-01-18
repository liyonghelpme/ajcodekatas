namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Genome : BaseGenome<int, int>
    {        
        public Genome(int size)
        {
            for (int k = 0; k < size; k++)
            {
                AddGene(k);
            }
        }

        public Genome(List<int> genes)
            : base(genes)
        {
        }
    }
}

