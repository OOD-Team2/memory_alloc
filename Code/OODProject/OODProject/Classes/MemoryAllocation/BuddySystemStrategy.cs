using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Classes.Model;

namespace OODProject.Classes.MemoryAllocation
{
    class BuddySystemStrategy : IMemoryAllocationStrategy
    {        
        public BuddySystemStrategy(){
            DataModel = new ProjectModel();
        }

        public ProjectModel DataModel { get ; set ; }

        public bool AllocateProcess(Process objProcess)
        {
            throw new NotImplementedException();
        }

        public bool DeAllocateProcess(Process objProcess)
        {
            throw new NotImplementedException();
        }
    }
}
