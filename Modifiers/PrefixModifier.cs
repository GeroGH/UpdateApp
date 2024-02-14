using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class PrefixModifier
    {
        internal static void Modify(Part part, ExecutionForm excutionForm, Color color)
        {
            part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            var prefix = mainPartPhase.PhaseComment;
            var startPos = prefix.IndexOf("[");
            var endPos = prefix.IndexOf("]");

            if (startPos == -1 || endPos == -1)
            {
                excutionForm.PrefixLabelUpdate($"Part Prefix Not Found", color);
                return;
            }

            prefix = prefix.Substring(startPos + 1, endPos - startPos - 1);

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            if (profileType == "B")
            {
                if (part.Equals(part.GetAssembly().GetMainPart()))
                {
                    prefix = $"{prefix}M";
                }
            }

            if (profileType != "B")
            {

                if (!part.Equals(part.GetAssembly().GetMainPart()))
                {
                    prefix = $"{prefix}P";
                }

                if (part.Equals(part.GetAssembly().GetMainPart()))
                {
                    prefix = $"{prefix}M";
                }
            }

            if (part.Name.ToUpper().Contains("PREP") || part.Name.ToUpper().Contains("BEARING") || part.Name.ToUpper().Contains("MACHINED"))
            {
                prefix = $"{prefix}#";
            }

            part.PartNumber.Prefix = prefix;
            part.Modify();

            excutionForm.PrefixLabelUpdate($"Part Prefix <<< {prefix} >>> Updated ", color);
        }
    }
}
