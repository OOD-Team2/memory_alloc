using OODProject.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.MemoryAllocation
{
    public interface IMemoryAllocationStrategy
    {
        ProjectModel DataModel { get; set; }

        bool AllocateProcess(Process objProcess);
        bool DeAllocateProcess(Process objProcess);
        ///////
    }
}
