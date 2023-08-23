using System;
using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class PrefixModifier
    {
        internal static void Update(Part part, ExecutionForm excutionForm, Color color)
        {
            var materialType = string.Empty;
            part.GetReportProperty("MATERIAL_TYPE", ref materialType);

            if (materialType != "STEEL")
            {
                excutionForm.PrefixLabelUpdate($"The Part Is Not Steel", color);
                return;
            }

            var name = string.Empty;
            part.GetReportProperty("NAME", ref name);

            if (name.Contains("HILTI"))
            {
                excutionForm.PrefixLabelUpdate($"The Part Is Hilti Anchor", color);
                return;
            }

            if (name.Contains("HALFEN"))
            {
                excutionForm.PrefixLabelUpdate($"The Part Is Halfen Bolt", color);
                return;
            }

            if (name.Contains("SHOULDER BOLT"))
            {
                excutionForm.PrefixLabelUpdate($"The Part Is ShoulderBolt", color);
                return;
            }

            if (name.Contains("STUD"))
            {
                excutionForm.PrefixLabelUpdate($"The Part Is Stud", color);
                return;
            }

            if (name.Contains("REINFORCEMENT"))
            {
                excutionForm.PrefixLabelUpdate($"The Part Is Reinforcement Bar", color);
                return;
            }

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
                    part.PartNumber.Prefix = $"P{prefix}";
                    excutionForm.PrefixLabelUpdate($"Part Prefix <<< P{prefix} >>> Updated ", color);
                }

                if (length <= 2000)
                {
                    part.PartNumber.Prefix = $"S{prefix}";
                    excutionForm.PrefixLabelUpdate($"Part Prefix <<< S{prefix} >>> Updated ", color);
                }
            }

            if (part.Equals(part.GetAssembly().GetMainPart()))
            {
                part.PartNumber.Prefix = $"M{prefix}";
                excutionForm.PrefixLabelUpdate($"Part Prefix <<< M{prefix} >>> Updated ", color);
            }

            part.Modify();
        }
    }
}
