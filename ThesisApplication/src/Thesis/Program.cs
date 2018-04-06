using System;
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

            // Set the references
            _wordNetForm.SetCeptreFormReference(_ceptreForm);
            _ceptreForm.SetWordNetFormReference(_wordNetForm);

            // Run the ceptreform
            Application.Run(_ceptreForm);
        }
    }
}