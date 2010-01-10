namespace AjConcurr.Tsp
{
    partial class Form1
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
            this.pcbTravel = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbTravel)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbTravel
            // 
            this.pcbTravel.Location = new System.Drawing.Point(12, 16);
            this.pcbTravel.Name = "pcbTravel";
            this.pcbTravel.Size = new System.Drawing.Size(240, 240);
            this.pcbTravel.TabIndex = 3;
            this.pcbTravel.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(259, 47);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(0, 13);
            this.lblValue.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 273);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.pcbTravel);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Genetic Algorithm with AjConcurr";
            ((System.ComponentModel.ISupportInitialize)(this.pcbTravel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbTravel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblValue;
    }
}

