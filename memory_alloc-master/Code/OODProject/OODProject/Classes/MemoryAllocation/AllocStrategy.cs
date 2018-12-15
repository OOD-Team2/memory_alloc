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

        override
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

            BlockFit firstblock = new BlockFit();
            firstblock.start_pos = 0;
            firstblock.ID = 0;
            firstblock.blockLength = MemorySize;
            avail.Add(firstblock);


            OnInitialize(arg);
        }

        public override bool AllocateProcess(Process proc, out ProcessAllocateEventArgs arg)
        {
            // Best fit algorithm from Bergmann's class
            BlockFit b, newSpace;
            List<BlockFit> viableSpaces = new List<BlockFit>();

            int size = proc.MemoryInKB;

            newSpace = new BlockFit();
            int start_position = 0;

            // adds all possible fitting blocks to list viableSpaces
            for (int i = 0; i < avail.Count; i++)
            {
                b = avail[i];
                if (b.blockLength >= size)
                {
                    viableSpaces.Add(b);
                }
            }

            // orders the blocks so index 0 is the best fit
            List<BlockFit> ordered = viableSpaces.OrderBy(f => f.blockLength).ToList();

            // modifies the allocated and available lists for adding this new memory block
            if (viableSpaces.Count != 0)
            {
                for (int i = 0; i < viableSpaces.Count; i++)
                {
                    BlockFit block_check = viableSpaces[i];
                    if (block_check.blockLength == ordered[0].blockLength)
                    {
                        // creates a new allocation block
                        newSpace.start_pos = block_check.start_pos;
                        newSpace.blockLength = size;
                        newSpace.ID = proc.ID;
                        allocated.Add(newSpace);

                        //updates the available block
                        block_check.start_pos = block_check.start_pos + size - 1;
                        block_check.blockLength = block_check.blockLength - size;

                        //updates for the start position for the display
                        start_position = newSpace.start_pos;
                    }
                }
            }

            

            // places into memory location
            arg = new ProcessAllocateEventArgs { ProcessID = proc.ID, ProcessName = proc.Name, BlockLength = size, StartBlock = start_position};

            Processes.Add(proc);

            return true;

        }

        public override bool DeAllocateProcess(Process proc, out ProcessDeAllocateEventArgs arg)
        {
            
            // modifies memory
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

            // finds and removes the target process 
            for (int i = Processes.Count - 1; i > 0; i--)
            {
                if (Processes[i].ID == proc.ID)
                    Processes.RemoveAt(i);
            }

            // converts the allocated data to available data
            BlockFit b;
            for (int i = 0; i < allocated.Count; i++)
            {
                b = allocated[i];
                if (b.ID == proc.ID)
                {
                    allocated.Remove(allocated[i]);
                    for (int j = 0; j < avail.Count; j++)
                    {
                        // if the newly available blocks are next to already existing available blocks, merge them
                        BlockFit bl = avail[j];
                        if (b.start_pos + b.blockLength - 1 == bl.start_pos)
                        {
                            b = new BlockFit() { ID = bl.ID, blockLength = b.blockLength + bl.blockLength, start_pos = b.start_pos };
                            avail.Remove(avail[j]);

                        }
                        if (bl.start_pos + bl.blockLength - 1 == b.start_pos)
                        {
                            b = new BlockFit() { blockLength = b.blockLength + bl.blockLength, start_pos = bl.start_pos };
                            avail.Remove(avail[j]);
                        }
                    }
                    avail.Add(b);

                    arg = new ProcessDeAllocateEventArgs { ProcessID = proc.ID, ProcessName = proc.Name, StartBlock = b.start_pos, BlockLength = b.blockLength };

                    return true;
                }
            }
            arg = null;
            return false;
        }
    }

    // object for the available/allocated lists
    public class BlockFit
    {
        public Int32 start_pos { get; set; }
        public Int32 blockLength { get; set; }
        public Int32 ID { get; set; }
    }
}
