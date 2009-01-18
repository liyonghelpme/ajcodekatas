namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjGa;

    public class Evaluator : IEvaluator<int, int>
    {
        private List<Position> positions;

        public Evaluator(List<Position> positions)
        {
            this.positions = positions;
        }

        public int Evaluate(IGenome<int, int> genome)
        {
            Position position = null;
            int value = 0;

            foreach (int p in genome.Genes)
            {
                if (position == null)
                {
                    position = this.positions[p];
                }
                else
                {
                    value += (this.positions[p].X - position.X) * (this.positions[p].X - position.X) + (this.positions[p].Y - position.Y) * (this.positions[p].Y - position.Y);
                    position = this.positions[p];
                }
            }

            genome.Value = value;

            return value;
        }
    }
}

