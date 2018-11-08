using OODProject.Classes.Controller;
using OODProject.Classes.MemoryAllocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OODProject
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
            //Application.Run(new ProjectView());

            ProjectView view = new ProjectView();
            view.Visible = false;

            BuddySystemStrategy strategy = new BuddySystemStrategy();

            ProjectController controller = new ProjectController(view, strategy);
            controller.LoadView();
            view.ShowDialog();
        }
    }
}
