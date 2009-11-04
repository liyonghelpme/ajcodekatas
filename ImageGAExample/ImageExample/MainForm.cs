using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageExampleLibrary;

namespace ImageExample
{
    public partial class MainForm : Form
    {
        Random random = new Random();

        Bitmap bitmap;
        Bitmap bitmap2;

        Population population;

        Evaluator evaluator;

        Mutator mutator = new Mutator();

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            this.bitmap = new Bitmap(this.openFileDialog1.FileName);

            this.picImage.Image = this.bitmap;

            this.CreateNewImage();
        }

        private void CreateNewImage()
        {
            this.bitmap2 = new Bitmap(this.bitmap.Width, this.bitmap.Height);

            Graphics graphics = Graphics.FromImage(this.bitmap2);

            graphics.FillRectangle(Brushes.White, 0, 0, this.bitmap2.Width, this.bitmap2.Height);

            Random random = new Random();
            int width = this.bitmap2.Width;
            int height = this.bitmap2.Height;

            this.evaluator = new Evaluator(this.bitmap);

            this.population = CreateUtilities.CreatePopulation(100, width, height, (width / 10) * (height / 10), 3);

            this.population.Evaluate(this.evaluator);
        }

        private void DrawNewImage(PolygonalImage image)
        {
            image.DrawBitmap(this.bitmap2);

            this.picImage.Image = this.bitmap2;
            this.lblDistance.Text = image.Distance.ToString();
        }

        private void drawNewImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DrawNewImage(this.population.GetBestImage());
        }

        private void MutateToBetterNewImage()
        {
            long best = 0;

            for (int k = 0; k < 10000; k++)
            {
                this.population = this.population.Mutate(this.mutator, this.evaluator);
                this.population.Evaluate(this.evaluator);

                PolygonalImage image = this.population.GetBestImage();

                if (image.Distance != best)
                {
                    this.DrawNewImage(image);
                    best = image.Distance;
                }

                //if ((k % 10) == 0)
                //{
                    //this.DrawNewImage(this.population.GetBestImage());
                //}

                Application.DoEvents();
            }

            this.DrawNewImage(this.population.GetBestImage());
        }

        private void mutateNewImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.population = this.population.Mutate(this.mutator, this.evaluator);
            this.DrawNewImage(this.population.GetBestImage());
        }

        private void mutateToBetterImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MutateToBetterNewImage();
        }
    }
}
