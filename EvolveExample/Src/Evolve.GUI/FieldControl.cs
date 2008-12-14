using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Evolve;

namespace Evolve.GUI
{
    public partial class FieldControl : UserControl
    {
        private static Random random = new Random();

        public FieldControl()
        {
            InitializeComponent();
        }

        public World World { get; set; }

        public void DrawWorld()
        {
            Bitmap bitmap = new Bitmap(picField.Width, picField.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            picField.Image = bitmap;

            if (this.World == null)
            {
                return;
            }

            int cellwidth = picField.Width / (this.World.Field.Width + 2);
            int cellheight = picField.Height / (this.World.Field.Height + 2);

            int cellsize = Math.Min(cellwidth, cellheight);
            Size size = new Size(cellsize, cellsize);
            Brush cellbrush;

            for (int x = 0; x < this.World.Field.Width; x++)
            {
                for (int y = 0; y < this.World.Field.Height; y++)
                {
                    int celltop = (y + 1) * cellsize;
                    int cellleft = (x + 1) * cellsize;
                    int food = this.World.Field.GetFoodAt(x, y);

                    if (food < 0)
                        food = 0;

                    if (food > 255)
                        food = 255;

                    Color color = Color.FromArgb(0, food, 0);

                    cellbrush = new SolidBrush(color);

                    graphics.FillRectangle(cellbrush, new Rectangle(new Point(cellleft, celltop), size));
                }
            }

            foreach (Animal animal in this.World.Animals) 
            {
                if (animal.Energy < 1)
                    continue;

                int x;
                int y;

                x = (animal.XPosition + 1) * cellsize + 2 + random.Next(cellsize-2);
                y = (animal.YPosition + 1) * cellsize + 2 + random.Next(cellsize-2);

                graphics.FillEllipse(Brushes.Red, x, y, cellsize / 2, cellsize / 2);
            }
        }

    }
}

