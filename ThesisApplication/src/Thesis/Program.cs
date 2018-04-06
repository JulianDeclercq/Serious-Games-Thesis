﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Thesis
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create the forms
            WordNetForm _wordNetForm = new WordNetForm();
            CeptreForm _ceptreForm = new CeptreForm();

            // Run the ceptreform
            List<UserControl> userControls = new List<UserControl>() { _ceptreForm, _wordNetForm };
            DisplayForm displayForm = new DisplayForm(userControls);

            // Set the references so the usercontrols can be switched from inside them
            _ceptreForm.SetDisplayFormReference(displayForm);

            Application.Run(displayForm);
        }
    }
}