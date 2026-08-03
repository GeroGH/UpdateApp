using System;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using static UpdateApp.NumberingSystemForm;

namespace UpdateApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            NumberingSystem numberingSystem;

            using (var form = new NumberingSystemForm())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                numberingSystem = form.SelectedSystem;
            }

            var model = new Model();
            Catalog.CollectPartsFromTheModel();

            foreach (var part in Catalog.Parts)
            {
                try
                {
                    PhaseMod.Modify(part);

                    if (numberingSystem == NumberingSystem.Current)
                    {
                        NumberignModCurrent.Modify(part);
                    }

                    if (numberingSystem == NumberingSystem.Current1000)
                    {
                        NumberignModCurrent1000.Modify(part);
                    }

                    if (numberingSystem == NumberingSystem.ProposalA)
                    {
                        NumberingModProposalA.Modify(part);
                    }

                    if (numberingSystem == NumberingSystem.ProposalABC)
                    {
                        NumberingModProposalABC.Modify(part);
                    }

                    SectionMod.Modify(part);

                    part.Modify();

                    Operation.DisplayPrompt($"Part prefixes {part.PartNumber.Prefix}{part.PartNumber.StartNumber}, {part.AssemblyNumber.Prefix}{part.AssemblyNumber.StartNumber} updated...");
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
