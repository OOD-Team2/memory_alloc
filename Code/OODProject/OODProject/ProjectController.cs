using OODProject.Classes.MemoryAllocation;
using OODProject.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OODProject
{
    static class ProjectController
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
            strategy.OnInitialize += new OnMemoryInitialize(view.OnInitialize);
            strategy.OnAllocated += new OnProcessAllocate(view.OnAllocated);
            strategy.OnDeAllocated += new OnProcessDeAllocate(view.OnDeAllocated);
           
            strategy.Initialize(1024);

            view.Show();

            //start feeding data from ProcessFeederClass
            ProcessFeeder feeder = new ProcessFeeder();
            while (feeder.PeekNextProcess() != null)
            {
                Application.DoEvents();
                Thread.Sleep(1000);

                strategy.FeedProcess(feeder.GetNextProcess());                
            }

            Application.Run();
        }          
    }
}
