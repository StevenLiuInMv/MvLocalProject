namespace MvLocalProject.Viewer
{
    partial class frmErpMultiVendorMailer
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
            this.sbOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.sbConvert = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // sbOpenFile
            // 
            this.sbOpenFile.Location = new System.Drawing.Point(12, 12);
            this.sbOpenFile.Name = "sbOpenFile";
            this.sbOpenFile.Size = new System.Drawing.Size(89, 34);
            this.sbOpenFile.TabIndex = 0;
            this.sbOpenFile.Text = "OpenFile";
            this.sbOpenFile.Click += new System.EventHandler(this.sbOpenFile_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 52);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1164, 562);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1158, 533);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(3, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(1152, 527);
            this.treeList1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1158, 533);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // sbConvert
            // 
            this.sbConvert.Location = new System.Drawing.Point(107, 12);
            this.sbConvert.Name = "sbConvert";
            this.sbConvert.Size = new System.Drawing.Size(89, 34);
            this.sbConvert.TabIndex = 2;
            this.sbConvert.Text = "Convert";
            this.sbConvert.Click += new System.EventHandler(this.sbConvert_Click);
            // 
            // frmErpMultiVendorMailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 619);
            this.Controls.Add(this.sbConvert);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.sbOpenFile);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmErpMultiVendorMailer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmErpMultiVendorMailer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmErpMultiVendorMailer_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbOpenFile;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SimpleButton sbConvert;
    }
}