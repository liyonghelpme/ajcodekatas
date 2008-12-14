using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Evolve.GUI
{
    public partial class EvolveForm : Form
    {
        private Thread evolvethread;
        private World world;
        private int lastfoodlevel;
        private List<ListViewItem> population;

        delegate void FormDelegate();
        FormDelegate redrawdelegate;
        FormDelegate populationdelegate;

        private int foodlevel = 40000;
        private int populationsize = 80;

        public EvolveForm()
        {
            InitializeComponent();

            this.world = new World(20, 20, this.foodlevel, this.populationsize, 400);
            this.fieldControl1.World = world;

            this.fieldControl1.DrawWorld();
            this.redrawdelegate = this.DrawField;
            this.populationdelegate = this.ShowPopulationList;
        }

        private void fieldControl1_SizeChanged(object sender, EventArgs e)
        {
            this.fieldControl1.DrawWorld();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRunning())
            {
                Stop();
            }

            Application.Exit();
        }

        private bool IsRunning()
        {
            return this.evolvethread != null && this.evolvethread.IsAlive;
        }

        private void Stop()
        {
            this.evolvethread.Abort();
            this.evolvethread = null;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start() {
            if (IsRunning())
                this.Stop();

            this.world = new World(20, 20, foodlevel, populationsize, 400);

            fieldControl1.World = world;
            fieldControl1.DrawWorld();

            evolvethread = new Thread(new ThreadStart(Evolve));

            evolvethread.Start();
        }

        private void Evolve()
        {
            int n = 0;
            int minlevel = this.world.Field.FoodLevel / 2;

            while (true) {
                this.world.RunStep();

                if (n == 0)
                    Invoke(this.redrawdelegate);

                n = (n++) % 10;

                int foodlevel = this.world.Field.FoodLevel;

                if (foodlevel <= minlevel || foodlevel == this.lastfoodlevel)
                {
                    this.ShowPopulation();
                    this.world.Evolve();
                    this.world.Reset();
                }

                lastfoodlevel = foodlevel;

                Thread.Sleep(0);
            }
        }

        private void ShowPopulation()
        {
            population = new List<ListViewItem>();
            List<Animal> animals = this.world.OrderedAnimals();

            foreach (Animal animal in animals)
                population.Add(new ListViewItem(new string[] { animal.ProgramText, animal.Energy.ToString() }));

            Invoke(this.populationdelegate);
        }

        private void ShowPopulationList()
        {
            this.listView1.Items.Clear();

            foreach (ListViewItem item in this.population)
            {
                this.listView1.Items.Add(item);
            }
        }

        private void DrawField()
        {
            Animal animal = this.world.BestSoFar();
            fieldControl1.DrawWorld();
            this.toolStripStatusLabel1.Text = "Food Level: " + this.world.Field.FoodLevel + " Best Animal Energy: "  + animal.Energy.ToString() + " Program: " + animal.ProgramText;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.foodlevel = 40000;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.foodlevel = 20000;
            toolStripMenuItem2.Checked = true;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.foodlevel = 80000;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem5.Checked = false;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.foodlevel = 160000;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = true;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            populationsize = 20;
            toolStripMenuItem6.Checked = true;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            populationsize = 40;
            toolStripMenuItem6.Checked = false;
            toolStripMenuItem7.Checked = true;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            populationsize = 80;
            toolStripMenuItem6.Checked = false;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = true;
            toolStripMenuItem9.Checked = false;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            populationsize = 160;
            toolStripMenuItem6.Checked = false;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = true;
        }
    }
}
