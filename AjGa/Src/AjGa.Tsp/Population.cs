namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Population : BasePopulation<int, int>
    {
        public Population()
        {
        }

        public Population(List<IGenome<int, int>> genomes)
            : base(genomes)
        {
        }

        public Population(IPopulation<int, int> population)
            : base(population)
        {
        }

        public Population(int size, int genomelength)
        {
            Creator creator = new Creator(genomelength);

            for (int k = 0; k < size; k++)
            {
                AddGenome(creator.Create());
            }
        }
    }
}

