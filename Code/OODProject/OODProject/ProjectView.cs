using OODProject.Classes.Controller;
using OODProject.Classes.Model;
using OODProject.Classes.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OODProject
{
    public partial class ProjectView : Form, IProjectView
    {
        ProjectController ViewController;

        public ProjectView()
        {
            InitializeComponent();
        }

        public ProjectModel DataModel { get ; set; }

        public void AddProcess(Process process)
        {
            ViewController.AllocateProcess(process);
        }

        public void RefreshView()
        {
            
        }

        public void RemoveProcess(Process process)
        {
            ViewController.DeAllocateProcess(process);
        }

        public void SetController(ProjectController controller)
        {
            ViewController = controller;
        }
    }
}
