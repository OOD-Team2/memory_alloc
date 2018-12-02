using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.Model
{
    public class MemoryBlock
    {
        public Int32 ProcessId { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsAssigned { get; set; }
    }
}
