using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageExampleLibrary
{
    public class PolygonalImage
    {
        private Polygon[] polygons;

        public Polygon[] Polygons { get { return this.polygons; } }

        public long Distance { get; set; } 

        public PolygonalImage(Polygon[] polygons)
        {
            this.polygons = polygons;
        }

        public void DrawGraphics(Graphics graphics)
        {
            foreach (Polygon polygon in this.polygons)
                polygon.Draw(graphics);
        }

        public void DrawBitmap(Bitmap bitmap)
        {
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);

            this.DrawGraphics(graphics);
        }
    }
}
