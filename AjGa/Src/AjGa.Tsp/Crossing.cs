using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjGa;

namespace AjGa.Tsp
{
    public class Crossing : IGenomaCrossover<int, int>
    {
        public IGenoma<int, int> Crossover(IGenoma<int, int> genoma1, IGenoma<int, int> genoma2)
        {
            return genoma1;
        }
    }
}
