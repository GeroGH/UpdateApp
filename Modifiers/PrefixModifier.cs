using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class PrefixModifier
    {
        internal static void Modify(Part part, ExecutionForm excutionForm, Color color)
        {
            part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            var phaseComment = mainPartPhase.PhaseComment;
            var startPos = phaseComment.IndexOf("[");
            var endPos = phaseComment.IndexOf("]");

            if (startPos == -1 || endPos == -1)
            {
                excutionForm.PrefixLabelUpdate($"Part Prefix Not Found", color);
                return;
            }

            var prefix = phaseComment.Substring(startPos + 1, endPos - startPos - 1);

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            var IsProfile = false;
            var IsPrimaryPart = false;
            var IsMachined = false;

            if (profileType != "B")
            {
                IsProfile = true;
            }

            if (part.Equals(part.GetAssembly().GetMainPart()))
            {
                IsPrimaryPart = true;
            }

            if (part.Name.ToUpper().Contains("PREP") ||
                part.Name.ToUpper().Contains("BEARING") ||
                part.Name.ToUpper().Contains("MACHINED"))
            {
                IsMachined = true;
            }

            if (IsPrimaryPart)
            {
                prefix = $"PP{prefix.Remove(0, 1)}";
            }

            if (!IsPrimaryPart)
            {
                if (IsProfile)
                {
                    prefix = $"P{prefix.Remove(0, 1)}";
                }
            }

            if (IsMachined)
            {
                prefix = $"{prefix}#";
            }

            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;
            var assemblyPrefix = mainPart.AssemblyNumber.Prefix;
            part.AssemblyNumber.Prefix = assemblyPrefix;
            part.PartNumber.Prefix = prefix;
            part.Modify();

            excutionForm.PrefixLabelUpdate($"Part and Assembly Prefix <<<[{prefix}] and [{assemblyPrefix}] >>> Updated ", color);

            //part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            //var phaseNumber = mainPartPhase.PhaseNumber;
            //var fittingAlocation = "X";
            //var prefix = string.Empty;

            //var profileType = string.Empty;
            //part.GetReportProperty("PROFILE_TYPE", ref profileType);

            //var IsProfile = false;
            //var IsPrimaryPart = false;

            //if (profileType != "B") { IsProfile = true; }

            //if (part.Equals(part.GetAssembly().GetMainPart())) { IsPrimaryPart = true; }

            //prefix = $"{fittingAlocation}{phaseNumber}F";

            //if (IsPrimaryPart)
            //{
            //    prefix = $"{fittingAlocation}{phaseNumber}M";
            //}

            //if (!IsPrimaryPart)
            //{
            //    if (IsProfile)
            //    {
            //        prefix = $"{fittingAlocation}{phaseNumber}P";
            //    }
            //}

            //part.PartNumber.Prefix = prefix;
            //part.Modify();
        }
    }
}
