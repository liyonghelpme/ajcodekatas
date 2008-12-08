using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjGa.Tsp
{
    public class Mutator : IGenomaMutator<int, int>
    {
        private static Random rnd = new Random();

        public IGenoma<int, int> Mutate(IGenoma<int, int> genoma)
        {
            int [] positions = new int[genoma.Genes.Count];

            for (int k = 0; k < positions.Length; k++)
                positions[k] = genoma.Genes[k];

            for (int l = 0; l < positions.Length * 3; l++)
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
