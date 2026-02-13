using System.Drawing;
using System.Windows.Forms;

namespace UpdateApp
{
    public partial class ExecutionForm : Form
    {
        public ExecutionForm()
        {
            this.InitializeComponent();
            this.PrepareUiForExecution();
        }

        private void PrepareUiForExecution()
        {
            this.SectionLabel.Text = string.Empty;
            this.PhaseLabel.Text = string.Empty;
            this.PrefixLabel.Text = string.Empty;
        }

        private void UpdateLabel(Label label, string text, Color color)
        {
            label.Text = $"{text} ...";
            label.ForeColor = color;
            label.Refresh();
        }

        public void PhaseLabelUpdate(string str, Color color)
        {
            this.UpdateLabel(this.PhaseLabel, str, color);
        }

        public void PrefixLabelUpdate(string str, Color color)
        {
            this.UpdateLabel(this.PrefixLabel, str, color);
        }
        public void SectionLabelUpdate(string str, Color color)
        {
            this.UpdateLabel(this.SectionLabel, str, color);
        }
    }
}
