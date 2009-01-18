namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Crossing : IGenomeCrossover<int, int>
    {
        public IGenome<int, int> Crossover(IGenome<int, int> genome1, IGenome<int, int> genome2)
        {
            return genome1;
        }
    }
}
