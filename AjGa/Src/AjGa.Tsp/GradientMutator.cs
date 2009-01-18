namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GradientMutator : IGenomeMutator<int, int>
    {
        private static Random rnd = new Random();
        private Evaluator evaluator;

        public GradientMutator(Evaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public IGenome<int, int> Mutate(IGenome<int, int> genome)
        {
            int value = this.evaluator.Evaluate(genome);

            IGenome<int, int> newgenome = this.TryMutate(genome);
            int ntries = 0;
            int maxtries = genome.Genes.Count * genome.Genes.Count / 4;

            while (this.evaluator.Evaluate(newgenome) >= value && ntries < maxtries)
            {
                ntries++;
                newgenome = this.TryMutate(genome);
            }

            return newgenome;
        }

        private static void SwapTwo(int[] positions)
        {
            int p1 = rnd.Next(positions.Length);
            int p2 = rnd.Next(positions.Length);

            int aux = positions[p1];
            positions[p1] = positions[p2];
            positions[p2] = aux;
        }

        private IGenome<int, int> TryMutate(IGenome<int, int> genome)
        {
            int[] positions = new int[genome.Genes.Count];

            for (int k = 0; k < positions.Length; k++)
            {
                positions[k] = genome.Genes[k];
            }

            SwapTwo(positions);

            List<int> genes = new List<int>(positions);

            return new Genome(genes);
        }
    }
}
