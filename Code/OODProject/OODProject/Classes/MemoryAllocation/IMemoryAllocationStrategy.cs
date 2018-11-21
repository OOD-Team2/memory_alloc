using OODProject.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.MemoryAllocation
{
    public abstract class IMemoryAllocationStrategy
    {
        public int MemorySize { get; set; }

        //contains list of processes
        public List<Process> Processes = new List<Process>();

        //contains list of memory items
        public List<MemoryBlock> Memory = new List<MemoryBlock>();

        //delegates for handlers
        public OnMemoryInitialize OnInitialize;
        public OnProcessAllocate OnAllocated;
        public OnProcessDeAllocate OnDeAllocated;

        public bool FeedProcess(Process proc)
        {
            bool response = false;

            if (proc.Type == "Allocate")
            {
                ProcessAllocateEventArgs arg = new ProcessAllocateEventArgs();
                response = AllocateProcess(proc, out arg);

                if (response == true)
                {
                   OnAllocated(arg);
                }

            }
            else if (proc.Type == "DeAllocate")
            {
                ProcessDeAllocateEventArgs arg = new ProcessDeAllocateEventArgs();
                response = DeAllocateProcess(proc, out arg);

                if (response == true)
                    OnDeAllocated(arg);
            }           

            return false;
        }

        public abstract bool AllocateProcess(Process objProcess, out ProcessAllocateEventArgs arg);
        public abstract bool DeAllocateProcess(Process objProcess, out ProcessDeAllocateEventArgs arg);
    }
}
