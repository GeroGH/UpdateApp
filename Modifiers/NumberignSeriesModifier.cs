using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberignSeriesModifier
    {
        internal static void Modify(Part part)
        {
            part.GetAssembly().GetMainPart().GetPhase(out var mainPartPhase);
            var phaseComment = mainPartPhase.PhaseComment;
            var startPos = phaseComment.IndexOf("[");
            var endPos = phaseComment.IndexOf("]");

            if (startPos == -1 || endPos == -1)
            {
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

            var assemblyNumber = mainPartPhase.PhaseNumber * 100 + 1;
            part.AssemblyNumber.StartNumber = assemblyNumber;

            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;

            var assemblyPrefix = mainPart.AssemblyNumber.Prefix;
            part.AssemblyNumber.Prefix = assemblyPrefix;
            part.PartNumber.Prefix = prefix;
        }
    }
}
