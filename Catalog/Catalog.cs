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