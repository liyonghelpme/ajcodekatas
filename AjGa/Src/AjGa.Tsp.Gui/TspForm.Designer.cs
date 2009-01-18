namespace AjGa.Tsp.Gui
{
    partial class TspForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picTravel = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtRun = new System.Windows.Forms.TextBox();
            this.numSideSize = new System.Windows.Forms.NumericUpDown();
            this.lblSideSize = new System.Windows.Forms.Label();
            this.panControl = new System.Windows.Forms.Panel();
            this.lblRuns = new System.Windows.Forms.Label();
            this.lblBestSoFar = new System.Windows.Forms.Label();
            this.lblDistribution = new System.Windows.Forms.Label();
            this.cmbDistribution = new System.Windows.Forms.ComboBox();
            this.lblNoRuns = new System.Windows.Forms.Label();
            this.numNoRuns = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblNoCities = new System.Windows.Forms.Label();
            this.numNoPoints = new System.Windows.Forms.NumericUpDown();
            this.lblPopulationSize = new System.Windows.Forms.Label();
            this.numPopulationSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.picTravel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSideSize)).BeginInit();
            this.panControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoRuns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNoPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).BeginInit();
            this.SuspendLayout();
            // 
            // picTravel
            // 
            this.picTravel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picTravel.Location = new System.Drawing.Point(12, 12);
            this.picTravel.Name = "picTravel";
            this.picTravel.Size = new System.Drawing.Size(344, 362);
            this.picTravel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTravel.TabIndex = 0;
            this.picTravel.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(22, 17);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(107, 31);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtValue
            // 
            this.txtValue.Enabled = false;
            this.txtValue.Location = new System.Drawing.Point(22, 289);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(107, 20);
            this.txtValue.TabIndex = 13;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRun
            // 
            this.txtRun.Enabled = false;
            this.txtRun.Location = new System.Drawing.Point(22, 331);
            this.txtRun.Name = "txtRun";
            this.txtRun.Size = new System.Drawing.Size(107, 20);
            this.txtRun.TabIndex = 15;
            this.txtRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numSideSize
            // 
            this.numSideSize.Location = new System.Drawing.Point(22, 239);
            this.numSideSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numSideSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSideSize.Name = "numSideSize";
            this.numSideSize.Size = new System.Drawing.Size(39, 20);
            this.numSideSize.TabIndex = 9;
            this.numSideSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // lblSideSize
            // 
            this.lblSideSize.AutoSize = true;
            this.lblSideSize.Location = new System.Drawing.Point(19, 210);
            this.lblSideSize.Name = "lblSideSize";
            this.lblSideSize.Size = new System.Drawing.Size(28, 26);
            this.lblSideSize.TabIndex = 8;
            this.lblSideSize.Text = "Side\r\nSize";
            // 
            // panControl
            // 
            this.panControl.Controls.Add(this.lblRuns);
            this.panControl.Controls.Add(this.lblBestSoFar);
            this.panControl.Controls.Add(this.lblDistribution);
            this.panControl.Controls.Add(this.cmbDistribution);
            this.panControl.Controls.Add(this.lblNoRuns);
            this.panControl.Controls.Add(this.numNoRuns);
            this.panControl.Controls.Add(this.btnStop);
            this.panControl.Controls.Add(this.lblNoCities);
            this.panControl.Controls.Add(this.numNoPoints);
            this.panControl.Controls.Add(this.lblPopulationSize);
            this.panControl.Controls.Add(this.numPopulationSize);
            this.panControl.Controls.Add(this.txtValue);
            this.panControl.Controls.Add(this.lblSideSize);
            this.panControl.Controls.Add(this.btnRun);
            this.panControl.Controls.Add(this.numSideSize);
            this.panControl.Controls.Add(this.txtRun);
            this.panControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.panControl.Location = new System.Drawing.Point(372, 0);
            this.panControl.Name = "panControl";
            this.panControl.Size = new System.Drawing.Size(145, 386);
            this.panControl.TabIndex = 6;
            // 
            // lblRuns
            // 
            this.lblRuns.AutoSize = true;
            this.lblRuns.Location = new System.Drawing.Point(19, 315);
            this.lblRuns.Name = "lblRuns";
            this.lblRuns.Size = new System.Drawing.Size(32, 13);
            this.lblRuns.TabIndex = 14;
            this.lblRuns.Text = "Runs";
            // 
            // lblBestSoFar
            // 
            this.lblBestSoFar.AutoSize = true;
            this.lblBestSoFar.Location = new System.Drawing.Point(21, 273);
            this.lblBestSoFar.Name = "lblBestSoFar";
            this.lblBestSoFar.Size = new System.Drawing.Size(57, 13);
            this.lblBestSoFar.TabIndex = 12;
            this.lblBestSoFar.Text = "Best so far";
            // 
            // lblDistribution
            // 
            this.lblDistribution.AutoSize = true;
            this.lblDistribution.Location = new System.Drawing.Point(19, 100);
            this.lblDistribution.Name = "lblDistribution";
            this.lblDistribution.Size = new System.Drawing.Size(59, 13);
            this.lblDistribution.TabIndex = 2;
            this.lblDistribution.Text = "Distribution";
            // 
            // cmbDistribution
            // 
            this.cmbDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistribution.FormattingEnabled = true;
            this.cmbDistribution.Items.AddRange(new object[] {
            "Random",
            "Grid"});
            this.cmbDistribution.Location = new System.Drawing.Point(22, 116);
            this.cmbDistribution.Name = "cmbDistribution";
            this.cmbDistribution.Size = new System.Drawing.Size(107, 21);
            this.cmbDistribution.TabIndex = 3;
            // 
            // lblNoRuns
            // 
            this.lblNoRuns.AutoSize = true;
            this.lblNoRuns.Location = new System.Drawing.Point(78, 210);
            this.lblNoRuns.Name = "lblNoRuns";
            this.lblNoRuns.Size = new System.Drawing.Size(39, 26);
            this.lblNoRuns.TabIndex = 10;
            this.lblNoRuns.Text = "No. of \r\nRuns";
            // 
            // numNoRuns
            // 
            this.numNoRuns.Location = new System.Drawing.Point(81, 239);
            this.numNoRuns.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numNoRuns.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numNoRuns.Name = "numNoRuns";
            this.numNoRuns.Size = new System.Drawing.Size(48, 20);
            this.numNoRuns.TabIndex = 11;
            this.numNoRuns.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(22, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(107, 31);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblNoCities
            // 
            this.lblNoCities.AutoSize = true;
            this.lblNoCities.Location = new System.Drawing.Point(19, 151);
            this.lblNoCities.Name = "lblNoCities";
            this.lblNoCities.Size = new System.Drawing.Size(39, 26);
            this.lblNoCities.TabIndex = 4;
            this.lblNoCities.Text = "No. of \r\nPoints";
            // 
            // numNoPoints
            // 
            this.numNoPoints.Location = new System.Drawing.Point(22, 180);
            this.numNoPoints.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numNoPoints.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numNoPoints.Name = "numNoPoints";
            this.numNoPoints.Size = new System.Drawing.Size(39, 20);
            this.numNoPoints.TabIndex = 5;
            this.numNoPoints.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblPopulationSize
            // 
            this.lblPopulationSize.AutoSize = true;
            this.lblPopulationSize.Location = new System.Drawing.Point(78, 151);
            this.lblPopulationSize.Name = "lblPopulationSize";
            this.lblPopulationSize.Size = new System.Drawing.Size(60, 26);
            this.lblPopulationSize.TabIndex = 6;
            this.lblPopulationSize.Text = "Population \r\nSize";
            // 
            // numPopulationSize
            // 
            this.numPopulationSize.Location = new System.Drawing.Point(81, 180);
            this.numPopulationSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numPopulationSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPopulationSize.Name = "numPopulationSize";
            this.numPopulationSize.Size = new System.Drawing.Size(48, 20);
            this.numPopulationSize.TabIndex = 7;
            this.numPopulationSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // TspForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 386);
            this.Controls.Add(this.panControl);
            this.Controls.Add(this.picTravel);
            this.Name = "TspForm";
            this.Text = "Travelling Salesman Problem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TspForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picTravel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSideSize)).EndInit();
            this.panControl.ResumeLayout(false);
            this.panControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoRuns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNoPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTravel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtRun;
        private System.Windows.Forms.NumericUpDown numSideSize;
        private System.Windows.Forms.Label lblSideSize;
        private System.Windows.Forms.Panel panControl;
        private System.Windows.Forms.Label lblPopulationSize;
        private System.Windows.Forms.NumericUpDown numPopulationSize;
        private System.Windows.Forms.Label lblNoCities;
        private System.Windows.Forms.NumericUpDown numNoPoints;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblNoRuns;
        private System.Windows.Forms.NumericUpDown numNoRuns;
        private System.Windows.Forms.ComboBox cmbDistribution;
        private System.Windows.Forms.Label lblDistribution;
        private System.Windows.Forms.Label lblRuns;
        private System.Windows.Forms.Label lblBestSoFar;
    }
}