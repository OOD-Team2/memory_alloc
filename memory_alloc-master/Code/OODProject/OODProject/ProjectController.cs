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

            //Simulate Buddy System First
            viewBuddy.StartPosition = FormStartPosition.CenterParent;
            viewBuddy.Show();

            //Simulate Best Fit AFTER Buddy System completes
            viewBestFit.StartPosition = FormStartPosition.CenterScreen;
            viewBestFit.Show();

            //start feeding data from ProcessFeederClass to Buddy System
            ProcessFeeder feederBuddy = new ProcessFeeder();
            while (feederBuddy.PeekNextProcess() != null)
            {
                Application.DoEvents();
                Thread.Sleep(1000);

                Process proc = feederBuddy.GetNextProcess();

                strategyBuddy.FeedProcess(proc);
            }

            //start feeding data from ProcessFeederClass to Best Fit
            ProcessFeeder feederBestFit = new ProcessFeeder();
            while (feederBestFit.PeekNextProcess() != null)
            {
                Application.DoEvents();
                Thread.Sleep(1000);

                Process proc = feederBestFit.GetNextProcess();

                strategyBestFit.FeedProcess(proc);
            }

            Application.Run();
        }          
    }
}
