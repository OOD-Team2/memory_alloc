using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Classes.Model;

namespace OODProject.Classes.MemoryAllocation
{
    class AllocStrategy : IMemoryAllocationStrategy
    {
        public AllocStrategy() { }

        public List<Process> avail = new List<Process>();

        public List<MemoryBlock> allocated = new List<MemoryBlock>();

        public void Initialize(int memSize)
        {
            MemorySize = memSize;

            for (int i = 0; i < MemorySize; i++)
            {
                Memory.Add(new MemoryBlock()
                {
                    ProcessId = 0,
                    IsAssigned = false,
                    IsEnd = false,
                    IsStart = false
                });
            }

            MemoryInitEventArgs arg = new MemoryInitEventArgs
            {
                MemoryAlgorithmName = "First Fit",
                NumberOfBlocks = MemorySize,
                Memory = this.Memory
            };

            OnInitialize(arg);
        }


        public override bool AllocateProcess(Process proc, out ProcessAllocateEventArgs arg)
        {
            MemoryBlock b, newBlock;
            

        }

        public override bool DeAllocateProcess(Process proc, out ProcessDeAllocateEventArgs arg)
        {

            bool isEnd = false;
            int startIndex = 0;
            int blockLength = 0;
            for (int i = 0; i < MemorySize; i++)
            {
                if (Memory[i].ProcessId == proc.ID && Memory[i].IsStart == true)
                {
                    startIndex = i;
                }

                if (Memory[i].ProcessId == proc.ID && Memory[i].IsEnd == true)
                {
                    blockLength = i - startIndex;

                    isEnd = true;
                }

                if (Memory[i].ProcessId == proc.ID)
                {
                    Memory[i].IsAssigned = false;
                    Memory[i].IsEnd = false;
                    Memory[i].IsStart = false;
                    Memory[i].ProcessId = 0;
                }

                if (isEnd) break;
            }

            for (int i = Processes.Count - 1; i > 0; i--)
            {
                if (Processes[i].ID == proc.ID)
                    Processes.RemoveAt(i);
            }
            arg = new ProcessDeAllocateEventArgs { ProcessID = proc.ID, ProcessName = proc.Name, StartBlock = startIndex, BlockLength = blockLength };

            return true;
        }
    }
}
