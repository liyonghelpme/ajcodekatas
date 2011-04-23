using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjDraw
{
    public class Composite : IDrawObject<Composite>, IDrawObject
    {
        private IList<IDrawObject> elements;

        public Composite()
        {
            this.elements = new List<IDrawObject>();
        }

        private Composite(IList<IDrawObject> elements)
        {
            this.elements = elements;
        }

        public Composite Add(IDrawObject element)
        {
            this.elements.Add(element);
            return this;
        }

        public Composite Translate(Point p)
        {
            this.elements = new List<IDrawObject>(
                this.elements.Select(x => x.Translate(p))
                );

            return this;
        }

        public Composite Resize(double ratio)
        {
            this.elements = this.elements.Select(x => x.Resize(ratio)).ToList();

            return this;
        }

        public Composite HorizontalResize(double ratio)
        {
            this.elements = this.elements.Select(x => x.HorizontalResize(ratio)).ToList();

            return this;
        }

        public Composite VerticalResize(double ratio)
        {
            this.elements = this.elements.Select(x => x.VerticalResize(ratio)).ToList();

            return this;
        }

        public Composite Duplicate()
        {
            return new Composite(this.elements);
        }

        public Composite Rotate(int degrees)
        {
            this.elements = this.elements.Select(x => x.Rotate(degrees)).ToList();

            return this;
        }

        public void Draw(IDrawImage image)
        {
            foreach (IDrawObject element in this.elements)
                element.Draw(image);
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
