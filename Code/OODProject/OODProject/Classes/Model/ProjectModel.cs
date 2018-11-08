using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.Model
{
    public class ProjectModel
    {
        List<Process> CurrentProcess { get; set; }
        List<MemoryBlock> InternalMemory { get; set; }
    }
}
