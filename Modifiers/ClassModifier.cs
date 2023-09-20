using System.Drawing;
using Tekla.Structures.Model;

namespace UpdateApp
{
    class ClassModifier
    {
        internal static void Modify(Part part, ExecutionForm excutionForm, Color color)
        {
            var ass = part.GetAssembly();
            var mainPart = ass.GetMainPart() as Part;

            part.Class = mainPart.Class;
            part.Modify();

            excutionForm.ClassLabelUpdate($"Part Class No. <<< {part.Class} >>> Updated ", color);
        }
    }
}
