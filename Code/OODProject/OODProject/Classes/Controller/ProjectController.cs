using OODProject.Classes.MemoryAllocation;
using OODProject.Classes.Model;
using OODProject.Classes.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.Controller
{
    public class ProjectController
    {
        IProjectView DisplayView;
        IMemoryAllocationStrategy MemoryStrategy;        

        public ProjectController(IProjectView view, IMemoryAllocationStrategy strategy)
        {
            DisplayView = view;
            MemoryStrategy = strategy;

            DisplayView.SetController(this);
        }

        public void AllocateProcess(Process process)
        {
            MemoryStrategy.AllocateProcess(process);
            DisplayView.DataModel = MemoryStrategy.DataModel;
            DisplayView.RefreshView();
        }

        public void DeAllocateProcess(Process process)
        {
            MemoryStrategy.DeAllocateProcess(process);
            DisplayView.DataModel = MemoryStrategy.DataModel;
            DisplayView.RefreshView();
        }

        public void LoadView()
        {
            DisplayView.RefreshView();
        }
    }
}
