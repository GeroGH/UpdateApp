﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Tekla.Structures.Model;

namespace UpdateApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var excutionForm = new ExecutionForm();
            excutionForm.Show();

            var model = new Model();

            excutionForm.UpdateCentralLabel("Collecting Parts From The Model", Color.Black);
            Catalog.CollectPartsFromTheModel();

            foreach (var part in Catalog.Parts)
            {
                ClassModifier.Modify(part, excutionForm, Color.Firebrick);
                PhaseModifier.Modify(part, excutionForm, Color.DarkMagenta);
                PrefixModifier.Modify(part, excutionForm, Color.DarkGreen);
                SectionModifier.Modify(part, excutionForm, Color.DodgerBlue);
            }

            model.CommitChanges();

            excutionForm.UpdateCentralLabel("Selecting Modified Parts In The Model", Color.Black);
            Catalog.SelectPartsInTheModel();

            excutionForm.UpdateCentralLabel("Update Completed", Color.Black);
        }
    }
}
