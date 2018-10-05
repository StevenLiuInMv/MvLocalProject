namespace MvLocalProject.Viewer
{
    partial class frmReportFinance
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.sbExportExcelFile = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.sbGenerateTextFile = new DevExpress.XtraEditors.SimpleButton();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barCopyCell = new DevExpress.XtraBars.BarButtonItem();
            this.barExportExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barExportText = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.sbExportExcelFile);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.deEnd);
            this.groupControl2.Controls.Add(this.sbGenerateTextFile);
            this.groupControl2.Controls.Add(this.sbOk);
            this.groupControl2.Location = new System.Drawing.Point(3, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(421, 59);
            this.groupControl2.TabIndex = 36;
            this.groupControl2.Text = "廠商匯款產生磁片作業";
            // 
            // sbExportExcelFile
            // 
            this.sbExportExcelFile.Location = new System.Drawing.Point(277, 27);
            this.sbExportExcelFile.Name = "sbExportExcelFile";
            this.sbExportExcelFile.Size = new System.Drawing.Size(65, 23);
            this.sbExportExcelFile.TabIndex = 54;
            this.sbExportExcelFile.Text = "Excel";
            this.sbExportExcelFile.Click += new System.EventHandler(this.sbExportExcelFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 53;
            this.label1.Text = "付款截止日";
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(87, 28);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(113, 20);
            this.deEnd.TabIndex = 52;
            // 
            // sbGenerateTextFile
            // 
            this.sbGenerateTextFile.Location = new System.Drawing.Point(348, 27);
            this.sbGenerateTextFile.Name = "sbGenerateTextFile";
            this.sbGenerateTextFile.Size = new System.Drawing.Size(65, 23);
            this.sbGenerateTextFile.TabIndex = 5;
            this.sbGenerateTextFile.Text = "Text";
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(206, 27);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(65, 23);
            this.sbOk.TabIndex = 2;
            this.sbOk.Text = "OK";
            this.sbOk.Click += new System.EventHandler(this.sbOk_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 20);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1300, 624);
            this.xtraTabControl1.TabIndex = 37;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Controls.Add(this.groupControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1294, 595);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textBox1);
            this.groupControl1.Location = new System.Drawing.Point(1093, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(198, 59);
            this.groupControl1.TabIndex = 38;
            this.groupControl1.Text = "Cell Selected";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 22);
            this.textBox1.TabIndex = 22;
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(3, 69);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(1288, 523);
            this.treeList1.TabIndex = 4;
            this.treeList1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDoubleClick);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1294, 595);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopyCell),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportText)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barCopyCell
            // 
            this.barCopyCell.Caption = "CopyCell";
            this.barCopyCell.Id = 0;
            this.barCopyCell.Name = "barCopyCell";
            this.barCopyCell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopyCell_ItemClick);
            // 
            // barExportExcel
            // 
            this.barExportExcel.Caption = "ExportExcel";
            this.barExportExcel.Id = 1;
            this.barExportExcel.Name = "barExportExcel";
            this.barExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportExcel_ItemClick);
            // 
            // barExportText
            // 
            this.barExportText.Caption = "ExportText";
            this.barExportText.Id = 2;
            this.barExportText.Name = "barExportText";
            this.barExportText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportText_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCopyCell,
            this.barExportExcel,
            this.barExportText});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            this.bar1.Visible = false;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            this.bar2.Visible = false;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1323, 49);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 633);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1323, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 49);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 584);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1323, 49);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 584);
            // 
            // frmReportFinance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 656);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReportFinance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReportFinance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReportFinance_FormClosed);
            this.Load += new System.EventHandler(this.frmReportFinance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton sbGenerateTextFile;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit deEnd;
        private DevExpress.XtraEditors.SimpleButton sbExportExcelFile;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barCopyCell;
        private DevExpress.XtraBars.BarButtonItem barExportExcel;
        private DevExpress.XtraBars.BarButtonItem barExportText;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}