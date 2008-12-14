namespace Evolve.GUI
{
    partial class FieldControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picField)).BeginInit();
            this.SuspendLayout();
            // 
            // picField
            // 
            this.picField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picField.Location = new System.Drawing.Point(0, 0);
            this.picField.Name = "picField";
            this.picField.Size = new System.Drawing.Size(150, 150);
            this.picField.TabIndex = 0;
            this.picField.TabStop = false;
            // 
            // FieldControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picField);
            this.Name = "FieldControl";
            ((System.ComponentModel.ISupportInitialize)(this.picField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picField;
    }
}
