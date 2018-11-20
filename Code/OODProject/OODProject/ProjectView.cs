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
        List<Rectangle> _rectangles = new List<Rectangle>();

        public ProjectView()
        {
            InitializeComponent();
        }

        public void OnInitialize(MemoryInitEventArgs arg)
        {
            lblMemorySize.Text = arg.NumberOfBlocks.ToString();
            RefreshMemoryList(arg.Memory);

            Application.DoEvents();
        }

        public void OnModified(MemoryModifiedEventArgs arg)
        {
            RefreshMemoryList(arg.Memory);
            RefreshProcessList(arg.Processes);

            Application.DoEvents();
        }

        private void RefreshProcessList(List<Process> processes)
        {
            lstProcesses.Items.Clear();
            processes.ForEach(o => lstProcesses.Items.Add(o.Name));
        }

        private void RefreshMemoryList(List<MemoryBlock> memory)
        {
            
            int x = 0;
            int width = 0;
          
            _rectangles.Clear();
            for(int i= 0; i <memory.Count; i++)
            {
                if (memory[i].IsAssigned)
                {
                    if (memory[i].IsStart == true)
                    {
                        x = i;
                        width = 0;
                    }

                    if (memory[i].IsEnd == true)
                    {
                        width = i - x;

                        _rectangles.Add(new Rectangle(x, 0, width, 20));
                    }
                }
            }
            panel1.Invalidate();

            
        }

        private void ProjectView_Load(object sender, EventArgs e)
        {

        }

        private void lstMemory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.Red);

            foreach (var rectangle in this._rectangles)
            {
                g.FillRectangle(myBrush, rectangle);
            }
        }
    }
}
