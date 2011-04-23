using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjDraw
{
    public class Line : IDrawObject<Line>, IDrawObject
    {
        public Line(Point from, Point to)
        {
            this.From = from;
            this.To = to;
        }

        public Point From { get; private set; }

        public Point To { get; private set; }

        public Line Translate(Point p)
        {
            this.From = this.From.Translate(p);
            this.To = this.To.Translate(p);
            return this;
        }

        public Line Resize(double ratio)
        {
            this.From = this.From.Resize(ratio);
            this.To = this.To.Resize(ratio);
            return this;
        }

        public Line HorizontalResize(double ratio)
        {
            this.From = this.From.HorizontalResize(ratio);
            this.To = this.To.HorizontalResize(ratio);
            return this;
        }

        public Line VerticalResize(double ratio)
        {
            this.From = this.From.VerticalResize(ratio);
            this.To = this.To.VerticalResize(ratio);
            return this;
        }

        public Line Rotate(int degrees)
        {
            return new Line(this.From.Rotate(degrees), this.To.Rotate(degrees));
        }

        public Line Duplicate()
        {
            return new Line(this.From, this.To);
        }

        public void Draw(IDrawImage image)
        {
            image.DrawLine(this.From, this.To);
        }

        IDrawObject IDrawObject.Translate(Point p)
        {
            return this.Translate(p);
        }

        IDrawObject IDrawObject.Resize(double ratio)
        {
            return this.Resize(ratio);
        }

        IDrawObject IDrawObject.HorizontalResize(double ratio)
        {
            return this.HorizontalResize(ratio);
        }

        IDrawObject IDrawObject.VerticalResize(double ratio)
        {
            return this.VerticalResize(ratio);
        }

        IDrawObject IDrawObject.Duplicate()
        {
            return this.Duplicate();
        }

        IDrawObject IDrawObject.Rotate(int degrees)
        {
            return this.Rotate(degrees);
        }

        void IDrawObject.Draw(IDrawImage image)
        {
            this.Draw(image);
        }
    }
}

