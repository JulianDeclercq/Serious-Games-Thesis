using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thesis
{
    public partial class DisplayForm : Form
    {
        public enum DisplayMode
        {
            Ceptre,
            WordNet
        }

        public DisplayForm(List<UserControl> userControls)
        {
            // Initialize this component
            InitializeComponent();

            foreach (var control in userControls)
            {
                // Set the docstyles
                control.Dock = DockStyle.Fill;

                // Add to this form
                Controls.Add(control);

                // Hide by default
                control.Hide();
            }

            // Show the first user control on start
            userControls.First().Show();
        }

        public void UpdateDisplayMode(DisplayMode displayMode)
        {
            foreach (UserControl control in Controls)
            {
                if (control.Name.ToLower().Contains(displayMode.ToString().ToLower()))
                {
                    control.Show();
                }
                else
                {
                    control.Hide();
                }
            }
        }
    }
}