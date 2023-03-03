using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class PhaseModifier
    {
        internal static void Update(Part part, ExecutionForm excutionForm, Color color)
        {
            part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            part.SetPhase(mainPartPhase);
            part.Modify();

            var bolts = part.GetBolts();
            if (bolts == null)
            {
                return;
            }

            while (bolts.MoveNext())
            {
                var boltGroup = bolts.Current as BoltGroup;
                if (boltGroup == null)
                {
                    continue;
                }

                boltGroup.PartToBeBolted.GetPhase(out var boltPartPhase);
                bolts.Current.SetPhase(boltPartPhase);
            }

            excutionForm.PhaseLabelUpdate($"Part Phase <<< {mainPartPhase.PhaseNumber} >>> Updated ", color);
        }
    }
}
