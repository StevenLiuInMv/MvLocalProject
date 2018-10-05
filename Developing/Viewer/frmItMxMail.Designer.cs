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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.sbEnableUser = new DevExpress.XtraEditors.SimpleButton();
            this.sbClearScript = new DevExpress.XtraEditors.SimpleButton();
            this.sbDisableUser = new DevExpress.XtraEditors.SimpleButton();
            this.cboMvDept = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sbFindEnabledAllUser = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
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
            this.gridControl1.Location = new System.Drawing.Point(12, 185);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1302, 375);
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
            this.sbQueryUser.Location = new System.Drawing.Point(12, 87);
            this.sbQueryUser.Name = "sbQueryUser";
            this.sbQueryUser.Size = new System.Drawing.Size(66, 92);
            this.sbQueryUser.TabIndex = 7;
            this.sbQueryUser.Text = "查詢使用者";
            this.sbQueryUser.Click += new System.EventHandler(this.sbQueryUser_Click);
            // 
            // sbExportUser
            // 
            this.sbExportUser.Location = new System.Drawing.Point(88, 87);
            this.sbExportUser.Name = "sbExportUser";
            this.sbExportUser.Size = new System.Drawing.Size(66, 26);
            this.sbExportUser.TabIndex = 8;
            this.sbExportUser.Text = "匯出使用者";
            // 
            // sbExportLdap
            // 
            this.sbExportLdap.Location = new System.Drawing.Point(88, 119);
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
            this.groupControl1.Size = new System.Drawing.Size(273, 69);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "日期區間";
            // 
            // sbExportAD
            // 
            this.sbExportAD.Location = new System.Drawing.Point(88, 153);
            this.sbExportAD.Name = "sbExportAD";
            this.sbExportAD.Size = new System.Drawing.Size(66, 26);
            this.sbExportAD.TabIndex = 11;
            this.sbExportAD.Text = "匯出AD";
            // 
            // sbLinkMvMxMail
            // 
            this.sbLinkMvMxMail.Location = new System.Drawing.Point(160, 87);
            this.sbLinkMvMxMail.Name = "sbLinkMvMxMail";
            this.sbLinkMvMxMail.Size = new System.Drawing.Size(89, 26);
            this.sbLinkMvMxMail.TabIndex = 12;
            this.sbLinkMvMxMail.Text = "Go_MvMail";
            this.sbLinkMvMxMail.Click += new System.EventHandler(this.sbLinkMvMxMail_Click);
            // 
            // sbLinkSiGoldMxMail
            // 
            this.sbLinkSiGoldMxMail.Location = new System.Drawing.Point(160, 119);
            this.sbLinkSiGoldMxMail.Name = "sbLinkSiGoldMxMail";
            this.sbLinkSiGoldMxMail.Size = new System.Drawing.Size(89, 26);
            this.sbLinkSiGoldMxMail.TabIndex = 13;
            this.sbLinkSiGoldMxMail.Text = "Go_SiGoldMail";
            this.sbLinkSiGoldMxMail.Click += new System.EventHandler(this.sbLinkSiGoldMxMail_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.sbEnableUser);
            this.groupControl2.Controls.Add(this.sbClearScript);
            this.groupControl2.Controls.Add(this.sbDisableUser);
            this.groupControl2.Controls.Add(this.cboMvDept);
            this.groupControl2.Controls.Add(this.richTextBox1);
            this.groupControl2.Controls.Add(this.txtUserName);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Location = new System.Drawing.Point(291, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(745, 167);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "AD指令產生器";
            // 
            // sbEnableUser
            // 
            this.sbEnableUser.Location = new System.Drawing.Point(522, 24);
            this.sbEnableUser.Name = "sbEnableUser";
            this.sbEnableUser.Size = new System.Drawing.Size(66, 22);
            this.sbEnableUser.TabIndex = 16;
            this.sbEnableUser.Text = "啟用使用者";
            this.sbEnableUser.Click += new System.EventHandler(this.sbEnableUser_Click);
            // 
            // sbClearScript
            // 
            this.sbClearScript.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sbClearScript.Appearance.Options.UseForeColor = true;
            this.sbClearScript.Location = new System.Drawing.Point(674, 24);
            this.sbClearScript.Name = "sbClearScript";
            this.sbClearScript.Size = new System.Drawing.Size(66, 22);
            this.sbClearScript.TabIndex = 15;
            this.sbClearScript.Text = "ClearText";
            this.sbClearScript.Click += new System.EventHandler(this.sbClearScript_Click);
            // 
            // sbDisableUser
            // 
            this.sbDisableUser.Location = new System.Drawing.Point(450, 24);
            this.sbDisableUser.Name = "sbDisableUser";
            this.sbDisableUser.Size = new System.Drawing.Size(66, 22);
            this.sbDisableUser.TabIndex = 14;
            this.sbDisableUser.Text = "停用使用者";
            this.sbDisableUser.Click += new System.EventHandler(this.sbDisableUser_Click);
            // 
            // cboMvDept
            // 
            this.cboMvDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMvDept.FormattingEnabled = true;
            this.cboMvDept.Location = new System.Drawing.Point(42, 24);
            this.cboMvDept.Name = "cboMvDept";
            this.cboMvDept.Size = new System.Drawing.Size(177, 22);
            this.cboMvDept.TabIndex = 9;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(8, 52);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(732, 110);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(267, 24);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(177, 22);
            this.txtUserName.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "姓名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "部門";
            // 
            // sbFindEnabledAllUser
            // 
            this.sbFindEnabledAllUser.AutoSize = true;
            this.sbFindEnabledAllUser.Location = new System.Drawing.Point(1042, 12);
            this.sbFindEnabledAllUser.Name = "sbFindEnabledAllUser";
            this.sbFindEnabledAllUser.Size = new System.Drawing.Size(114, 22);
            this.sbFindEnabledAllUser.TabIndex = 14;
            this.sbFindEnabledAllUser.Text = "找All_Enabled_User";
            // 
            // frmItMxMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 572);
            this.Controls.Add(this.sbFindEnabledAllUser);
            this.Controls.Add(this.groupControl2);
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cboMvDept;
        private DevExpress.XtraEditors.SimpleButton sbDisableUser;
        private DevExpress.XtraEditors.SimpleButton sbClearScript;
        private DevExpress.XtraEditors.SimpleButton sbEnableUser;
        private DevExpress.XtraEditors.SimpleButton sbFindEnabledAllUser;
    }
}