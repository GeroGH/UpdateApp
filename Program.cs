using System;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;

namespace UpdateApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var model = new Model();
            Catalog.CollectPartsFromTheModel();

            foreach (var part in Catalog.Parts)
            {
                try
                {
                    PhaseModifier.Modify(part);
                    NumberignSeriesModifier.Modify(part);
                    //NumberingSeriesModifierProposal.Modify(part, "B");
                    SectionModifier.Modify(part);
                    part.Modify();
                    Operation.DisplayPrompt($"Part prefixes {part.PartNumber.Prefix,-1}, {part.AssemblyNumber.Prefix,-1} updated ...");
                }
                catch (Exception)
                {
                    continue;
                }
            }

            model.CommitChanges();
            Catalog.SelectPartsInTheModel();
            Operation.DisplayPrompt($"Update complete on total of {Catalog.Parts.Count} parts !");
        }
    }
}
