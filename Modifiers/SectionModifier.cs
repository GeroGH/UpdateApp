using System;
using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class SectionModifier
    {
        internal static void Update(Part part, ExecutionForm excutionForm, Color color)
        {
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
