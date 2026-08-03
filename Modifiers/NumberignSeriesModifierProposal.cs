using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberingSeriesModifierProposal
    {
        private const string ProfilePrefix = "P";
        private const string FittingPrefix = "F";

        public static void Modify(Part part, string projectPrefix)
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

        private static string GetAssemblyType(Part part)
        {
            var name = part.Name.ToUpper();
            var isTemporary = name.Contains("TEMP") || name.Contains("TEMPORARY");
            var prefix = "Z";

            switch (name)
            {
                // Specific names first
                case string n when n.Contains("LOOSE") && n.Contains("PLATE"): prefix = "LP"; break;
                case string n when n.Contains("SPLICE") && n.Contains("PLATE"): prefix = "SP"; break;
                case string n when n.Contains("FINGER") && n.Contains("PACK"): prefix = "FP"; break;
                case string n when n.Contains("SHOULDER") && n.Contains("BOLT"): prefix = "SB"; break;
                case string n when n.Contains("GRATING") || n.Contains("GREATING"): prefix = "GR"; break;
                case string n when n.Contains("SHIM"): prefix = "SH"; break;
                case string n when n.Contains("HANGER"): prefix = "HG"; break;

                // Assembly families
                case string n when n.Contains("STAIR"): prefix = "ST"; break;
                case string n when n.Contains("FRAME"): prefix = "FR"; break;

                // Company specific members
                case string n when n.Contains("FABRICATED") && n.Contains("BEAM"): prefix = "FB"; break;
                case string n when n.Contains("CELLULAR") && n.Contains("BEAM"): prefix = "CB"; break;
                case string n when n.Contains("STUDDED") && n.Contains("BEAM"): prefix = "SB"; break;

                // Hollow sections
                case string n when n.Contains("HOLLOW"): prefix = "H"; break;
                case string n when n.Contains("SHS"): prefix = "H"; break;
                case string n when n.Contains("RHS"): prefix = "H"; break;
                case string n when n.Contains("CHS"): prefix = "H"; break;

                // General structural types
                case string n when n.Contains("COLUMN"): prefix = "C"; break;
                case string n when n.Contains("POST"): prefix = "N"; break;
                case string n when n.Contains("TRIMMER"): prefix = "J"; break;
                case string n when n.Contains("BEAM"): prefix = "B"; break;
                case string n when n.Contains("BRACE"): prefix = "X"; break;
                case string n when n.Contains("TRUSS"): prefix = "TR"; break;
                case string n when n.Contains("RAFTER"): prefix = "R"; break;
                case string n when n.Contains("GIRDER"): prefix = "G"; break;
                case string n when n.Contains("CHANNEL"): prefix = "U"; break;
                case string n when n.Contains("ANGLE"): prefix = "E"; break;
                case string n when n.Contains("BRACKET"): prefix = "A"; break;
            }

            if (isTemporary)
            {
                prefix = $"T{prefix}";
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