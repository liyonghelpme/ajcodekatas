using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageExampleLibrary
{
    public class Evaluator
    {
        private Bitmap original;
        private Point[] points;

        public Evaluator(Bitmap bitmap)
        {
            this.original = bitmap;

            int width = bitmap.Width;
            int height = bitmap.Height;

            this.points = new Point[(width / 5) * (height / 5)];

            for (int k = 0; k < this.points.Length; k++)
                this.points[k] = CreateUtilities.CreatePoint(width, height);
        }

        public long Evaluate(PolygonalImage image)
        {
            Bitmap bitmap = new Bitmap(this.original.Width, this.original.Height);
            image.DrawBitmap(bitmap);

            image.Distance = BitmapDistance(this.original, bitmap, this.points);

            return image.Distance;
        }

        private static long BitmapDistance(Bitmap bitmap1, Bitmap bitmap2, Point[] points)
        {
            long distance = 0;

            foreach (Point point in points)
                distance += PixelDistance(bitmap1, bitmap2, point.X, point.Y);

            return distance;
        }

        private static int PixelDistance(Bitmap bitmap1, Bitmap bitmap2, int x, int y)
        {
            Color color1 = bitmap1.GetPixel(x, y);
            Color color2 = bitmap2.GetPixel(x, y);

            return ColorDistance(color1, color2);
        }

        private static int ColorDistance(Color color1, Color color2)
        {
            int da = Math.Abs(((int)color1.A) - ((int)color2.A));
            int dr = Math.Abs(((int)color1.R) - ((int)color2.R));
            int dg = Math.Abs(((int)color1.G) - ((int)color2.G));
            int db = Math.Abs(((int)color1.B) - ((int)color2.B));

            return dr*dr + dg*dg + db*db + da*da;
        }
    }
}
