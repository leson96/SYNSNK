using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPSYNCSNK
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
            Application.Run(new SYNC_SNK());

            Form FormName = new Form();

            FormName.ShowInTaskbar = false;
            FormName.Visible = false;
            FormName.Opacity = 0;
            FormName.Show();
            FormName.Hide();
        }
    }
}
