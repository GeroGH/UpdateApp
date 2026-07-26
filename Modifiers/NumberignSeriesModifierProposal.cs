using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberingSeriesModifierProposal
    {
        private const string ProfilePrefix = "P";
        private const string FittingPrefix = "F";

        public static void Modify(Part part, string projectPrefix,string type)
        {
            if (part == null)
                return;

            if (string.IsNullOrWhiteSpace(type))
                return;

            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;
                        
            mainPart.GetPhase(out var phase);
            var phaseNumber = phase.PhaseNumber;

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            var isPrimaryPart = part.Equals(mainPart);
            var isProfile = profileType != "B";

            var assemblyPrefix = $"{projectPrefix}{phaseNumber}-{type}-";

            if (isPrimaryPart)
            {
                ApplyNumberingSeries(part, assemblyPrefix, assemblyPrefix);
                return;
            }

            if (isProfile)
            {
                ApplyNumberingSeries(part, $"{projectPrefix}{phaseNumber}-{ProfilePrefix}-", assemblyPrefix);
                return;
            }

            ApplyNumberingSeries(part, $"{projectPrefix}{phaseNumber}-{FittingPrefix}-", assemblyPrefix);
        }

        private static void ApplyNumberingSeries(Part part, string partPrefix, string assemblyPrefix)
        {
            part.PartNumber.Prefix = partPrefix;
            part.AssemblyNumber.Prefix = assemblyPrefix;
            part.Modify();
        }
    }
}