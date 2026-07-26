using System;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;

namespace UpdateApp
{
    static class Program
    {
        private const string ProjectPrefix = "U";

        [STAThread]
        static void Main()
        {
            var model = new Model();
            Catalog.CollectPartsFromTheModel();

            var useProposal = DialogResult.No;

            useProposal = MessageBox.Show(
                "Would you like to use the proposed numbering convention?",
                "Numbering Convention",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            var type = string.Empty;

            if (useProposal == DialogResult.Yes)
            {
                type = Microsoft.VisualBasic.Interaction.InputBox("Enter the assembly type (e.g. B, C, B2, BA, BA2)","Assembly Type","B");

                if (string.IsNullOrWhiteSpace(type))
                    return;
            }

            foreach (var part in Catalog.Parts)
            {
                try
                {
                    PhaseModifier.Modify(part);

                    if (useProposal == DialogResult.Yes)
                    {
                        NumberingSeriesModifierProposal.Modify(part, ProjectPrefix, type);
                    }

                    if (useProposal == DialogResult.No)
                    {
                        NumberignSeriesModifier.Modify(part);
                    }

                    SectionModifier.Modify(part);

                    part.Modify();

                    Operation.DisplayPrompt($"Part prefixes {part.PartNumber.Prefix}, {part.AssemblyNumber.Prefix} updated...");
                }
                catch
                {
                    continue;
                }
            }

            model.CommitChanges();
            Catalog.SelectPartsInTheModel();

            Operation.DisplayPrompt($"Update complete on total of {Catalog.Parts.Count} parts!");
        }
    }
}
