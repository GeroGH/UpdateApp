using Tekla.Structures.Model;

namespace UpdateApp
{
    class NumberignModCurrent
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

            var assemblyNumber = 1;
            part.AssemblyNumber.StartNumber = assemblyNumber;

            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;

            var type = GetAssemblyType(mainPart);

            part.AssemblyNumber.Prefix = type;
            part.PartNumber.Prefix = prefix;
        }
        private static string GetAssemblyType(Part part)
        {
            var prefix = "Z";
            var name = part.Name.ToUpper();

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

            var isTemporary = name.Contains("TEMP") || name.Contains("TEMPORARY");

            if (isTemporary)
            {
                prefix = $"T{prefix}";
            }

            return prefix;
        }
    }
}
