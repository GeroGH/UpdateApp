using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberingModProposalA
    {
        private const string ProjectPrefix = "U";
        private const string ProfilePrefix = "P";
        private const string FittingPrefix = "F";

        public static void Modify(Part part)
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
            var phaseNumber = phase.PhaseNumber;

            var type = GetAssemblyType(mainPart);

            if (string.IsNullOrWhiteSpace(type))
                return;

            var profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            var isPrimaryPart = part.Equals(mainPart);
            var isProfile = profileType != "B";

            var assemblyPrefix = $"{ProjectPrefix}{phaseNumber}-{type}-";

            if (isPrimaryPart)
            {
                ApplyNumberingSeries(part, assemblyPrefix, assemblyPrefix);
                return;
            }

            if (isProfile)
            {
                ApplyNumberingSeries(part, $"{ProjectPrefix}{phaseNumber}-{ProfilePrefix}-", assemblyPrefix);
                return;
            }

            ApplyNumberingSeries(part, $"{ProjectPrefix}{phaseNumber}-{FittingPrefix}-", assemblyPrefix);
        }

        private static string GetAssemblyType(Part part)
        {
            var prefix = "A";
            var name = part.Name.ToUpper();

            switch (name)
            {
                case string n when n.Contains("PLATE") && n.Contains("GIRDER"): prefix = "PG"; break;
                case string n when n.Contains("CELLULAR") && n.Contains("BEAM"): prefix = "CB"; break;
                case string n when n.Contains("CAMBER") && n.Contains("BEAM"): prefix = "PCB"; break;
            }

            var isTemporary = name.Contains("TEMP") || name.Contains("TEMPORARY");

            if (isTemporary)
            {
                prefix = "T";
            }

            return prefix;
        }

        private static void ApplyNumberingSeries(Part part, string partPrefix, string assemblyPrefix)
        {
            part.PartNumber.Prefix = partPrefix;
            part.AssemblyNumber.Prefix = assemblyPrefix;
            part.PartNumber.StartNumber = 1;
            part.AssemblyNumber.StartNumber = 1;
            part.Modify();
        }
    }
}