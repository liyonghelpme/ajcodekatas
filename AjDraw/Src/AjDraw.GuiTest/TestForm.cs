using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AjDraw.GuiTest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            Line line = new Line(new Point(20, 20), new Point(50, 50));
            DrawImage image = new DrawImage(this.pictureBox1.Width, this.pictureBox1.Height);
            line.Draw(image);
            this.pictureBox1.Image = image.Image;
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            Line line = new Line(new Point(20, 0), new Point(100, 0));
            Composite composite = new Composite();

            composite.Add(line);

            for (int k = 10; k <= 270; k+=10)
                composite.Add(line.Duplicate().Rotate(k));

            DrawImage image = new DrawImage(this.pictureBox1.Width, this.pictureBox1.Height);
            composite.Draw(image);
            this.pictureBox1.Image = image.Image;
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            Wave wave = new Wave(-360, 360, 30);
            wave.HorizontalResize(0.5);
            wave.VerticalResize(2);
            DrawImage image = new DrawImage(this.pictureBox1.Width, this.pictureBox1.Height);
            wave.Draw(image);
            this.pictureBox1.Image = image.Image;
        }
    }
}
