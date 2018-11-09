using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Classes.MemoryAllocation;

namespace OODProject.Classes.Model
{
    public class ProjectModel
    {
        List<Process> ProcessList { get; set; }
        List<MemoryBlock> MemoryBlocks { get; set; }

        //
        // will need to be changed based on what we are doing per size
        const int totalSize = 0;
        BuddySystemStrategy util = new BuddySystemStrategy();

        public ProjectModel()
        {
        }

        public void Start()
        {
            GetProcesses();
            FeedProcesses();
        }

        public int getSize()
        {
            return totalSize;
        }

        public List<Process> getProcessList()
        {
            return ProcessList;
        }

        /*
         * 
         * This version of the function is used if we are reading in a file.
         * in the index 0 is the ID of the process if needed (p1, p2, p3, etc)
         * and in the index 1 is the memory in kb.
         * this can obviously be changed
         * - Chris A
         * 
         * */
        void GetProcesses()
        {
            string[] file = File.readAllLines();
            foreach (String line in file)
            {
                string[] l = line.Split('\t');
                if (l[0] == "Id") continue;

                Process obj = new Process();
                obj.ID = Convert.ToInt32(l[0]);
                obj.MemoryInKB = Convert.ToInt32(l[1]);
                ProcessList.Add(obj);
            }
        }

        void FeedProcesses()
        {
            foreach (var process in ProcessList)
            {
                if (process.Type == "Request")
                {
                    util.AllocateProcess(process);
                } else if (process.Type == Release)
            }
        }


    }
}
