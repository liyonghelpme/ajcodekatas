using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjGa;

namespace AjGa.Tsp
{
    public class Evaluator : IEvaluator<int, int>
    {
        private List<Position> positions;

        public Evaluator(List<Position> positions)
        {
            this.positions = positions;
        }

        public int Evaluate(IGenoma<int,int> genoma)
        {
            Position position = null;
            int value = 0;

            foreach (int p in genoma.Genes)
            {
                if (position == null)
                    position = positions[p];
                else
                {
                    value += (positions[p].X - position.X) * (positions[p].X - position.X) + (positions[p].Y - position.Y) * (positions[p].Y - position.Y);
                    position = positions[p];        
                }
            }

            genoma.Value = value;

            return value;
        }
    }
}

