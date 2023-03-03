using System;
using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class SectionModifier
    {
        internal static void Update(Part part, ExecutionForm excutionForm, Color color)
        {
            var materialType = string.Empty;
            part.GetReportProperty("MATERIAL_TYPE", ref materialType);
            if (materialType != "STEEL")
            {
                excutionForm.SectionLabelUpdate($"The Part Is Not Steel", color);
                return;
            }

            var name = string.Empty;
            part.GetReportProperty("NAME", ref name);
            if (name == "HILTI")
            {
                excutionForm.SectionLabelUpdate($"The Part Is Hilti Anchor", color);
                return;
            }

            var section = part.Profile.ProfileString;

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            if (profileType == "B")
            {
                var width = 0.0;
                part.GetReportProperty("WIDTH", ref width);
                section = $"{Math.Round(width)}mm";
                excutionForm.SectionLabelUpdate($"Plate <<< {section} >>> Updated ", color);
            }

            part.SetUserProperty("SectionSize", section);
            excutionForm.SectionLabelUpdate($"Part Section <<< {section} >>> Updated ", color);
        }
    }
}
