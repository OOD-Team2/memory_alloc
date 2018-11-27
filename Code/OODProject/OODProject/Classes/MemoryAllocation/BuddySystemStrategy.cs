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
        public BuddySystemStrategy() {}

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
                MemoryAlgorithmName = "Buddy System",
                NumberOfBlocks = MemorySize,
                Memory = this.Memory
            };

            OnInitialize(arg);
        }


        public override bool AllocateProcess(Process proc, out ProcessAllocateEventArgs arg)
        {
            //Find the power of process size e.g 65K = 2 ^ 7
            int b = (int)Math.Ceiling(Math.Log(proc.MemoryInKB, 2));

            //Find length of the block to allocate. 2 ^ 7 = 128K
            int blockLength = (int)Math.Pow(2, b);

            //Define start and ending index of where the block wil be places in Memory
            int startIndex = 0, endIndex = 0;

            //Divide memory until find the right block
            for (int i = MemorySize; i >= 0; i = i / 2)
            {
                if (i == blockLength)
                {
                    //i - 1 because index start from 0 e.g. 128 will be 127
                    endIndex = i - 1;

                    //start index will start from 0
                    startIndex = i - blockLength;

                    //if any of the item between start and end index are assigned OR start index is not a multiple of block length 
                    //then keep incrementing start index
                    while ((Memory[startIndex].IsAssigned || Memory[endIndex].IsAssigned) || (startIndex > 1 && startIndex % blockLength != 0))
                    {
                        startIndex++;
                        endIndex = startIndex + blockLength - 1;

                        if (endIndex >= MemorySize) break;
                    }

                    //else quit the loop
                    break;
                }
            }

            //this condition means algo could not find a space big enough to allocate the block
            if (Memory[startIndex].IsAssigned || Memory[endIndex].IsAssigned)
            {
                //"No space available to allocate";
                arg = new ProcessAllocateEventArgs();
                return false;
            }

            //assigning all items between start and end index to the process
            for (int i = 0; i < MemorySize; i++)
            {
                if (i >= startIndex && i <= endIndex && Memory[i].IsAssigned == false)
                {
                    Memory[i].ProcessId = proc.ID;
                    Memory[i].IsAssigned = true;

                    if (i == startIndex)
                    {
                        Memory[i].IsStart = true;
                    }

                    if (i == endIndex)
                    {
                        Memory[i].IsEnd = true;
                        break;
                    }
                }
            }

            // display message about where block has been placed in the memory
            arg = new ProcessAllocateEventArgs();
            arg.ProcessID = proc.ID;
            arg.ProcessName = proc.Name;
            for (int k = 0; k < MemorySize; k++)
            {
                if (Memory[k].ProcessId == proc.ID && Memory[k].IsStart == true)
                {
                    arg.StartBlock = k;
                    arg.BlockLength = blockLength;
                }
                
            }

            Processes.Add(proc);

            return true;
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

            for(int i= Processes.Count -1; i >0; i--)
            {
                if (Processes[i].ID == proc.ID)
                    Processes.RemoveAt(i);
            }
            arg = new ProcessDeAllocateEventArgs { ProcessID = proc.ID, ProcessName = proc.Name, StartBlock = startIndex, BlockLength = blockLength };

            return true;
        }
    }
}
