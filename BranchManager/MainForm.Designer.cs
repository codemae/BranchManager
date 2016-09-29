namespace BranchManager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.branchList = new System.Windows.Forms.ListView();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.localToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // branchList
            // 
            this.branchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.branchList.ContextMenuStrip = this.ContextMenu;
            this.branchList.FullRowSelect = true;
            this.branchList.Location = new System.Drawing.Point(12, 12);
            this.branchList.MultiSelect = false;
            this.branchList.Name = "branchList";
            this.branchList.ShowItemToolTips = true;
            this.branchList.Size = new System.Drawing.Size(591, 238);
            this.branchList.TabIndex = 1;
            this.branchList.UseCompatibleStateImageBehavior = false;
            this.branchList.View = System.Windows.Forms.View.Details;
            this.branchList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.branchList_MouseDoubleClick);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(125, 30);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remoteToolStripMenuItem1,
            this.localToolStripMenuItem1,
            this.bothToolStripMenuItem});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // remoteToolStripMenuItem1
            // 
            this.remoteToolStripMenuItem1.Name = "remoteToolStripMenuItem1";
            this.remoteToolStripMenuItem1.Size = new System.Drawing.Size(152, 26);
            this.remoteToolStripMenuItem1.Text = "Remote";
            this.remoteToolStripMenuItem1.Click += new System.EventHandler(this.remoteToolStripMenuItem_Click);
            // 
            // localToolStripMenuItem1
            // 
            this.localToolStripMenuItem1.Name = "localToolStripMenuItem1";
            this.localToolStripMenuItem1.Size = new System.Drawing.Size(152, 26);
            this.localToolStripMenuItem1.Text = "Local";
            // 
            // bothToolStripMenuItem
            // 
            this.bothToolStripMenuItem.Name = "bothToolStripMenuItem";
            this.bothToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.bothToolStripMenuItem.Text = "Both";
            this.bothToolStripMenuItem.Click += new System.EventHandler(this.bothToolStripMenuItem_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(13, 256);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(590, 148);
            this.txtOutput.TabIndex = 3;
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.allToolStripMenuItem.Text = "All";
            // 
            // remoteToolStripMenuItem
            // 
            this.remoteToolStripMenuItem.Name = "remoteToolStripMenuItem";
            this.remoteToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.remoteToolStripMenuItem.Text = "Remote";
            // 
            // localToolStripMenuItem
            // 
            this.localToolStripMenuItem.Name = "localToolStripMenuItem";
            this.localToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.localToolStripMenuItem.Text = "Local";
            this.localToolStripMenuItem.Click += new System.EventHandler(this.localToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 416);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.branchList);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView branchList;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ToolStripMenuItem remoteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem localToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localToolStripMenuItem;
    }
}

