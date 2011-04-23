using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjDraw
{
    public interface IDrawObject
    {
        IDrawObject Translate(Point p);

        IDrawObject Resize(double ratio);

        IDrawObject HorizontalResize(double ratio);

        IDrawObject VerticalResize(double ratio);

        IDrawObject Duplicate();

        IDrawObject Rotate(int degrees);

        void Draw(IDrawImage image);
    }

    public interface IDrawObject<T> where T : IDrawObject<T>, IDrawObject
    {
        T Translate(Point p);

        T Resize(double ratio);

        T HorizontalResize(double ratio);

        T VerticalResize(double ratio);

        T Duplicate();

        T Rotate(int degrees);

        void Draw(IDrawImage image);       
    }
}
