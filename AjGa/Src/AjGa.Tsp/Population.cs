using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjGa;

namespace AjGa.Tsp
{
    public class Population : BasePopulation<int, int>
    {
        public Population()
        {
        }

        public Population(List<IGenoma<int,int>> genomas)
            : base(genomas)
        {
        }

        public Population(IPopulation<int, int> population)
            : base(population)
        {
        }

        public Population(int size, int genomalength)
        {
            Creator creator = new Creator(genomalength);

            for (int k = 0; k < size; k++)
                AddGenoma(creator.Create());
        }
    }
}

