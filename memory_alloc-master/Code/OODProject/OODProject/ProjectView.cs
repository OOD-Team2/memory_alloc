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
        List<DrawObject> drawObjects = new List<DrawObject>();

        public ProjectView()
        {
            InitializeComponent();
        }

        public void OnInitialize(MemoryInitEventArgs arg)
        {
            this.Text += " - " + arg.MemoryAlgorithmName;
            lblMemorySize.Text = arg.NumberOfBlocks.ToString();
            
            Application.DoEvents();
        }

        public void OnAllocated(ProcessAllocateEventArgs arg)
        {
            drawObjects.Add(new DrawObject { ProcessName = arg.ProcessName, Rectangle = new Rectangle(arg.StartBlock, 0, arg.BlockLength - 1, 55) });
            panel1.Invalidate();

            Application.DoEvents();
        }

        public void OnDeAllocated(ProcessDeAllocateEventArgs arg)
        {
            for(int i =drawObjects.Count -1; i >=0; i--)
            {
                if (drawObjects[i].Rectangle.X == arg.StartBlock)
                    drawObjects.RemoveAt(i);
            }
            
            panel1.Invalidate();

            Application.DoEvents();
        }

         private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Font fontArial = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);

            foreach (var drawObject in this.drawObjects)
            {                
                g.FillRectangle(myBrush, drawObject.Rectangle);
                g.DrawString("  " + drawObject.ProcessName, fontArial, Brushes.White, drawObject.Rectangle);
            }
        }

        private void ProjectView_Load(object sender, EventArgs e)
        {

        }
    }
}
