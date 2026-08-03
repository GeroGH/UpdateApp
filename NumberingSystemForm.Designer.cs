namespace UpdateApp
{
    partial class NumberingSystemForm
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
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.radioCurrent1000 = new System.Windows.Forms.RadioButton();
            this.ok = new System.Windows.Forms.Button();
            this.radioProposalABC = new System.Windows.Forms.RadioButton();
            this.radioProposalA = new System.Windows.Forms.RadioButton();
            this.radioCurrent = new System.Windows.Forms.RadioButton();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.radioCurrent1000);
            this.gbOptions.Controls.Add(this.ok);
            this.gbOptions.Controls.Add(this.radioProposalABC);
            this.gbOptions.Controls.Add(this.radioProposalA);
            this.gbOptions.Controls.Add(this.radioCurrent);
            this.gbOptions.Location = new System.Drawing.Point(13, 13);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(243, 160);
            this.gbOptions.TabIndex = 0;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options:";
            // 
            // radioCurrent1000
            // 
            this.radioCurrent1000.AutoSize = true;
            this.radioCurrent1000.Location = new System.Drawing.Point(6, 47);
            this.radioCurrent1000.Name = "radioCurrent1000";
            this.radioCurrent1000.Size = new System.Drawing.Size(186, 17);
            this.radioCurrent1000.TabIndex = 4;
            this.radioCurrent1000.TabStop = true;
            this.radioCurrent1000.Text = "Current Bourne Convention +1000";
            this.radioCurrent1000.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(6, 118);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(232, 34);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // radioProposalABC
            // 
            this.radioProposalABC.AutoSize = true;
            this.radioProposalABC.Location = new System.Drawing.Point(6, 95);
            this.radioProposalABC.Name = "radioProposalABC";
            this.radioProposalABC.Size = new System.Drawing.Size(201, 17);
            this.radioProposalABC.TabIndex = 1;
            this.radioProposalABC.TabStop = true;
            this.radioProposalABC.Text = "Proposal Assembly Style U320-A,B,C ";
            this.radioProposalABC.UseVisualStyleBackColor = true;
            // 
            // radioProposalA
            // 
            this.radioProposalA.AutoSize = true;
            this.radioProposalA.Location = new System.Drawing.Point(6, 71);
            this.radioProposalA.Name = "radioProposalA";
            this.radioProposalA.Size = new System.Drawing.Size(178, 17);
            this.radioProposalA.TabIndex = 2;
            this.radioProposalA.TabStop = true;
            this.radioProposalA.Text = "Proposal Assembly Style U320-A";
            this.radioProposalA.UseVisualStyleBackColor = true;
            // 
            // radioCurrent
            // 
            this.radioCurrent.AutoSize = true;
            this.radioCurrent.Location = new System.Drawing.Point(6, 23);
            this.radioCurrent.Name = "radioCurrent";
            this.radioCurrent.Size = new System.Drawing.Size(153, 17);
            this.radioCurrent.TabIndex = 0;
            this.radioCurrent.TabStop = true;
            this.radioCurrent.Text = "Current Bourne Convention";
            this.radioCurrent.UseVisualStyleBackColor = true;
            // 
            // NumberingSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 177);
            this.Controls.Add(this.gbOptions);
            this.Name = "NumberingSystemForm";
            this.Text = "Numbering";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.RadioButton radioProposalA;
        private System.Windows.Forms.RadioButton radioProposalABC;
        private System.Windows.Forms.RadioButton radioCurrent;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.RadioButton radioCurrent1000;
    }
}