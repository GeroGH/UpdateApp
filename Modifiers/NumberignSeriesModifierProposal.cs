using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberingSeriesModifierProposal
    {
        public static void Modify(Part part, string type)
        {
            if (part == null)
                return;

            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;

            mainPart.GetPhase(out var phase);

            var project = "R";

            var phaseNumber = phase.PhaseNumber;

            var IsProfile = false;
            var IsPrimaryPart = false;

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            if (profileType != "B")
            {
                IsProfile = true;
            }

            if (part.Equals(part.GetAssembly().GetMainPart()))
            {
                IsPrimaryPart = true;
            }
            var assemblyPrefix = string.Empty;
            var partPrefix = string.Empty;

            if (IsProfile)
            {
                partPrefix = $"{project}{phaseNumber}-{"P"}-";
            }

            if (!IsProfile)
            {
                partPrefix = $"{project}{phaseNumber}-{"F"}-";
            }

            if (IsPrimaryPart)
            {
                partPrefix = $"{project}{phaseNumber}-{type}-";
                assemblyPrefix = $"{project}{phaseNumber}-{type}-";
            }

            part.PartNumber.Prefix = partPrefix;
            part.PartNumber.StartNumber = 1;

            part.AssemblyNumber.Prefix = assemblyPrefix;
            part.AssemblyNumber.StartNumber = 1;

            part.Modify();
        }
    }
}