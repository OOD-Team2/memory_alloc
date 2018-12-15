using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Classes.MemoryAllocation;

namespace OODProject.Classes.Model
{
    public class ProcessFeeder
    {
        Queue<Process> ProcessList { get; set; }

        public ProcessFeeder()
        {
            ProcessList = new Queue<Process>();

            ProcessList.Enqueue(new Process { ID = 1, Name = "A", MemoryInKB = 34, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 2, Name = "B", MemoryInKB = 108, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 3, Name = "C", MemoryInKB = 400, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 4, Name = "D", MemoryInKB = 67, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 2, Name = "B", MemoryInKB = 108, Type = "DeAllocate" });
            ProcessList.Enqueue(new Process { ID = 4, Name = "D", MemoryInKB = 67, Type = "DeAllocate" });
            ProcessList.Enqueue(new Process { ID = 5, Name = "E", MemoryInKB = 34, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 6, Name = "F", MemoryInKB = 108, Type = "Allocate" });
            ProcessList.Enqueue(new Process { ID = 1, Name = "A", MemoryInKB = 34, Type = "DeAllocate" });
            ProcessList.Enqueue(new Process { ID = 3, Name = "C", MemoryInKB = 400, Type = "DeAllocate" });
        }

        public Process GetNextProcess()
        {
            
            if (ProcessList.Count > 0) {
                return ProcessList.Dequeue();
            }

            return null;
        }

        public Process PeekNextProcess()
        {
            if (ProcessList.Count > 0 )
                return ProcessList.Peek();
            else
                return null;
        }
    }
}
