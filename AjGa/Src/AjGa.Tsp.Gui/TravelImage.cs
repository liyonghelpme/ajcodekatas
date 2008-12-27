using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AjGa.Tsp.Gui
{
    class TravelImage
    {
        private Bitmap image;
        private Graphics graphics;
        private Brush brush;
        private short width;
        private short height;

        public TravelImage()
            : this(6, 6)
        {
        }

        public TravelImage(short width, short height)
        {
            this.width = width;
            this.height = height;
            image = new Bitmap(width * 40, height * 40);
            graphics = Graphics.FromImage(image);
            brush = Brushes.Beige;
        }

        public Image Image
        {
            get { return image; }
        }

        public void DrawTravel(Genoma g, List<Position> positions)
        {
            graphics.FillRectangle(Brushes.LightGoldenrodYellow, 0, 0, image.Width, image.Height);

            Position p1 = null;

            foreach (int pos in g.Genes)
            {
                if (p1 == null)
                    p1 = positions[pos];
                else
                {
                    Position p2 = positions[pos];
                    graphics.DrawLine(Pens.Black, 10 + p1.X * 20, 10 + p1.Y * 20, 10 + p2.X * 20, 10 + p2.Y * 20);
                    p1 = p2;
                }
            }
        }
    }
}
