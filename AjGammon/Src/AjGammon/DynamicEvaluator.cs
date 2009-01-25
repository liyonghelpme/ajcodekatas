namespace AjGammon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DynamicEvaluator
    {
        private StaticEvaluator staticEvaluator = new StaticEvaluator();

        public DynamicEvaluator()
        {
        }

        public int Evaluate(BoardPosition board, int firstDice, int secondDice, int level)
        {
            Generator generator = new Generator(board, firstDice, secondDice);

            int bestValue;

            if (board.Color == Color.White)
            {
                bestValue = 100000;
            }
            else
            {
                bestValue = -100000;
            }

            foreach (BoardPosition newBoard in generator.Positions)
            {
                int value = 0;

                if (level <= 0)
                {
                    value = this.staticEvaluator.Evaluate(newBoard);
                }
                else
                {
                    newBoard.NextColor();

                    for (int x = 1; x <= 6; x++)
                    {
                        for (int y = 1; y <= 6; y++)
                        {
                            value += this.Evaluate(newBoard, x, y, level - 1);
                        }
                    }

                    value /= 36;
                }

                if (board.Color == Color.White && value < bestValue)
                {
                    bestValue = value;
                }
                else if (board.Color == Color.Red && value > bestValue)
                {
                    bestValue = value;
                }
            }

            return bestValue;
        }
    }
}
