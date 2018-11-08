using OODProject.Classes.Controller;
using OODProject.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.View
{
    public interface IProjectView
    {
        ProjectModel DataModel { get; set; }

        void SetController(ProjectController controller);
        
        void AddProcess(Process process);
        void RemoveProcess(Process process);
        void RefreshView();
    }
}
