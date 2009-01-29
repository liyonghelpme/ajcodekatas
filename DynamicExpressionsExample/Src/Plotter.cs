namespace DynamicExpressionsExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    class Plotter
    {
        public Image Plot(double[] values, int width, int height, double xscale, double yscale)
        {
            Image image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

            double[] xvalues = new double[values.Length];
            double[] yvalues = new double[values.Length];

            double xstep = width / (double) values.Length;
            double ystep = xstep / yscale;

            for (int k = 0; k < values.Length; k++)
            {
                xvalues[k] = xstep * k;
                yvalues[k] = -values[k] * ystep + height/2;
            }

            for (int k = 1; k < values.Length; k++)
            {
                graphics.DrawLine(Pens.Black, (float) xvalues[k - 1], (float) yvalues[k - 1], (float) xvalues[k], (float) yvalues[k]);
            }

            return image;
        }
    }
}
