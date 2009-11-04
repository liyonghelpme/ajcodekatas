using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageExampleLibrary
{
    public class CreateUtilities
    {
        private static Random random = new Random();

        public static Color CreateColor()
        {
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));

            return color;
        }

        public static Point CreatePoint(int width, int height)
        {
            int w = random.Next(width);
            int h = random.Next(height);

            return new Point(w, h);
        }

        public static Point[] CreatePoints(int npoints, int width, int height)
        {
            Point[] points = new Point[npoints];

            for (int k = 0; k < npoints; k++)
                points[k] = CreatePoint(width, height);

            return points;
        }

        public static Polygon CreatePolygon(int width, int height, int nsides, int deltaw, int deltah)
        {
            Point[] points = new Point[nsides];
            Color color = CreateColor();

            points[0] = CreatePoint(width, height);

            for (int k = 1; k < nsides; k++)
                points[k] = new Point(points[k-1].X + random.Next(deltaw) - deltaw / 2, points[k-1].Y + random.Next(deltah) - deltah / 2);

            return new Polygon(points, color);
        }

        public static PolygonalImage CreatePolygonalImage(int width, int height, int npolygons, int nsides)
        {
            Polygon[] polygons = new Polygon[npolygons];

            for (int k = 0; k < polygons.Length; k++)
                polygons[k] = CreatePolygon(width, height, nsides, width / 5, height / 5);

            PolygonalImage image = new PolygonalImage(polygons);

            return image;
        }

        public static Population CreatePopulation(int nimages, int width, int height, int npolygons, int nsides)
        {
            PolygonalImage[] images = new PolygonalImage[nimages];

            for (int k = 0; k < images.Length; k++)
                images[k] = CreatePolygonalImage(width, height, npolygons, nsides);

            return new Population(images);
        }
    }
}

