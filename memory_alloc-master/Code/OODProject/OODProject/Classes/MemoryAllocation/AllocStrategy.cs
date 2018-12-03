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
        public AllocStrategy() {}

        public List<BlockFit> avail = new List<BlockFit>();

        public List<BlockFit> allocated = new List<BlockFit>();

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
                MemoryAlgorithmName = "Best Fit",
                NumberOfBlocks = MemorySize,
                Memory = this.Memory
            };

            OnInitialize(arg);
        }

        public override bool AllocateProcess(Process proc, out ProcessAllocateEventArgs arg)
        {
            // Best fit algorithm from Bergmann's class
            BlockFit b, newSpace;
            List<BlockFit> viableSpaces = new List<BlockFit>();

            newSpace = new BlockFit();
            int size = proc.MemoryInKB;
            
            // adds all possible fitting blocks to list viableSpaces
            for (int i = 0; i < avail.Count; i ++)
            {
                b = avail[i];
                if (b.MemoryInKB >= size)
                {
                    viableSpaces.Add(b);
                }
            }

            // orders the blocks so index 0 is the best fit
            List<BlockFit> ordered = viableSpaces.OrderBy(f => f.MemoryInKB).ToList();

            // modifies the allocated and available lists for adding this new memory block
            if (viableSpaces.Count != 0)
            {
                for (int i = 0; i < viableSpaces.Count; i++)
                {
                    BlockFit block_check = viableSpaces[i];
                    if (block_check.MemoryInKB == ordered[0].MemoryInKB)
                    {

                        newSpace.start_pos = block_check.start_pos;
                        newSpace.MemoryInKB = size;
                        newSpace.ID = proc.ID;
                        allocated.Add(newSpace);
                        block_check.start_pos = block_check.start_pos + size;
                        block_check.MemoryInKB = block_check.MemoryInKB - size;
                    
                    }
                }
            }

            // needs fixing
            arg = new ProcessAllocateEventArgs { ProcessID = proc.ID, ProcessName = proc.Name, BlockLength = proc.MemoryInKB };

            return true;

        }

        public override bool DeAllocateProcess(Process proc, out ProcessDeAllocateEventArgs arg)
        {
            
            // Handles external memory
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

            // Handles internal class memory
            BlockFit b;
            for (int i = 0; i < allocated.Count; i++)
            {
                b = allocated[i];
                if (b.ID == proc.ID)
                {
                    allocated.Remove(allocated[i]);
                    for (int j = 0; j < avail.Count; j++)
                    {
                        BlockFit bl = avail[j];
                        if (b.start_pos + b.MemoryInKB == bl.start_pos)
                        {
                            b = new BlockFit() { MemoryInKB = b.MemoryInKB + bl.MemoryInKB, start_pos = b.start_pos };
                            avail.Remove(avail[j]);

                        }
                        if (bl.start_pos + bl.MemoryInKB == b.start_pos)
                        {
                            b = new BlockFit() { MemoryInKB = b.MemoryInKB + bl.MemoryInKB, start_pos = bl.start_pos };
                            avail.Remove(avail[j]);
                        }
                    }
                    avail.Add(b);
                    return true;
                }
            }
            return false;
        }
    }

    public class BlockFit
    {
        public Int32 start_pos { get; set; }
        public Int32 MemoryInKB { get; set; }
        public Int32 ID { get; set; }
    }
}
