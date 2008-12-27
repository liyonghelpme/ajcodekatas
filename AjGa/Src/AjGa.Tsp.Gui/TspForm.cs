using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AjGa.Tsp.Gui
{
    public partial class TspForm : Form
    {
        private TravelImage travelimage = new TravelImage();
        delegate void UpdateView(Image image, int value, int run);
        UpdateView updatestatus;
        Thread runthread = null;

        public TspForm()
        {
            InitializeComponent();
            this.updatestatus = UpdateStatus;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRunning())
                Stop();

            runthread = new Thread(new ThreadStart(Run));
            runthread.Start();
        }

        private void Run()
        {
            List<Position> positions = new List<Position>();
            Random random = new Random();
            Evaluator evaluator = new Evaluator(positions);

            for (int k = 0; k < 100; k++)
            {
                positions.Add(new Position(random.Next(12), random.Next(12)));
            }

            Population population = new Population(100, positions.Count);
            List<IGenomaFactory<int, int>> operators = new List<IGenomaFactory<int, int>>();

            for (int k = 0; k < 60; k++)
            {
                operators.Add(new GradientMutator(evaluator));
            }

            for (int k = 0; k < 20; k++)
            {
                operators.Add(new Mutator());
            }

            Evolution evolution = new Evolution(new Evaluator(positions), operators);

            try
            {
                for (int n = 0; n < 500; n++)
                {
                    Population newpopulation = (Population)evolution.RunGeneration(population);
                    Genoma best = (Genoma)population.Genomas[0];
                    travelimage.DrawTravel(best, positions);
                    Invoke(this.updatestatus, travelimage.Image, best.Value, n);

                    population = newpopulation;
                    Thread.Sleep(0);
                }
            }
            catch
            {
            }
        }

        private void UpdateStatus(Image image, int value, int run)
        {
            this.picTravel.Image = image;
            txtValue.Text = value.ToString();
            txtRun.Text = run.ToString();
            picTravel.Refresh();
            txtValue.Refresh();
            txtRun.Refresh();
        }

        private bool IsRunning()
        {
            return (runthread != null);
        }

        private void Stop()
        {
            runthread.Abort();
            runthread = null;
        }

        private void TspForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsRunning())
                Stop();
        }
    }
}
