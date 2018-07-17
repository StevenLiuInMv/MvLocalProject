namespace MvLocalProject.Viewer
{
    partial class frmBom
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
            this.lblBom = new DevExpress.XtraEditors.LabelControl();
            this.sbtnGetBomList = new DevExpress.XtraEditors.SimpleButton();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barCopyCell = new DevExpress.XtraBars.BarButtonItem();
            this.barExpandAll = new DevExpress.XtraBars.BarButtonItem();
            this.barCollapseAll = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBom
            // 
            this.lblBom.Location = new System.Drawing.Point(5, 34);
            this.lblBom.Name = "lblBom";
            this.lblBom.Size = new System.Drawing.Size(31, 14);
            this.lblBom.TabIndex = 0;
            this.lblBom.Text = "Name";
            // 
            // sbtnGetBomList
            // 
            this.sbtnGetBomList.Location = new System.Drawing.Point(448, 30);
            this.sbtnGetBomList.Name = "sbtnGetBomList";
            this.sbtnGetBomList.Size = new System.Drawing.Size(41, 23);
            this.sbtnGetBomList.TabIndex = 2;
            this.sbtnGetBomList.Text = "Get";
            this.sbtnGetBomList.Click += new System.EventHandler(this.sbtnGetBomList_Click);
            // 
            // cboBomType
            // 
            this.cboBomType.FormattingEnabled = true;
            this.cboBomType.Location = new System.Drawing.Point(42, 30);
            this.cboBomType.Name = "cboBomType";
            this.cboBomType.Size = new System.Drawing.Size(400, 22);
            this.cboBomType.TabIndex = 3;
            this.cboBomType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboBomType_KeyDown);
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(5, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(639, 476);
            this.treeList1.TabIndex = 4;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // treeList2
            // 
            this.treeList2.DataSource = null;
            this.treeList2.Location = new System.Drawing.Point(650, 3);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(639, 476);
            this.treeList2.TabIndex = 5;
            this.treeList2.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList2_NodeCellStyle);
            this.treeList2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList2_MouseUp);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(1244, 4);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(63, 33);
            this.btnExportExcel.TabIndex = 6;
            this.btnExportExcel.Text = "Export  Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Visible = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCopyCell,
            this.barExpandAll,
            this.barCollapseAll});
            this.barManager1.MaxItemId = 8;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1318, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 616);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1318, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 616);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1318, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 616);
            // 
            // barCopyCell
            // 
            this.barCopyCell.Caption = "CopyCell";
            this.barCopyCell.Id = 5;
            this.barCopyCell.Name = "barCopyCell";
            this.barCopyCell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopyCell_ItemClick);
            // 
            // barExpandAll
            // 
            this.barExpandAll.Caption = "ExpandAll";
            this.barExpandAll.Id = 6;
            this.barExpandAll.Name = "barExpandAll";
            this.barExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExpandAll_ItemClick);
            // 
            // barCollapseAll
            // 
            this.barCollapseAll.Caption = "CollapseAll";
            this.barCollapseAll.Id = 7;
            this.barCollapseAll.Name = "barCollapseAll";
            this.barCollapseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCollapseAll_ItemClick);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 90);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1300, 511);
            this.xtraTabControl1.TabIndex = 17;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Controls.Add(this.treeList2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.treeList3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // treeList3
            // 
            this.treeList3.DataSource = null;
            this.treeList3.Location = new System.Drawing.Point(5, 3);
            this.treeList3.Name = "treeList3";
            this.treeList3.Size = new System.Drawing.Size(1286, 476);
            this.treeList3.TabIndex = 5;
            this.treeList3.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList3_NodeCellStyle);
            this.treeList3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList3_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 22);
            this.textBox1.TabIndex = 22;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textBox1);
            this.groupControl1.Location = new System.Drawing.Point(1109, 53);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(198, 59);
            this.groupControl1.TabIndex = 27;
            this.groupControl1.Text = "Cell Selected";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.sbtnGetBomList);
            this.groupControl2.Controls.Add(this.cboBomType);
            this.groupControl2.Controls.Add(this.lblBom);
            this.groupControl2.Location = new System.Drawing.Point(12, 25);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(497, 59);
            this.groupControl2.TabIndex = 28;
            this.groupControl2.Text = "Bom Selection";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopyCell),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExpandAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCollapseAll)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 616);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBom_FormClosed);
            this.Load += new System.EventHandler(this.frmBom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblBom;
        private DevExpress.XtraEditors.SimpleButton sbtnGetBomList;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.ComboBox cboBomType;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private System.Windows.Forms.Button btnExportExcel;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTreeList.TreeList treeList3;
        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barCopyCell;
        private DevExpress.XtraBars.BarButtonItem barExpandAll;
        private DevExpress.XtraBars.BarButtonItem barCollapseAll;
    }
}