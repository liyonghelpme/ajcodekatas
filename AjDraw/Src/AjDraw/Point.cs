using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjDraw
{
    public class Point
    {
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public Point Translate(Point p)
        {
            return new Point(this.X + p.X, this.Y + p.Y);
        }

        public Point Resize(double ratio)
        {
            return new Point(this.X * ratio, this.Y * ratio);
        }

        public Point HorizontalResize(double ratio)
        {
            return new Point(this.X * ratio, this.Y);
        }

        public Point VerticalResize(double ratio)
        {
            return new Point(this.X, this.Y * ratio);
        }

        public Point Rotate(int degrees)
        {
            if (degrees == 0)
                return this;

            if (degrees == 90)
                return new Point(-this.Y, this.X);

            if (degrees == 180)
                return new Point(-this.X, -this.Y);

            if (degrees == 270)
                return new Point(this.Y, -this.X);

            double newx = this.X * Math.Cos(2 * Math.PI / 360 * degrees)
                        - this.Y * Math.Cos(2 * Math.PI / 360 * degrees);

            double newy = this.X * Math.Sin(2 * Math.PI / 360 * degrees)
                        + this.Y * Math.Cos(2 * Math.PI / 360 * degrees);

            return new Point(newx, newy);
        }
    }
}

