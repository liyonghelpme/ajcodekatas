namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Evolution : BaseEvolution<int, int>
    {
        public Evolution(Evaluator evaluator)
            : base(evaluator, new List<IGenomeFactory<int, int>>())
        {
        }

        public Evolution(Evaluator evaluator, List<IGenomeFactory<int, int>> operators)
            : base(evaluator, operators)
        {
        }

        protected override IPopulation<int, int> CreateNewPopulation()
        {
            return new Population();
        }
    }
}

