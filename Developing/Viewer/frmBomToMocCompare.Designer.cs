namespace MvLocalProject.Viewer
{
    partial class frmBomToMocCompare
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
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.sbGetMoc = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.sbMocCompare = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.sbMergeBom = new DevExpress.XtraEditors.SimpleButton();
            this.sbConvertVirtualMoc = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnGetBomList = new DevExpress.XtraEditors.SimpleButton();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.lblBom = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList7 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList4 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList5 = new DevExpress.XtraTreeList.TreeList();
            this.treeList6 = new DevExpress.XtraTreeList.TreeList();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barCopyCell = new DevExpress.XtraBars.BarButtonItem();
            this.barExpandAll = new DevExpress.XtraBars.BarButtonItem();
            this.barCollapseAll = new DevExpress.XtraBars.BarButtonItem();
            this.barMappingNodes = new DevExpress.XtraBars.BarButtonItem();
            this.barShowRowId = new DevExpress.XtraBars.BarButtonItem();
            this.barHideRowId = new DevExpress.XtraBars.BarButtonItem();
            this.barExportExcel = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList7)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).BeginInit();
            this.xtraTabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.sbGetMoc);
            this.groupControl3.Controls.Add(this.textEdit1);
            this.groupControl3.Controls.Add(this.sbMocCompare);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Location = new System.Drawing.Point(625, 25);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(478, 59);
            this.groupControl3.TabIndex = 34;
            this.groupControl3.Text = "Moc Compare Selection";
            // 
            // sbGetMoc
            // 
            this.sbGetMoc.Location = new System.Drawing.Point(351, 30);
            this.sbGetMoc.Name = "sbGetMoc";
            this.sbGetMoc.Size = new System.Drawing.Size(58, 24);
            this.sbGetMoc.TabIndex = 6;
            this.sbGetMoc.Text = "GetMoc";
            this.sbGetMoc.Click += new System.EventHandler(this.sbGetMoc_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(42, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(303, 20);
            this.textEdit1.TabIndex = 5;
            this.textEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyDown);
            // 
            // sbMocCompare
            // 
            this.sbMocCompare.Location = new System.Drawing.Point(415, 29);
            this.sbMocCompare.Name = "sbMocCompare";
            this.sbMocCompare.Size = new System.Drawing.Size(58, 24);
            this.sbMocCompare.TabIndex = 4;
            this.sbMocCompare.Text = "Compare";
            this.sbMocCompare.Click += new System.EventHandler(this.sbMocCompare_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.sbMergeBom);
            this.groupControl2.Controls.Add(this.sbConvertVirtualMoc);
            this.groupControl2.Controls.Add(this.sbtnGetBomList);
            this.groupControl2.Controls.Add(this.cboBomType);
            this.groupControl2.Controls.Add(this.lblBom);
            this.groupControl2.Location = new System.Drawing.Point(12, 25);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(607, 59);
            this.groupControl2.TabIndex = 33;
            this.groupControl2.Text = "Bom Selection";
            // 
            // sbMergeBom
            // 
            this.sbMergeBom.Location = new System.Drawing.Point(493, 30);
            this.sbMergeBom.Name = "sbMergeBom";
            this.sbMergeBom.Size = new System.Drawing.Size(50, 23);
            this.sbMergeBom.TabIndex = 5;
            this.sbMergeBom.Text = "Merge";
            this.sbMergeBom.Click += new System.EventHandler(this.sbMergeBom_Click);
            // 
            // sbConvertVirtualMoc
            // 
            this.sbConvertVirtualMoc.Location = new System.Drawing.Point(549, 30);
            this.sbConvertVirtualMoc.Name = "sbConvertVirtualMoc";
            this.sbConvertVirtualMoc.Size = new System.Drawing.Size(50, 23);
            this.sbConvertVirtualMoc.TabIndex = 4;
            this.sbConvertVirtualMoc.Text = "Convert";
            this.sbConvertVirtualMoc.Click += new System.EventHandler(this.sbConvertVirtualMoc_Click);
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
            // lblBom
            // 
            this.lblBom.Location = new System.Drawing.Point(5, 34);
            this.lblBom.Name = "lblBom";
            this.lblBom.Size = new System.Drawing.Size(31, 14);
            this.lblBom.TabIndex = 0;
            this.lblBom.Text = "Name";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textBox1);
            this.groupControl1.Location = new System.Drawing.Point(1109, 25);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(198, 59);
            this.groupControl1.TabIndex = 32;
            this.groupControl1.Text = "Cell Selected";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 22);
            this.textBox1.TabIndex = 22;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 90);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1300, 511);
            this.xtraTabControl1.TabIndex = 31;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Controls.Add(this.treeList2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(5, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(745, 476);
            this.treeList1.TabIndex = 4;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // treeList2
            // 
            this.treeList2.DataSource = null;
            this.treeList2.Location = new System.Drawing.Point(756, 3);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(533, 476);
            this.treeList2.TabIndex = 5;
            this.treeList2.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList2_NodeCellStyle);
            this.treeList2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList2_MouseUp);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.treeList7);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // treeList7
            // 
            this.treeList7.DataSource = null;
            this.treeList7.Location = new System.Drawing.Point(4, 3);
            this.treeList7.Name = "treeList7";
            this.treeList7.Size = new System.Drawing.Size(1286, 476);
            this.treeList7.TabIndex = 7;
            this.treeList7.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList7_NodeCellStyle);
            this.treeList7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList7_MouseUp);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.treeList3);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage3.Text = "xtraTabPage3";
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
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.treeList4);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage4.Text = "xtraTabPage4";
            // 
            // treeList4
            // 
            this.treeList4.DataSource = null;
            this.treeList4.Location = new System.Drawing.Point(4, 3);
            this.treeList4.Name = "treeList4";
            this.treeList4.Size = new System.Drawing.Size(1286, 476);
            this.treeList4.TabIndex = 6;
            this.treeList4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList4_MouseUp);
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.treeList5);
            this.xtraTabPage5.Controls.Add(this.treeList6);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1294, 482);
            this.xtraTabPage5.Text = "xtraTabPage5";
            // 
            // treeList5
            // 
            this.treeList5.DataSource = null;
            this.treeList5.Location = new System.Drawing.Point(5, 3);
            this.treeList5.Name = "treeList5";
            this.treeList5.Size = new System.Drawing.Size(448, 476);
            this.treeList5.TabIndex = 6;
            this.treeList5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList5_MouseUp);
            // 
            // treeList6
            // 
            this.treeList6.DataSource = null;
            this.treeList6.Location = new System.Drawing.Point(459, 3);
            this.treeList6.Name = "treeList6";
            this.treeList6.Size = new System.Drawing.Size(830, 476);
            this.treeList6.TabIndex = 7;
            this.treeList6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList6_MouseUp);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(1244, 4);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(63, 33);
            this.btnExportExcel.TabIndex = 30;
            this.btnExportExcel.Text = "Export  Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Visible = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCopyCell,
            this.barExpandAll,
            this.barCollapseAll,
            this.barMappingNodes,
            this.barShowRowId,
            this.barHideRowId,
            this.barExportExcel});
            this.barManager1.MaxItemId = 7;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1315, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 580);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1315, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 580);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1315, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 580);
            // 
            // barCopyCell
            // 
            this.barCopyCell.Caption = "CopyCell";
            this.barCopyCell.Id = 0;
            this.barCopyCell.Name = "barCopyCell";
            this.barCopyCell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopyCell_ItemClick);
            // 
            // barExpandAll
            // 
            this.barExpandAll.Caption = "ExpandAll";
            this.barExpandAll.Id = 1;
            this.barExpandAll.Name = "barExpandAll";
            this.barExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExpandAll_ItemClick);
            // 
            // barCollapseAll
            // 
            this.barCollapseAll.Caption = "CollapseAll";
            this.barCollapseAll.Id = 2;
            this.barCollapseAll.Name = "barCollapseAll";
            this.barCollapseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCollapseAll_ItemClick);
            // 
            // barMappingNodes
            // 
            this.barMappingNodes.Caption = "MappingNodes";
            this.barMappingNodes.Id = 3;
            this.barMappingNodes.Name = "barMappingNodes";
            this.barMappingNodes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMappingNodes_ItemClick);
            // 
            // barShowRowId
            // 
            this.barShowRowId.Caption = "ShowRowId";
            this.barShowRowId.Id = 4;
            this.barShowRowId.Name = "barShowRowId";
            this.barShowRowId.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barShowRowId_ItemClick);
            // 
            // barHideRowId
            // 
            this.barHideRowId.Caption = "HideRowId";
            this.barHideRowId.Id = 5;
            this.barHideRowId.Name = "barHideRowId";
            this.barHideRowId.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barHideRowId_ItemClick);
            // 
            // barExportExcel
            // 
            this.barExportExcel.Caption = "ExportExcel";
            this.barExportExcel.Id = 6;
            this.barExportExcel.Name = "barExportExcel";
            this.barExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportExcel_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopyCell),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExpandAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCollapseAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMappingNodes),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShowRowId),
            new DevExpress.XtraBars.LinkPersistInfo(this.barHideRowId),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportExcel)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmBomToMocCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 603);
            this.Controls.Add(this.groupControl3);
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
            this.Name = "frmBomToMocCompare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBomToMocCompare";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBomToMocCompare_FormClosed);
            this.Load += new System.EventHandler(this.frmBomToMocCompare_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList7)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).EndInit();
            this.xtraTabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton sbMocCompare;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton sbtnGetBomList;
        private System.Windows.Forms.ComboBox cboBomType;
        private DevExpress.XtraEditors.LabelControl lblBom;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTreeList.TreeList treeList3;
        private System.Windows.Forms.Button btnExportExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barCopyCell;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTreeList.TreeList treeList4;
        private DevExpress.XtraEditors.SimpleButton sbGetMoc;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTreeList.TreeList treeList5;
        private DevExpress.XtraTreeList.TreeList treeList6;
        private DevExpress.XtraBars.BarButtonItem barExpandAll;
        private DevExpress.XtraBars.BarButtonItem barCollapseAll;
        private DevExpress.XtraBars.BarButtonItem barMappingNodes;
        private DevExpress.XtraEditors.SimpleButton sbConvertVirtualMoc;
        private DevExpress.XtraEditors.SimpleButton sbMergeBom;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTreeList.TreeList treeList7;
        private DevExpress.XtraBars.BarButtonItem barShowRowId;
        private DevExpress.XtraBars.BarButtonItem barHideRowId;
        private DevExpress.XtraBars.BarButtonItem barExportExcel;
    }
}