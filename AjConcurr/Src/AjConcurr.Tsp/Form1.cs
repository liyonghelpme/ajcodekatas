using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AjConcurr.Tsp
{
    public partial class Form1 : Form
    {
        private TravelImage travel;
        private Random random = new Random();
        private const int PopulationSize = 20;
        private const int MapSize = 12;

        public Form1()
        {
            InitializeComponent();

            this.travel = new TravelImage(MapSize, MapSize);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Channel genomas = new Channel();
            Channel evaluated = new Channel();
            Channel bestsofar = new Channel();
            Channel mutator = new Channel();

            // Generates the initial populations
            GoRoutines.Go(() =>
            {
                Genoma genoma = GenerateGenoma();

                for (int j = 0; j < PopulationSize * 10; j++)
                {
                    genomas.Send(this.Mutate(genoma));
                }
            });

            // Evaluate genomas
            GoRoutines.Go(() =>
            {
                while (true)
                {
                    Genoma genoma = (Genoma)genomas.Receive();
                    genoma.value = this.CalculateValue(genoma);
                    evaluated.Send(genoma);
                }
            });

            // Collect and select
            GoRoutines.Go(() =>
                {
                    GenomaComparer comparer = new GenomaComparer();

                    while (true)
                    {
                        List<Genoma> genomalist = new List<Genoma>();

                        for (int k = 0; k < PopulationSize; k++)
                        {
                            Genoma genoma = (Genoma)evaluated.Receive();
                            genomalist.Add(genoma);
                        }

                        genomalist.Sort(comparer);

                        GoRoutines.Go(() => bestsofar.Send(genomalist[0]));

                        for (int k = 0; k < PopulationSize / 5; k++)
                            GoRoutines.Go(() => evaluated.Send(genomalist[k]));

                        for (int k = 0; k < PopulationSize / 5; k++)
                        {
                            GoRoutines.Go(() => mutator.Send(genomalist[k]));
                            GoRoutines.Go(() => mutator.Send(genomalist[k]));
                            GoRoutines.Go(() => mutator.Send(genomalist[k]));
                            GoRoutines.Go(() => mutator.Send(genomalist[k]));
                        }
                    }
                });

            // Mutates
            GoRoutines.Go(() =>
            {
                Random rnd = new Random();
                while (true)
                {
                    Genoma genoma = (Genoma)mutator.Receive();
                    Genoma newgenoma = this.Mutate(genoma);

                    //if (rnd.Next(2) == 0)
                    //    newgenoma = this.Mutate(newgenoma);

                    while (newgenoma.value >= genoma.value)
                    {
                        if (rnd.Next(3) == 0)
                            break;

                        newgenoma = this.Mutate(genoma);
                    }

                    evaluated.Send(newgenoma);
                }
            });

            // Receives and draws the results
            GoRoutines.Go(() =>
            {
                Genoma best = null;

                while (true)
                {
                    Genoma genoma = (Genoma)bestsofar.Receive();

                    if (best == null || best.value > genoma.value)
                    {
                        best = genoma;
                        this.BestGenoma(genoma);
                    }
                }
            });
        }

        private Genoma GenerateGenoma()
        {
            Genoma genoma = new Genoma();

            for (int k = 0; k < MapSize*MapSize / 2; k++)
            {
                Point pt = new Point();
                pt.x = this.random.Next(MapSize);
                pt.y = this.random.Next(MapSize);

                genoma.travel.Add(pt);
            }
            return genoma;
        }

        delegate void BestGenomaDelegate(Genoma g);

        private void BestGenoma(Genoma genoma)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BestGenomaDelegate(BestGenoma), genoma);
                return;
            }

            travel.DrawTravel(genoma);
            pcbTravel.Image = travel.Image;
            lblValue.Text = genoma.value.ToString();
        }

        private int CalculateValue(Genoma genoma)
        {
            int value = 0;

            Point point1 = null;

            foreach (Point point in genoma.travel)
            {
                if (point1 != null)
                    value += (point.x - point1.x) * (point.x - point1.x) + (point.y - point1.y) * (point.y - point1.y);

                point1 = point;
            }

            return value;
        }

        private Genoma Mutate(Genoma genoma)
        {
            Genoma newgenoma = new Genoma();

            int position1 = random.Next(genoma.travel.Count);
            int position2 = random.Next(genoma.travel.Count);

            for (int k = 0; k < genoma.travel.Count; k++)
            {
                int p = k;

                if (position1 == k)
                    p = position2;
                if (position2 == k)
                    p = position1;

                newgenoma.travel.Add(genoma.travel[p]);
            }

            newgenoma.value = this.CalculateValue(newgenoma);

            return newgenoma;
        }
    }
}
