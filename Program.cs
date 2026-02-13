using System;
using System.Drawing;
using System.Windows.Forms;
using Tekla.Structures.Model;

namespace UpdateApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var excutionForm = new ExecutionForm();
            excutionForm.Show();

            var model = new Model();

            Catalog.CollectPartsFromTheModel();

            foreach (var part in Catalog.Parts)
            {
                PhaseModifier.Modify(part, excutionForm, Color.DarkMagenta);
                PrefixModifier.Modify(part, excutionForm, Color.DarkGreen);
                SectionModifier.Modify(part, excutionForm, Color.DarkCyan);
            }

            model.CommitChanges();

            Catalog.SelectPartsInTheModel();
        }
    }
}
