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
            ((System.ComponentModel.ISupportInitialize)(this.picTravel)).BeginInit();
            this.SuspendLayout();
            // 
            // picTravel
            // 
            this.picTravel.Location = new System.Drawing.Point(12, 12);
            this.picTravel.Name = "picTravel";
            this.picTravel.Size = new System.Drawing.Size(325, 362);
            this.picTravel.TabIndex = 0;
            this.picTravel.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(344, 13);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(104, 31);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtValue
            // 
            this.txtValue.Enabled = false;
            this.txtValue.Location = new System.Drawing.Point(344, 51);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRun
            // 
            this.txtRun.Enabled = false;
            this.txtRun.Location = new System.Drawing.Point(344, 83);
            this.txtRun.Name = "txtRun";
            this.txtRun.Size = new System.Drawing.Size(100, 20);
            this.txtRun.TabIndex = 3;
            this.txtRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TspForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 386);
            this.Controls.Add(this.txtRun);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.picTravel);
            this.Name = "TspForm";
            this.Text = "TspForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TspForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picTravel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picTravel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtRun;
    }
}