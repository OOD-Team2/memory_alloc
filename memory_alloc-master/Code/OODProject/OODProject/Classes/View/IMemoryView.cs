using OODProject.Classes.MemoryAllocation;
using OODProject.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Classes.View
{
    public interface IMemoryView
    {

        void OnInitialize(MemoryInitEventArgs arg);
    }
}
