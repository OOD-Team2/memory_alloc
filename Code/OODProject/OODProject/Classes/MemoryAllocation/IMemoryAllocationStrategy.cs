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
        public OnMemoryModified OnModified;
        


        public bool FeedProcess(Process proc)
        {
            bool response = false;

            if (proc.Type == "Allocate")
            {
                response = AllocateProcess(proc);
            }else if (proc.Type == "DeAllocate")
            {
                response = DeAllocateProcess(proc);
            }

            if (response == true) {
                MemoryModifiedEventArgs arg = new MemoryModifiedEventArgs { Memory = this.Memory, Processes = this.Processes };

                OnModified(arg);
            }

            return false;
        }

        public abstract bool AllocateProcess(Process objProcess);
        public abstract bool DeAllocateProcess(Process objProcess);
    }
}
