namespace MvLocalProject.Viewer
{
    partial class frmTestParser
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
            this.sbParserRdDiffBomExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbGetBomP07 = new DevExpress.XtraEditors.SimpleButton();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.lblBom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sbCheckInvmb = new DevExpress.XtraEditors.SimpleButton();
            this.sbGetSumByOrderType = new DevExpress.XtraEditors.SimpleButton();
            this.cboOrderType = new System.Windows.Forms.ComboBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.deStart = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.sbCreateA121 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbParserRdDiffBomExcel
            // 
            this.sbParserRdDiffBomExcel.Location = new System.Drawing.Point(12, 12);
            this.sbParserRdDiffBomExcel.Name = "sbParserRdDiffBomExcel";
            this.sbParserRdDiffBomExcel.Size = new System.Drawing.Size(153, 20);
            this.sbParserRdDiffBomExcel.TabIndex = 0;
            this.sbParserRdDiffBomExcel.Text = "讀取RD差異料件清單表";
            this.sbParserRdDiffBomExcel.Click += new System.EventHandler(this.sbParserRdDiffBomExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 114);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1055, 502);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // sbGetBomP07
            // 
            this.sbGetBomP07.Location = new System.Drawing.Point(15, 63);
            this.sbGetBomP07.Name = "sbGetBomP07";
            this.sbGetBomP07.Size = new System.Drawing.Size(74, 25);
            this.sbGetBomP07.TabIndex = 2;
            this.sbGetBomP07.Text = "讀取Bom07";
            this.sbGetBomP07.Click += new System.EventHandler(this.sbGetBomP07_Click);
            // 
            // cboBomType
            // 
            this.cboBomType.FormattingEnabled = true;
            this.cboBomType.Location = new System.Drawing.Point(69, 29);
            this.cboBomType.Name = "cboBomType";
            this.cboBomType.Size = new System.Drawing.Size(237, 22);
            this.cboBomType.TabIndex = 5;
            // 
            // lblBom
            // 
            this.lblBom.Location = new System.Drawing.Point(15, 32);
            this.lblBom.Name = "lblBom";
            this.lblBom.Size = new System.Drawing.Size(48, 14);
            this.lblBom.TabIndex = 4;
            this.lblBom.Text = "Bom編號";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(95, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "labelControl1";
            // 
            // sbCheckInvmb
            // 
            this.sbCheckInvmb.Location = new System.Drawing.Point(282, 12);
            this.sbCheckInvmb.Name = "sbCheckInvmb";
            this.sbCheckInvmb.Size = new System.Drawing.Size(76, 20);
            this.sbCheckInvmb.TabIndex = 7;
            this.sbCheckInvmb.Text = "checkInvmb";
            this.sbCheckInvmb.Click += new System.EventHandler(this.sbCheckInvmb_Click);
            // 
            // sbGetSumByOrderType
            // 
            this.sbGetSumByOrderType.Location = new System.Drawing.Point(168, 63);
            this.sbGetSumByOrderType.Name = "sbGetSumByOrderType";
            this.sbGetSumByOrderType.Size = new System.Drawing.Size(74, 24);
            this.sbGetSumByOrderType.TabIndex = 8;
            this.sbGetSumByOrderType.Text = "GetCount";
            this.sbGetSumByOrderType.Click += new System.EventHandler(this.sbGetSumByOrderType_Click);
            // 
            // cboOrderType
            // 
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Location = new System.Drawing.Point(47, 65);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(115, 22);
            this.cboOrderType.TabIndex = 9;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.deEnd);
            this.groupControl1.Controls.Add(this.deStart);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.cboOrderType);
            this.groupControl1.Controls.Add(this.sbGetSumByOrderType);
            this.groupControl1.Location = new System.Drawing.Point(787, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(280, 96);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "統計單別資訊";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "結束日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "起始日";
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(157, 38);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(113, 20);
            this.deEnd.TabIndex = 14;
            // 
            // deStart
            // 
            this.deStart.EditValue = null;
            this.deStart.Location = new System.Drawing.Point(17, 38);
            this.deStart.Name = "deStart";
            this.deStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Size = new System.Drawing.Size(113, 20);
            this.deStart.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "單別";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.lblBom);
            this.groupControl2.Controls.Add(this.cboBomType);
            this.groupControl2.Controls.Add(this.sbGetBomP07);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Location = new System.Drawing.Point(470, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(311, 96);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 22);
            this.textBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 58);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(223, 22);
            this.textBox2.TabIndex = 13;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 84);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(223, 22);
            this.textBox3.TabIndex = 14;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(241, 32);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(223, 22);
            this.textBox4.TabIndex = 15;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(241, 58);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(223, 22);
            this.textBox5.TabIndex = 16;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(241, 84);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(223, 22);
            this.textBox6.TabIndex = 17;
            // 
            // sbCreateA121
            // 
            this.sbCreateA121.Location = new System.Drawing.Point(388, 12);
            this.sbCreateA121.Name = "sbCreateA121";
            this.sbCreateA121.Size = new System.Drawing.Size(76, 20);
            this.sbCreateA121.TabIndex = 18;
            this.sbCreateA121.Text = "create_A121";
            this.sbCreateA121.Click += new System.EventHandler(this.sbCreateA121_Click);
            // 
            // frmTestParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 628);
            this.Controls.Add(this.sbCreateA121);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.sbCheckInvmb);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.sbParserRdDiffBomExcel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestParser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestParser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestParser_FormClosed);
            this.Load += new System.EventHandler(this.frmTestParser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbParserRdDiffBomExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sbGetBomP07;
        private System.Windows.Forms.ComboBox cboBomType;
        private DevExpress.XtraEditors.LabelControl lblBom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton sbCheckInvmb;
        private DevExpress.XtraEditors.SimpleButton sbGetSumByOrderType;
        private System.Windows.Forms.ComboBox cboOrderType;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit deEnd;
        private DevExpress.XtraEditors.DateEdit deStart;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private DevExpress.XtraEditors.SimpleButton sbCreateA121;
    }
}