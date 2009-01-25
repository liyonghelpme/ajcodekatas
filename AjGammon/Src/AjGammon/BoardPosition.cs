namespace AjGammon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BoardPosition
    {
        public const int Size = 26;

        private static int[] initialPositions = new int[] { 0, 0, 0, 1, 1, 1 };

        private short[] cells;

        public BoardPosition()
            : this(initialPositions, initialPositions)
        {
        }

        public BoardPosition(int[] whitePositions, int[] redPositions)
        {
            this.cells = new short[Size];

            if (whitePositions != null)
            {
                foreach (int position in whitePositions)
                {
                    this.PutColorAt(Color.White, position);
                }
            }

            if (redPositions != null)
            {
                foreach (int position in redPositions)
                {
                    this.PutColorAt(Color.Red, position);
                }
            }
        }

        private BoardPosition(BoardPosition board)
        {
            this.cells = (short[])board.cells.Clone();
            this.Color = board.Color;
        }

        public Color Color { get; set; }

        public BoardPosition Clone()
        {
            return new BoardPosition(this);
        }

        public short GetColors(int position)
        {
            return this.cells[position];
        }

        public bool CanMove(int position, int dice)
        {
            int from;
            int to;

            if (this.Color == Color.White)
            {
                from = position;
                to = position + dice;

                if (to >= Size)
                {
                    to = Size - 1;
                }   
            }
            else
            {
                from = Size - position - 1;
                to = Size - position - dice - 1;

                if (to < 0)
                {
                    to = 0;
                }
            }

            if (this.Color == Color.White)
            {
                if (this.cells[from] <= 0) 
                {
                    return false;
                }

                if (this.cells[to] < -1 && to != Size - 1) 
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (this.cells[from] >= 0)
                {
                    return false;
                }

                if (this.cells[to] > 1 && to != Size - 1)
                {
                    return false;
                }

                return true;
            }
        }

        public void NextColor()
        {
            if (this.Color == Color.White)
            {
                this.Color = Color.Red;
            }
            else
            {
                this.Color = Color.White;
            }
        }

        public void Move(int from, int to) 
        {
            if (this.Color == Color.White)
            {
                this.cells[from]--;

                if (to >= Size)
                {
                    to = Size - 1;
                }

                if (this.cells[to] == -1)
                {
                    this.cells[to] = 0;
                    this.cells[Size - 1]--;
                }

                this.cells[to]++;

                return;
            }

            from = Size - from - 1;
            to = Size - to - 1;

            this.cells[from]++;

            if (to < 0)
            {
                to = 0;
            }

            if (this.cells[to] == 1)
            {
                this.cells[to] = 0;
                this.cells[0]++;
            }

            this.cells[to]--;
        }

        private void PutColorAt(Color color, int position) 
        {
            if (color == Color.White && this.cells[position] >= 0)
            {
                this.cells[position]++;
                return;
            }

            if (this.cells[Size - position - 1] <= 0)
            {
                this.cells[Size - position - 1]--;
                return;
            }

            throw new InvalidOperationException("Invalid move");
        }
    }
}
