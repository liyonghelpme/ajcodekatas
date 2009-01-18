namespace AjGa.Tsp.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public class TravelImage
    {
        private Image image;
        private short width;
        private short height;

        public TravelImage(short width, short height)
        {
            this.width = width;
            this.height = height;
        }

        public Image Image
        {
            get
            {
                return this.image; 
            }
        }

        public void DrawTravel(int gwidth, int gheight, Genome g, List<Position> positions)
        {
            int gsize = Math.Min(gwidth, gheight);
            int size = Math.Min(this.width, this.height);

            int cellsize = gsize / size;

            this.image = new Bitmap(cellsize * this.width, cellsize * this.height);
            Graphics graphics = Graphics.FromImage(this.image);
            graphics.FillRectangle(Brushes.LightGoldenrodYellow, 0, 0, gwidth, gheight);

            int top = cellsize / 2;
            int left = cellsize / 2;

            Position p1 = null;

            foreach (int pos in g.Genes)
            {
                if (p1 == null)
                {
                    p1 = positions[pos];
                }
                else
                {
                    Position p2 = positions[pos];
                    graphics.DrawLine(Pens.Black, left + p1.X * cellsize, top + p1.Y * cellsize, left + p2.X * cellsize, top + p2.Y * cellsize);
                    p1 = p2;
                }
            }
        }
    }
}
