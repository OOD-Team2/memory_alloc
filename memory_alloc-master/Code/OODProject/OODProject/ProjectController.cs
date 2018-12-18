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

            // ProjectView - Best Fit
            ProjectView viewBestFit = new ProjectView();
            viewBestFit.Visible = false;

            // ProjectView - Buddy System
            ProjectView viewBuddy = new ProjectView();
            viewBuddy.Visible = false;

            //Buddy Sytem
            IMemoryAllocationStrategy strategyBuddy = new BuddySystemStrategy();
            strategyBuddy.OnInitialize += new OnMemoryInitialize(viewBuddy.OnInitialize);
            strategyBuddy.OnAllocated += new OnProcessAllocate(viewBuddy.OnAllocated);
            strategyBuddy.OnDeAllocated += new OnProcessDeAllocate(viewBuddy.OnDeAllocated);

            strategyBuddy.Initialize(1024);


            //Best Fit
            IMemoryAllocationStrategy strategyBestFit = new AllocStrategy();
            strategyBestFit.OnInitialize += new OnMemoryInitialize(viewBestFit.OnInitialize);
            strategyBestFit.OnAllocated += new OnProcessAllocate(viewBestFit.OnAllocated);
            strategyBestFit.OnDeAllocated += new OnProcessDeAllocate(viewBestFit.OnDeAllocated);

            strategyBestFit.Initialize(1024);

            //Simulate Best Fit & Buddy System same time
            viewBestFit.Show();
            viewBuddy.Show();

            //start feeding data from ProcessFeederClass
            ProcessFeeder feeder = new ProcessFeeder();
            while (feeder.PeekNextProcess() != null)
            {
                Application.DoEvents();
                Thread.Sleep(1000);

                strategyBestFit.FeedProcess(feeder.GetNextProcess());
                strategyBuddy.FeedProcess(feeder.GetNextProcess());
            }

            Application.Run();
        }          
    }
}
