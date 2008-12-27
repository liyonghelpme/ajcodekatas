using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa.Tsp
{
    public class GradientMutator : IGenomaMutator<int, int>
    {
        private static Random rnd = new Random();
        private Evaluator evaluator;

        public GradientMutator(Evaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public IGenoma<int, int> Mutate(IGenoma<int, int> genoma)
        {
            int value = evaluator.Evaluate(genoma);

            IGenoma<int, int> newgenoma = TryMutate(genoma);
            int ntries = 0;
            int maxtries = genoma.Genes.Count * genoma.Genes.Count / 4;

            while (evaluator.Evaluate(newgenoma) >= value && ntries < maxtries)
            {
                ntries++;
                newgenoma = TryMutate(genoma);
            }

            return newgenoma;
        }

        private IGenoma<int, int> TryMutate(IGenoma<int, int> genoma)
        {
            int [] positions = new int[genoma.Genes.Count];

            for (int k = 0; k < positions.Length; k++)
                positions[k] = genoma.Genes[k];

            SwapTwo(positions);

            List<int> genes = new List<int>(positions);

            return new Genoma(genes);
        }

        private static void SwapTwo(int [] positions) 
        {
            int p1 = rnd.Next(positions.Length);
            int p2 = rnd.Next(positions.Length);

            int aux = positions[p1];
            positions[p1] = positions[p2];
            positions[p2] = aux;
        }
    }
}
