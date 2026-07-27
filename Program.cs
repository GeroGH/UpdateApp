using System;
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

            foreach (var part in Catalog.Parts)
            {
                try
                {
                    PhaseModifier.Modify(part);

                    if (useProposal == DialogResult.Yes)
                    {
                        type = GetAssemblyType(part);

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
        private static string GetAssemblyType(Part part)
        {
            var name = part.Name.ToUpper();

            if (name.Contains("ANGLE"))
                return "A";

            if (name.Contains("BEAM"))
                return "B";

            if (name.Contains("BRACE"))
                return "BR";

            if (name.Contains("BRACKET"))
                return "BK";

            if (name.Contains("CHANNEL"))
                return "CH";

            if (name.Contains("COLUMN"))
                return "C";

            if (name.Contains("LOOSE"))
                return "L";

            if (name.Contains("PACK"))
                return "PK";

            if (name.Contains("PLATE"))
                return "PL";

            if (name.Contains("GIRDER"))
                return "G";

            if (name.Contains("RAFTER"))
                return "R";

            if (name.Contains("TRIMMER"))
                return "T";

            if (name.Contains("TRUSS"))
                return "TR";

            return "AD";
        }
    }
}
