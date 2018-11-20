using OODProject.Classes.MemoryAllocation;
using OODProject.Classes.Model;
using OODProject.Classes.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OODProject
{
    public partial class ProjectView : Form, IMemoryView
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        public void OnInitialize(MemoryInitEventArgs arg)
        {
            lblMemorySize.Text = arg.NumberOfBlocks.ToString();
            RefreshMemoryList(arg.Memory);
        }

        public void OnModified(MemoryModifiedEventArgs arg)
        {
            RefreshMemoryList(arg.Memory);
        }

        private void RefreshMemoryList(List<MemoryBlock> memory)
        {
            lstMemory.Items.Clear();
            foreach(MemoryBlock mem in memory)
            {
                ListViewItem lv = new ListViewItem(new[] { mem.ProcessId.ToString()});
                lstMemory.Items.Add(lv);
            }
            Application.DoEvents();
        }

        private void ProjectView_Load(object sender, EventArgs e)
        {

        }
    }
}
