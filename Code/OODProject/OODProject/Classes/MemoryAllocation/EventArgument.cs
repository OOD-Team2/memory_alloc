using OODProject.Classes.Model;
using System;
using System.Collections.Generic;

namespace OODProject.Classes.MemoryAllocation
{
    public delegate void OnMemoryInitialize(MemoryInitEventArgs arg);
        
    public delegate void OnProcessAllocate(ProcessAllocateEventArgs arg);
    public delegate void OnProcessDeAllocate(ProcessDeAllocateEventArgs arg);

    public class MemoryInitEventArgs : EventArgs
    {
        public int NumberOfBlocks { get; set; }
        public List<MemoryBlock> Memory { get; set; }
    }

    public class ProcessAllocateEventArgs : EventArgs
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int StartBlock { get; set; }
        public int BlockLength { get; set; }
    }

    public class ProcessDeAllocateEventArgs : EventArgs
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int StartBlock { get; set; }
        public int BlockLength { get; set; }
    }
}