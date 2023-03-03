using System.Drawing;
using System.Windows.Forms;
using Tekla.Structures.Model.Operations;

namespace UpdateApp
{
    public partial class ExecutionForm : Form
    {
        public ExecutionForm()
        {
            this.InitializeComponent();
        }

        public void UpdateCentralLabel(string str, Color color)
        {
            this.ClassLabel.Hide();
            this.SectionLabel.Hide();
            this.PrefixLabel.Hide();

            this.PhaseLabel.Text = str + " ...";
            this.PhaseLabel.ForeColor = color;
            this.PhaseLabel.Update();

            Operation.DisplayPrompt(str + " ...");
        }


        public void ClassLabelUpdate(string str, Color color)
        {
            this.ClassLabel.Show();
            this.ClassLabel.Text = str + " ...";
            this.ClassLabel.ForeColor = color;
            this.ClassLabel.Update();
        }

        public void PhaseLabelUpdate(string str, Color color)
        {
            this.PhaseLabel.Show();
            this.PhaseLabel.Text = str + " ...";
            this.PhaseLabel.ForeColor = color;
            this.PhaseLabel.Update();
        }

        public void SectionLabelUpdate(string str, Color color)
        {
            this.SectionLabel.Show();
            this.SectionLabel.Text = str + " ...";
            this.SectionLabel.ForeColor = color;
            this.SectionLabel.Update();
        }

        public void PrefixLabelUpdate(string str, Color color)
        {
            this.PrefixLabel.Show();
            this.PrefixLabel.Text = str + " ...";
            this.PrefixLabel.ForeColor = color;
            this.PrefixLabel.Update();
        }
    }
}
