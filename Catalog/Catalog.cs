using System.Collections;
using System.Collections.Generic;
using Tekla.Structures.Model;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace UpdateApp
{
    public static class Catalog
    {
        public static List<Part> Parts { get; set; }
        static Catalog()
        {
            Parts = new List<Part>();
        }
        public static void CollectPartsFromTheModel()
        {
            ModelObjectEnumerator.AutoFetch = true;
            var mos = new ModelObjectSelector();
            var moe = mos.GetSelectedObjects();
            while (moe.MoveNext())
            {
                var part = moe.Current as Part;
                if (part == null)
                {
                    continue;
                }

                var materialType = string.Empty;
                part.GetReportProperty("MATERIAL_TYPE", ref materialType);

                if (materialType != "STEEL")
                {
                    continue;
                }

                var name = string.Empty;
                part.GetReportProperty("NAME", ref name);

                name = name.ToUpper();

                if (name.Contains("HILTI"))
                {
                    continue;
                }

                if (name.Contains("HALFEN"))
                {
                    continue;
                }

                if (name.Contains("SHOULDER") && name.Contains("BOLT"))
                {
                    continue;
                }

                if (name.Contains("HD") && name.Contains("BOLT"))
                {
                    continue;
                }

                if (name.Contains("SHEAR") && name.Contains("STUD"))
                {
                    continue;
                }

                Parts.Add(part);
            }

        }
        public static void SelectPartsInTheModel()
        {
            var mos = new ModelObjectSelector();
            mos.Select(new ArrayList(Parts), false);
        }
    }
}