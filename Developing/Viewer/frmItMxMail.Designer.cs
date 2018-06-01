namespace MvLocalProject.Viewer
{
    partial class frmItMxMail
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.deStart = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbQueryUser = new DevExpress.XtraEditors.SimpleButton();
            this.sbExportUser = new DevExpress.XtraEditors.SimpleButton();
            this.sbExportLdap = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.sbExportAD = new DevExpress.XtraEditors.SimpleButton();
            this.sbLinkMvMxMail = new DevExpress.XtraEditors.SimpleButton();
            this.sbLinkSiGoldMxMail = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "起始日";
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(148, 40);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(113, 20);
            this.deEnd.TabIndex = 3;
            // 
            // deStart
            // 
            this.deStart.EditValue = null;
            this.deStart.Location = new System.Drawing.Point(8, 40);
            this.deStart.Name = "deStart";
            this.deStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Size = new System.Drawing.Size(113, 20);
            this.deStart.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "結束日";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 87);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1302, 380);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            // 
            // sbQueryUser
            // 
            this.sbQueryUser.Location = new System.Drawing.Point(288, 12);
            this.sbQueryUser.Name = "sbQueryUser";
            this.sbQueryUser.Size = new System.Drawing.Size(66, 69);
            this.sbQueryUser.TabIndex = 7;
            this.sbQueryUser.Text = "查詢使用者";
            this.sbQueryUser.Click += new System.EventHandler(this.sbQueryUser_Click);
            // 
            // sbExportUser
            // 
            this.sbExportUser.Location = new System.Drawing.Point(360, 16);
            this.sbExportUser.Name = "sbExportUser";
            this.sbExportUser.Size = new System.Drawing.Size(66, 26);
            this.sbExportUser.TabIndex = 8;
            this.sbExportUser.Text = "匯出使用者";
            // 
            // sbExportLdap
            // 
            this.sbExportLdap.Location = new System.Drawing.Point(360, 48);
            this.sbExportLdap.Name = "sbExportLdap";
            this.sbExportLdap.Size = new System.Drawing.Size(66, 28);
            this.sbExportLdap.TabIndex = 9;
            this.sbExportLdap.Text = "匯出LDAP";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.deEnd);
            this.groupControl1.Controls.Add(this.deStart);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(270, 69);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "日期區間";
            // 
            // sbExportAD
            // 
            this.sbExportAD.Location = new System.Drawing.Point(520, 16);
            this.sbExportAD.Name = "sbExportAD";
            this.sbExportAD.Size = new System.Drawing.Size(66, 26);
            this.sbExportAD.TabIndex = 11;
            this.sbExportAD.Text = "匯出AD";
            this.sbExportAD.Click += new System.EventHandler(this.sbExportAD_Click);
            // 
            // sbLinkMvMxMail
            // 
            this.sbLinkMvMxMail.Location = new System.Drawing.Point(432, 16);
            this.sbLinkMvMxMail.Name = "sbLinkMvMxMail";
            this.sbLinkMvMxMail.Size = new System.Drawing.Size(82, 26);
            this.sbLinkMvMxMail.TabIndex = 12;
            this.sbLinkMvMxMail.Text = "Go_MvMail";
            this.sbLinkMvMxMail.Click += new System.EventHandler(this.sbLinkMvMxMail_Click);
            // 
            // sbLinkSiGoldMxMail
            // 
            this.sbLinkSiGoldMxMail.Location = new System.Drawing.Point(432, 48);
            this.sbLinkSiGoldMxMail.Name = "sbLinkSiGoldMxMail";
            this.sbLinkSiGoldMxMail.Size = new System.Drawing.Size(82, 26);
            this.sbLinkSiGoldMxMail.TabIndex = 13;
            this.sbLinkSiGoldMxMail.Text = "Go_SiGoldMail";
            this.sbLinkSiGoldMxMail.Click += new System.EventHandler(this.sbLinkSiGoldMxMail_Click);
            // 
            // frmItMxMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 477);
            this.Controls.Add(this.sbLinkSiGoldMxMail);
            this.Controls.Add(this.sbLinkMvMxMail);
            this.Controls.Add(this.sbExportAD);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.sbExportLdap);
            this.Controls.Add(this.sbExportUser);
            this.Controls.Add(this.sbQueryUser);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmItMxMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmItMxMail";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmItMxMail_FormClosed);
            this.Load += new System.EventHandler(this.frmItMxMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit deEnd;
        private DevExpress.XtraEditors.DateEdit deStart;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sbQueryUser;
        private DevExpress.XtraEditors.SimpleButton sbExportUser;
        private DevExpress.XtraEditors.SimpleButton sbExportLdap;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton sbExportAD;
        private DevExpress.XtraEditors.SimpleButton sbLinkMvMxMail;
        private DevExpress.XtraEditors.SimpleButton sbLinkSiGoldMxMail;
    }
}