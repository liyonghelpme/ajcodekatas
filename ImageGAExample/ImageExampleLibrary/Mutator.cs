using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageExampleLibrary
{
    public class Mutator
    {
        private static Random random = new Random();

        public PolygonalImage Mutate(PolygonalImage image)
        {
            Polygon[] newpolygons = new Polygon[image.Polygons.Length];

            //System.Array.Copy(image.Polygons, newpolygons, newpolygons.Length);

            //int p = random.Next(newpolygons.Length);

            //newpolygons[p] = this.MutatePolygon(newpolygons[p]);

            for (int k = 0; k < newpolygons.Length; k++)
                newpolygons[k] = this.MutatePolygon(image.Polygons[k]);

            return new PolygonalImage(newpolygons);
        }

        private Polygon MutatePolygon(Polygon polygon)
        {
            if ((random.Next() % 10) != 0)
                return polygon;

            Color newcolor = polygon.Color;
            Point[] newpoints = new Point[polygon.Points.Length];

            newcolor = MutateColor(polygon.Color);
            for (int k = 0; k < newpoints.Length; k++)
                newpoints[k] = MutatePoint(polygon.Points[k]);

            //Point[] newpoints = new Point[polygon.Points.Count];
            //for (int k = 0; k < polygon.Points.Count; k++)
            //    newpoints[k] = MutatePoint(polygon.Points[k]);

            //Color newcolor = MutateColor(polygon.Color);

            return new Polygon(newpoints, newcolor);
        }

        private Point MutatePoint(Point point)
        {
            Point newpoint = new Point(point.X + random.Next(20) - 10, point.Y + random.Next(20) - 10);
            return newpoint;
        }

        private Color MutateColor(Color color)
        {
            Color newcolor = Color.FromArgb((color.A + random.Next(20) - 10 + 256) % 256,
                     (color.R + random.Next(20) - 10 + 256) % 256,
                     (color.G + random.Next(20) - 10 + 256) % 256,
                     (color.B + random.Next(20) - 10 + 256) % 256);

            return newcolor;
        }
    }
}
