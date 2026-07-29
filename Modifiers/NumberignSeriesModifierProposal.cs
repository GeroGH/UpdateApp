using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberingSeriesModifierProposal
    {
        private const string ProfilePrefix = "P";
        private const string FittingPrefix = "F";

        public static void Modify(Part part, string projectPrefix)
        {
            var type = GetAssemblyType(part);

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

        private static string GetAssemblyType(Part part)
        {
            var assembly = part.GetAssembly();
            var mainPart = assembly.GetMainPart() as Part;
            var name = mainPart.Name.ToUpper();

            switch (name)
            {
                // Specific names first
                case string n when n.Contains("LOOSE") && n.Contains("PLATE"): return "LP";
                case string n when n.Contains("SPLICE") && n.Contains("PLATE"): return "SP";
                case string n when n.Contains("FINGER") && n.Contains("PACK"): return "FP";
                case string n when n.Contains("SHOULDER") && n.Contains("BOLT"): return "SB";
                case string n when n.Contains("GRATING") || n.Contains("GREATING"): return "GR";
                case string n when n.Contains("SHIM"): return "SH";
                case string n when n.Contains("HANGER"): return "HG";

                // Assembly families
                case string n when n.Contains("STAIR"): return "ST";
                case string n when n.Contains("FRAME"): return "FR";

                // Company specific members
                case string n when n.Contains("FABRICATED") && n.Contains("BEAM"): return "FB";
                case string n when n.Contains("CELLULAR") && n.Contains("BEAM"): return "CB";
                case string n when n.Contains("STUDDED") && n.Contains("BEAM"): return "SB";

                // Hollow sections
                case string n when n.Contains("HOLLOW"): return "H";
                case string n when n.Contains("SHS"): return "H";
                case string n when n.Contains("RHS"): return "H";
                case string n when n.Contains("CHS"): return "H";

                // General structural types
                case string n when n.Contains("COLUMN"): return "C";
                case string n when n.Contains("POST"): return "N";
                case string n when n.Contains("TRIMMER"): return "J";
                case string n when n.Contains("BEAM"): return "B";
                case string n when n.Contains("BRACE"): return "X";
                case string n when n.Contains("TRUSS"): return "TR";
                case string n when n.Contains("RAFTER"): return "R";
                case string n when n.Contains("GIRDER"): return "G";
                case string n when n.Contains("CHANNEL"): return "U";
                case string n when n.Contains("ANGLE"): return "E";
                case string n when n.Contains("BRACKET"): return "A";

                default: return "Z";
            }
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