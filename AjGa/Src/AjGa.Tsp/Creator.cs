namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Creator : IGenomeCreator<int, int>
    {
        private int size;
        private Mutator mutator = new Mutator();

        public Creator(int size)
        {
            this.size = size;
        }

        public IGenome<int, int> Create()  
        {
            return this.mutator.Mutate(new Genome(this.size), this.size*3);
        }
    }
}

