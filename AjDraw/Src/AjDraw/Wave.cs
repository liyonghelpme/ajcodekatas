using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjDraw
{
    public class Wave : IDrawObject<Wave>, IDrawObject
    {
        private Composite lines = new Composite();

        public Wave(int from, int to, double height)
        {
            Point last = null;

            for (int degrees = from; degrees <= to; degrees += 1)
            {
                double x = Math.PI / 180 * degrees * height;
                double y = Math.Sin(Math.PI / 180 * degrees) * height;

                if (last == null)
                    last = new Point(x, y);
                else
                {
                    Point newpoint = new Point(x, y);
                    lines.Add(new Line(last, newpoint));
                    last = newpoint;
                }
            }
        }

        public Wave Translate(Point p)
        {
            this.lines.Translate(p);
            return this;
        }

        public Wave Resize(double ratio)
        {
            this.lines.Resize(ratio);
            return this;
        }

        public Wave HorizontalResize(double ratio)
        {
            this.lines.HorizontalResize(ratio);
            return this;
        }

        public Wave VerticalResize(double ratio)
        {
            this.lines.VerticalResize(ratio);
            return this;
        }

        public Wave Duplicate()
        {
            throw new NotImplementedException();
        }

        public Wave Rotate(int degrees)
        {
            throw new NotImplementedException();
        }

        public void Draw(IDrawImage image)
        {
            this.lines.Draw(image);
        }

        IDrawObject IDrawObject.Translate(Point p)
        {
            throw new NotImplementedException();
        }

        IDrawObject IDrawObject.Resize(double ratio)
        {
            throw new NotImplementedException();
        }

        IDrawObject IDrawObject.HorizontalResize(double ratio)
        {
            throw new NotImplementedException();
        }

        IDrawObject IDrawObject.VerticalResize(double ratio)
        {
            throw new NotImplementedException();
        }

        IDrawObject IDrawObject.Duplicate()
        {
            throw new NotImplementedException();
        }

        IDrawObject IDrawObject.Rotate(int degrees)
        {
            throw new NotImplementedException();
        }

        void IDrawObject.Draw(IDrawImage image)
        {
            throw new NotImplementedException();
        }
    }
}
