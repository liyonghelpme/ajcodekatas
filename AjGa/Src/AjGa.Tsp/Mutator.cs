namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Mutator : IGenomeMutator<int, int>
    {
        private static Random rnd = new Random();

        public IGenome<int, int> Mutate(IGenome<int, int> genome)
        {
            return Mutate(genome, rnd.Next(3) + 1);
        }

        public IGenome<int, int> Mutate(IGenome<int, int> genome, int nmutations)
        {
            int[] positions = new int[genome.Genes.Count];

            for (int k = 0; k < positions.Length; k++)
            {
                positions[k] = genome.Genes[k];
            }

            for (int l = 0; l < nmutations; l++)
            {
                SwapTwo(positions);
            }

            List<int> genes = new List<int>(positions);

            return new Genome(genes);
        }

        private static void SwapTwo(int[] positions) 
        {
            int p1 = rnd.Next(positions.Length);
            int p2 = rnd.Next(positions.Length);

            int aux = positions[p1];
            positions[p1] = positions[p2];
            positions[p2] = aux;
        }
    }
}
