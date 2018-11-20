using OODProject.Classes.Model;
using System;
using System.Collections.Generic;

namespace OODProject.Classes.MemoryAllocation
{
    public delegate void OnMemoryInitialize(MemoryInitEventArgs arg);

    public delegate void OnMemoryModified(MemoryModifiedEventArgs arg);

    public class MemoryInitEventArgs : EventArgs
    {
        public int NumberOfBlocks { get; set; }
        public List<MemoryBlock> Memory { get; set; }
    }

    public class MemoryModifiedEventArgs : EventArgs
    {
        public List<MemoryBlock> Memory { get; set; }
        public List<Process> Processes { get; set; }
    }
}