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
    public class DrawObject {
        public string ProcessName { get; set; }
        public Rectangle Rectangle { get; set; }
    }

    public partial class ProjectView : Form, IMemoryView
    {
        List<DrawObject> drawObjects = new List<DrawObject>();

        public ProjectView()
        {
            InitializeComponent();
        }

        public void OnInitialize(MemoryInitEventArgs arg)
        {
            lblMemorySize.Text = arg.NumberOfBlocks.ToString();
            //RefreshMemoryList(arg.Memory);
            lstProcesses.DisplayMember = "Name";

            Application.DoEvents();
        }

        public void OnAllocated(ProcessAllocateEventArgs arg)
        {
            lstProcesses.Items.Add(new Process { ID = arg.ProcessID, Name = arg.ProcessName });
            drawObjects.Add(new DrawObject { ProcessName = arg.ProcessName, Rectangle = new Rectangle(arg.StartBlock, 0, arg.BlockLength - 1, 20) });
            panel1.Invalidate();

            Application.DoEvents();
        }

        public void OnDeAllocated(ProcessDeAllocateEventArgs arg)
        {
            for (int j = lstProcesses.Items.Count -1; j >= 0; j--)
            {
                if (((Process)lstProcesses.Items[j]).Name == arg.ProcessName)
                {
                    lstProcesses.Items.RemoveAt(j);
                }
            }

            for(int i =drawObjects.Count -1; i >=0; i--)
            {
                if (drawObjects[i].Rectangle.X == arg.StartBlock)
                    drawObjects.RemoveAt(i);
            }
            
            panel1.Invalidate();

            Application.DoEvents();
        }

        private void RefreshProcessList(List<Process> processes)
        {
            lstProcesses.Items.Clear();
            processes.ForEach(o => lstProcesses.Items.Add(o.Name));
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Font fontArial = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);

            foreach (var drawObject in this.drawObjects)
            {                
                g.FillRectangle(myBrush, drawObject.Rectangle);
                g.DrawString("  " + drawObject.ProcessName, fontArial, Brushes.White, drawObject.Rectangle);
            }
        }
    }
}
