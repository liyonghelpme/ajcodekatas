using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AjDraw
{
    public class DrawImage : IDrawImage
    {
        private Image image;
        private int xadjust;
        private int yadjust;
        private Graphics graphics;
        private Pen pen;

        public DrawImage(int width, int height)
        {
            this.image = new Bitmap(width, height);
            this.xadjust = width / 2;
            this.yadjust = height / 2;
            this.graphics = Graphics.FromImage(image);
            this.graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            this.pen = Pens.Black;
        }

        public Image Image { get { return this.image; } }

        public void DrawLine(Point from, Point to)
        {
            this.graphics.DrawLine(this.pen, (float)from.X + this.xadjust, (float)-from.Y + this.yadjust, (float)to.X + this.xadjust, (float)-to.Y + this.yadjust);
        }
    }
}

