namespace OODProject
{
    partial class ProjectView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstProcesses = new System.Windows.Forms.ListBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lblMemorySize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lstProcesses
            // 
            this.lstProcesses.FormattingEnabled = true;
            this.lstProcesses.Location = new System.Drawing.Point(12, 77);
            this.lstProcesses.Name = "lstProcesses";
            this.lstProcesses.Size = new System.Drawing.Size(120, 95);
            this.lstProcesses.TabIndex = 0;
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(12, 61);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(68, 13);
            this.lblProcess.TabIndex = 1;
            this.lblProcess.Text = "Processes : -";
            // 
            // lblMemorySize
            // 
            this.lblMemorySize.AutoSize = true;
            this.lblMemorySize.Location = new System.Drawing.Point(283, 9);
            this.lblMemorySize.Name = "lblMemorySize";
            this.lblMemorySize.Size = new System.Drawing.Size(74, 13);
            this.lblMemorySize.TabIndex = 5;
            this.lblMemorySize.Text = "lblMemorySize";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Memory Size: -";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Green;
            this.panel1.Location = new System.Drawing.Point(204, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 20);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 647);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMemorySize);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.lstProcesses);
            this.Name = "ProjectView";
            this.Text = "Memory Viewer";            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstProcesses;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Label lblMemorySize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}

