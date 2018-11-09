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
            MemoryModel = new MemoryBlock();
        }

        public ProjectModel DataModel { get ; set ; }

        public bool AllocateProcess(Process objProcess)
        {
            int b = (int)Math.Ceiling(Math.Log(objProcess.MemoryInKB, 2));
            int blockLength = (int)Math.Pow(2, b);
            int startIndex = 0, endIndex = 0;

            for (int i = DataModel.getSize(); i >= 0; i = i / 2)
            {
             
            }
        }

        public bool DeAllocateProcess(Process objProcess)
        {
            throw new NotImplementedException();
        }
    }
}
