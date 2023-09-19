using System;
using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class PrefixModifier
    {
        internal static void Update(Part part, ExecutionForm excutionForm, Color color)
        {
            part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            part.SetPhase(mainPartPhase);
            var prefix = mainPartPhase.PhaseComment;

            var startPos = prefix.IndexOf("[");
            var endPos = prefix.IndexOf("]");

            if (startPos == -1 || endPos == -1)
            {
                excutionForm.PrefixLabelUpdate($"Part Prefix Not Found", color);
                return;
            }

            prefix = prefix.Substring(startPos + 1, endPos - startPos - 1);

            if (part.Name.ToUpper().Contains("PREP") || part.Name.ToUpper().Contains("BEARING") || part.Name.ToUpper().Contains("MACHINED"))
            {
                prefix = $"{prefix}#";
                excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix}# >>> Updated ", color);
            }

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            if (profileType == "B")
            {
                part.PartNumber.Prefix = prefix;
                excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix} >>> Updated ", color);
            }

            if (profileType != "B")
            {
                var length = 0.0;
                part.GetReportProperty("LENGTH", ref length);
                length = Math.Round(length);

                if (length > 2000)
                {
                    part.PartNumber.Prefix = $"{prefix}P";
                    excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix}P >>> Updated ", color);
                }

                if (length <= 2000)
                {
                    part.PartNumber.Prefix = $"{prefix}S";
                    excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix}S >>> Updated ", color);
                }
            }

            if (part.Equals(part.GetAssembly().GetMainPart()))
            {
                part.PartNumber.Prefix = $"{prefix}M";
                excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix}M >>> Updated ", color);
            }

            part.Modify();
        }
    }
}
