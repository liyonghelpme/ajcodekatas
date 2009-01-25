namespace AjGammon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StaticEvaluator
    {
        public StaticEvaluator()
        {
        }

        public int Evaluate(BoardPosition board)
        {
            int value = 0;

            for (int x = 0; x < BoardPosition.Size; x++)
            {
                int colors = board.GetColors(x);

                if (colors > 0)
                {
                    value += colors * (BoardPosition.Size - x - 1);
                }
                else if (colors < 0)
                {
                    value += colors * x;
                }
            }

            return value;
        }
    }
}
