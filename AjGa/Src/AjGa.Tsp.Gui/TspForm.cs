namespace AjGa.Tsp.Gui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public partial class TspForm : Form
    {
        private TravelImage travelimage;
        private short sidesize;
        private short nopoints;
        private short populationsize;
        private short noruns;
        private bool israndom;

        private Thread runthread = null;

        private UpdateView updatestatus;

        public TspForm()
        {
            this.InitializeComponent();
            this.updatestatus = this.UpdateStatus;
            this.cmbDistribution.SelectedIndex = 0;
        }

        private delegate void UpdateView(Genome best, int run, List<Position> positions);

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Start();
        }

        private void Start()
        {
            if (this.IsRunning())
            {
                this.Stop();
            }

            this.btnRun.Enabled = false;
            this.btnStop.Enabled = true;

            this.cmbDistribution.Enabled = false;

            this.numNoPoints.Enabled = false;
            this.numPopulationSize.Enabled = false;
            this.numSideSize.Enabled = false;
            this.numNoRuns.Enabled = false;

            this.sidesize = (short)this.numSideSize.Value;
            this.nopoints = (short)this.numNoPoints.Value;
            this.populationsize = (short)this.numPopulationSize.Value;
            this.noruns = (short)this.numNoRuns.Value;
            this.israndom = (this.cmbDistribution.SelectedIndex == 0);

            if (!this.israndom)
            {
                this.nopoints = (short)((this.sidesize + 1) * (this.sidesize + 1));
            }

            this.runthread = new Thread(new ThreadStart(this.Run));
            this.runthread.Start();
        }

        private void Run()
        {
            this.travelimage = new TravelImage(this.sidesize, this.sidesize);

            List<Position> positions = new List<Position>();
            Random random = new Random();
            Evaluator evaluator = new Evaluator(positions);

            if (this.israndom)
            {
                for (int k = 0; k < this.nopoints; k++)
                {
                    positions.Add(new Position(random.Next(this.sidesize), random.Next(this.sidesize)));
                }
            }
            else
            {
                for (int k = 0; k < this.sidesize; k++)
                {
                    for (int j = 0; j < this.sidesize; j++)
                    {
                        positions.Add(new Position(k, j));
                    }
                }
            }

            Population population = new Population(this.populationsize, positions.Count);
            List<IGenomeFactory<int, int>> operators = new List<IGenomeFactory<int, int>>();

            for (int k = 0; k < 20 * this.populationsize / 100; k++)
            {
                operators.Add(new GradientMutator(evaluator));
            }

            for (int k = 0; k < 50 * this.populationsize / 100; k++)
            {
                operators.Add(new Mutator());
            }

            Evolution evolution = new Evolution(new Evaluator(positions), operators);

            try
            {
                for (int n = 0; n < this.noruns; n++)
                {
                    Population newpopulation = (Population)evolution.RunGeneration(population);
                    Genome best = (Genome)population.Genomes[0];
                    Invoke(this.updatestatus, best, n + 1, positions);

                    population = newpopulation;
                    Thread.Sleep(0);
                }
            }
            catch
            {
            }
        }

        private void UpdateStatus(Genome best, int run, List<Position> positions)
        {
            this.travelimage.DrawTravel(this.picTravel.Width, this.picTravel.Height, best, positions);
            this.picTravel.Image = this.travelimage.Image;
            this.txtValue.Text = best.Value.ToString();
            this.txtRun.Text = run.ToString();
            this.picTravel.Refresh();
            this.txtValue.Refresh();
            this.txtRun.Refresh();
        }

        private bool IsRunning()
        {
            return this.runthread != null;
        }

        private void Stop()
        {
            this.runthread.Abort();
            this.runthread = null;

            this.btnRun.Enabled = true;
            this.btnStop.Enabled = false;

            this.cmbDistribution.Enabled = true;

            this.numNoPoints.Enabled = true;
            this.numPopulationSize.Enabled = true;
            this.numSideSize.Enabled = true;
            this.numNoRuns.Enabled = true;
        }

        private void TspForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsRunning())
            {
                this.Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.IsRunning())
                this.Stop();
        }
    }
}
