using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageExampleLibrary
{
    public class Polygon
    {
        private Point[] points;
        private Color color;

        public Point[] Points { get { return this.points; } }
        public Color Color { get { return this.color; } }

        public Polygon(Point[] points, Color color)
        {
            this.points = points;
            this.color = color;
        }

        public void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(this.color);
            graphics.FillPolygon(brush, this.points);
        }
    }
}
