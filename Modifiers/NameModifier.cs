using Tekla.Structures.Model;

namespace UpdateApp
{
    internal class NameModifier
    {
        internal static void Modify(Part part)
        {
            var assembly = part.GetAssembly();
            if (assembly == null)
                return;

            var mainPart = assembly.GetMainPart() as Part;
            if (mainPart == null)
                return;

            var name = string.Empty;
            var comment = string.Empty;

            mainPart.GetReportProperty("NAME", ref name);
            mainPart.GetReportProperty("comment", ref comment);

            part.Name = name;
            part.SetUserProperty("comment", comment);

            part.Modify();
        }
    }
}