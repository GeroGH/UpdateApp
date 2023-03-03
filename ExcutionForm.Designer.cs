namespace UpdateApp
{
    partial class ExecutionForm
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
            this.ClassLabel = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.SectionLabel = new System.Windows.Forms.Label();
            this.PrefixLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClassLabel
            // 
            this.ClassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClassLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.ClassLabel.Location = new System.Drawing.Point(12, 9);
            this.ClassLabel.Name = "ClassLabel";
            this.ClassLabel.Size = new System.Drawing.Size(473, 33);
            this.ClassLabel.TabIndex = 0;
            this.ClassLabel.Text = "Class Update";
            this.ClassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseLabel.ForeColor = System.Drawing.Color.DarkMagenta;
            this.PhaseLabel.Location = new System.Drawing.Point(12, 42);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(473, 33);
            this.PhaseLabel.TabIndex = 1;
            this.PhaseLabel.Text = "Phase Update";
            this.PhaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SectionLabel
            // 
            this.SectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.SectionLabel.Location = new System.Drawing.Point(12, 108);
            this.SectionLabel.Name = "SectionLabel";
            this.SectionLabel.Size = new System.Drawing.Size(473, 33);
            this.SectionLabel.TabIndex = 2;
            this.SectionLabel.Text = "Section Update";
            this.SectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrefixLabel
            // 
            this.PrefixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrefixLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.PrefixLabel.Location = new System.Drawing.Point(12, 75);
            this.PrefixLabel.Name = "PrefixLabel";
            this.PrefixLabel.Size = new System.Drawing.Size(473, 33);
            this.PrefixLabel.TabIndex = 3;
            this.PrefixLabel.Text = "Prefix Update";
            this.PrefixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExecutionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 150);
            this.Controls.Add(this.PrefixLabel);
            this.Controls.Add(this.SectionLabel);
            this.Controls.Add(this.PhaseLabel);
            this.Controls.Add(this.ClassLabel);
            this.Name = "ExecutionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Application v1.29";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ClassLabel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.Label SectionLabel;
        private System.Windows.Forms.Label PrefixLabel;
    }
}

