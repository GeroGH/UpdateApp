using Tekla.Structures.Model;

namespace UpdateApp
{
    class ClassModifier
    {
        internal static void Modify(Part part)
        {
            var ass = part.GetAssembly();
            var mainPart = ass.GetMainPart() as Part;

            part.Class = mainPart.Class;
        }
    }
}
