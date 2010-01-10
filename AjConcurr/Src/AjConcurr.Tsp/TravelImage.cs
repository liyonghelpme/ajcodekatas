using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AjConcurr.Tsp
{
    class TravelImage
    {
        private Bitmap image;
        private Graphics graphics;
        private Brush brush;
        private short width;
        private short height;

        public TravelImage()
            : this(6,6)
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

        public void DrawTravel(Genoma g)
        {
            graphics.FillRectangle(Brushes.LightGoldenrodYellow, 0, 0, image.Width, image.Height);

            Point p1 = null;

            foreach (Point pt in g.travel)
                if (p1 == null)
                    p1 = pt;
                else
                {
                    graphics.DrawLine(Pens.Black, 10 + p1.x * 20, 10 + p1.y * 20, 10 + pt.x * 20, 10 + pt.y * 20);
                    p1 = pt;
                }
        }
    }
}
