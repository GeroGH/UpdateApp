using System;
using System.Windows.Forms;

namespace UpdateApp
{
    public partial class NumberingSystemForm : Form
    {
        public enum NumberingSystem
        {
            Current,
            Current1000,
            ProposalABC,
            ProposalA,
        }
        public NumberingSystem SelectedSystem { get; private set; }
        public NumberingSystemForm()
        {
            this.InitializeComponent();
            this.TopMost = true;
            this.CenterToScreen();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (this.radioCurrent.Checked)
                this.SelectedSystem = NumberingSystem.Current;

            if (this.radioCurrent1000.Checked)
                this.SelectedSystem = NumberingSystem.Current1000;

            if (this.radioProposalA.Checked)
                this.SelectedSystem = NumberingSystem.ProposalA;

            if (this.radioProposalABC.Checked)
                this.SelectedSystem = NumberingSystem.ProposalABC;

            this.DialogResult = DialogResult.OK;
        }
    }
}
