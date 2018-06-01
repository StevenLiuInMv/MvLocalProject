namespace MvLocalProject.Viewer
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bomCompareToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.diskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diskScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testProcessBarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testMvSecurityScanBoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mocP07ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mocP10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bomP09ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.報表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invP15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purP06W3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblResult = new System.Windows.Forms.Label();
            this.searchBomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolToolStripMenuItem,
            this.checkDiskToolStripMenuItem,
            this.reportToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 2;
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.erpToolStripMenuItem,
            this.diskToolStripMenuItem,
            this.testingToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // erpToolStripMenuItem
            // 
            this.erpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchBomToolStripMenuItem,
            this.bomCompareToolStripMenuItem2});
            this.erpToolStripMenuItem.Name = "erpToolStripMenuItem";
            this.erpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.erpToolStripMenuItem.Text = "ERP";
            // 
            // bomCompareToolStripMenuItem2
            // 
            this.bomCompareToolStripMenuItem2.Name = "bomCompareToolStripMenuItem2";
            this.bomCompareToolStripMenuItem2.Size = new System.Drawing.Size(155, 22);
            this.bomCompareToolStripMenuItem2.Text = "BomCompare";
            this.bomCompareToolStripMenuItem2.Click += new System.EventHandler(this.bomCompareToolStripMenuItem_Click);
            // 
            // diskToolStripMenuItem
            // 
            this.diskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diskScanToolStripMenuItem});
            this.diskToolStripMenuItem.Name = "diskToolStripMenuItem";
            this.diskToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.diskToolStripMenuItem.Text = "Disk";
            // 
            // diskScanToolStripMenuItem
            // 
            this.diskScanToolStripMenuItem.Name = "diskScanToolStripMenuItem";
            this.diskScanToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.diskScanToolStripMenuItem.Text = "DiskScan";
            this.diskScanToolStripMenuItem.Click += new System.EventHandler(this.diskScanToolStripMenuItem_Click);
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testProcessBarToolStripMenuItem1,
            this.testMvSecurityScanBoToolStripMenuItem1,
            this.testExtractToolStripMenuItem,
            this.testADToolStripMenuItem});
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.testingToolStripMenuItem.Text = "Testing";
            // 
            // testProcessBarToolStripMenuItem1
            // 
            this.testProcessBarToolStripMenuItem1.Name = "testProcessBarToolStripMenuItem1";
            this.testProcessBarToolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
            this.testProcessBarToolStripMenuItem1.Text = "TestProcessBar";
            this.testProcessBarToolStripMenuItem1.Click += new System.EventHandler(this.testProcessBarToolStripMenuItem_Click);
            // 
            // testMvSecurityScanBoToolStripMenuItem1
            // 
            this.testMvSecurityScanBoToolStripMenuItem1.Name = "testMvSecurityScanBoToolStripMenuItem1";
            this.testMvSecurityScanBoToolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
            this.testMvSecurityScanBoToolStripMenuItem1.Text = "TestMvSecurityScanBo";
            this.testMvSecurityScanBoToolStripMenuItem1.Click += new System.EventHandler(this.testMvSecurityScanBoToolStripMenuItem_Click);
            // 
            // testExtractToolStripMenuItem
            // 
            this.testExtractToolStripMenuItem.Name = "testExtractToolStripMenuItem";
            this.testExtractToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.testExtractToolStripMenuItem.Text = "TestExtract";
            this.testExtractToolStripMenuItem.Click += new System.EventHandler(this.testExtractToolStripMenuItem_Click);
            // 
            // testADToolStripMenuItem
            // 
            this.testADToolStripMenuItem.Name = "testADToolStripMenuItem";
            this.testADToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.testADToolStripMenuItem.Text = "TestAD";
            this.testADToolStripMenuItem.Click += new System.EventHandler(this.testADToolStripMenuItem_Click);
            // 
            // checkDiskToolStripMenuItem
            // 
            this.checkDiskToolStripMenuItem.Name = "checkDiskToolStripMenuItem";
            this.checkDiskToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // reportToolStripMenuItem1
            // 
            this.reportToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mocP07ToolStripMenuItem,
            this.mocP10ToolStripMenuItem,
            this.bomP09ToolStripMenuItem1});
            this.reportToolStripMenuItem1.Name = "reportToolStripMenuItem1";
            this.reportToolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
            this.reportToolStripMenuItem1.Text = "Report";
            // 
            // mocP07ToolStripMenuItem
            // 
            this.mocP07ToolStripMenuItem.Name = "mocP07ToolStripMenuItem";
            this.mocP07ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.mocP07ToolStripMenuItem.Text = "MocP07";
            this.mocP07ToolStripMenuItem.Click += new System.EventHandler(this.mocP07ToolStripMenuItem_Click);
            // 
            // mocP10ToolStripMenuItem
            // 
            this.mocP10ToolStripMenuItem.Name = "mocP10ToolStripMenuItem";
            this.mocP10ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.mocP10ToolStripMenuItem.Text = "MocP10";
            this.mocP10ToolStripMenuItem.Click += new System.EventHandler(this.mocP10ToolStripMenuItem_Click);
            // 
            // bomP09ToolStripMenuItem1
            // 
            this.bomP09ToolStripMenuItem1.Name = "bomP09ToolStripMenuItem1";
            this.bomP09ToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.bomP09ToolStripMenuItem1.Text = "BomP09";
            this.bomP09ToolStripMenuItem1.Click += new System.EventHandler(this.bomP09ToolStripMenuItem1_Click);
            // 
            // 報表ToolStripMenuItem
            // 
            this.報表ToolStripMenuItem.Name = "報表ToolStripMenuItem";
            this.報表ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // invP15ToolStripMenuItem
            // 
            this.invP15ToolStripMenuItem.Name = "invP15ToolStripMenuItem";
            this.invP15ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // purP06W3ToolStripMenuItem
            // 
            this.purP06W3ToolStripMenuItem.Name = "purP06W3ToolStripMenuItem";
            this.purP06W3ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.purP06W3ToolStripMenuItem.Text = "PurP06W3";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(21, 54);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(27, 12);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "Wait";
            // 
            // searchBomToolStripMenuItem
            // 
            this.searchBomToolStripMenuItem.Name = "searchBomToolStripMenuItem";
            this.searchBomToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.searchBomToolStripMenuItem.Text = "SearchBom";
            this.searchBomToolStripMenuItem.Click += new System.EventHandler(this.searchBomToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 109);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPortal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 報表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invP15ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purP06W3ToolStripMenuItem;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mocP07ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mocP10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bomP09ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem erpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bomCompareToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem diskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diskScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testProcessBarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem testMvSecurityScanBoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem testExtractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchBomToolStripMenuItem;
    }
}