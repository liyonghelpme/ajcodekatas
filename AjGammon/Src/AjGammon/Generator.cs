namespace AjGammon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Generator
    {
        private List<BoardPosition> boards = new List<BoardPosition>();

        public Generator(BoardPosition board, int firstDice, int secondDice)
        {
            for (int x = 0; x < BoardPosition.Size - 1; x++)
            {
                if (board.CanMove(x, x + firstDice))
                {
                    BoardPosition newBoard = board.Clone();
                    newBoard.Move(x, x + firstDice);

                    for (int y = 0; y < BoardPosition.Size - 1; y++)
                    {
                        if (newBoard.CanMove(y, y + secondDice))
                        {
                            BoardPosition generatedBoard = newBoard.Clone();
                            generatedBoard.Move(y, y + secondDice);

                            this.boards.Add(generatedBoard);
                        }
                    }
                }

                if (board.CanMove(x, x + secondDice))
                {
                    BoardPosition newBoard = board.Clone();
                    newBoard.Move(x, x + secondDice);

                    for (int y = 0; y < BoardPosition.Size - 1; y++)
                    {
                        if (newBoard.CanMove(y, y + firstDice))
                        {
                            BoardPosition generatedBoard = newBoard.Clone();
                            generatedBoard.Move(y, y + firstDice);

                            this.boards.Add(generatedBoard);
                        }
                    }
                }
            }

            if (this.boards.Count == 0)
            {
                this.boards.Add(board);
            }
        }

        public ICollection<BoardPosition> Positions
        {
            get
            {
                return this.boards;
            }
        }
    }
}
