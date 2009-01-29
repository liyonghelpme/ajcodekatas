using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicExpressionsExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            this.Calculate();
        }

        private void Calculate()
        {
            Calculator calculator = new Calculator();
            Plotter plotter = new Plotter();

            try
            {
                Delegate e = calculator.CompileFormula(this.txtFormula.Text);
                double[] values = calculator.Calculate(e, -10, 10, 0.01);

                this.picPlot.Image = plotter.Plot(values, this.picPlot.Width, this.picPlot.Height, 0.01, 0.01);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
