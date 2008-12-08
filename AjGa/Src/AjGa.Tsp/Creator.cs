using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjGa;

namespace AjGa.Tsp
{
    public class Creator : IGenomaCreator<int, int>
    {
        private int size;
        private Mutator mutator = new Mutator();

        public Creator(int size)
        {
            this.size = size;
        }

        public IGenoma<int, int> Create()  
        {
            return mutator.Mutate(new Genoma(this.size));
        }
    }
}

