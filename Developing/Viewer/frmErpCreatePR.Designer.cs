namespace MvLocalProject.Viewer
{
    partial class frmErpCreatePR
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label8 = new System.Windows.Forms.Label();
            this.cboErpNotePr = new System.Windows.Forms.ComboBox();
            this.sbCreateNote = new DevExpress.XtraEditors.SimpleButton();
            this.sbOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.label8);
            this.groupControl2.Controls.Add(this.cboErpNotePr);
            this.groupControl2.Controls.Add(this.sbCreateNote);
            this.groupControl2.Controls.Add(this.sbOpenFile);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(341, 59);
            this.groupControl2.TabIndex = 35;
            this.groupControl2.Text = "Function Selection";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 14);
            this.label8.TabIndex = 51;
            this.label8.Text = "請購單別";
            // 
            // cboErpNotePr
            // 
            this.cboErpNotePr.FormattingEnabled = true;
            this.cboErpNotePr.Location = new System.Drawing.Point(65, 31);
            this.cboErpNotePr.Name = "cboErpNotePr";
            this.cboErpNotePr.Size = new System.Drawing.Size(119, 22);
            this.cboErpNotePr.TabIndex = 9;
            // 
            // sbCreateNote
            // 
            this.sbCreateNote.Location = new System.Drawing.Point(261, 31);
            this.sbCreateNote.Name = "sbCreateNote";
            this.sbCreateNote.Size = new System.Drawing.Size(73, 23);
            this.sbCreateNote.TabIndex = 5;
            this.sbCreateNote.Text = "CreateNote";
            this.sbCreateNote.Click += new System.EventHandler(this.sbCreateNote_Click);
            // 
            // sbOpenFile
            // 
            this.sbOpenFile.Location = new System.Drawing.Point(190, 31);
            this.sbOpenFile.Name = "sbOpenFile";
            this.sbOpenFile.Size = new System.Drawing.Size(65, 23);
            this.sbOpenFile.TabIndex = 2;
            this.sbOpenFile.Text = "OpenFile";
            this.sbOpenFile.Click += new System.EventHandler(this.sbOpenFile_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 77);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1300, 531);
            this.xtraTabControl1.TabIndex = 36;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1294, 502);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(3, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(1288, 496);
            this.treeList1.TabIndex = 4;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.treeList3);
            this.xtraTabPage2.Controls.Add(this.textBox7);
            this.xtraTabPage2.Controls.Add(this.label7);
            this.xtraTabPage2.Controls.Add(this.textBox4);
            this.xtraTabPage2.Controls.Add(this.textBox5);
            this.xtraTabPage2.Controls.Add(this.textBox6);
            this.xtraTabPage2.Controls.Add(this.textBox3);
            this.xtraTabPage2.Controls.Add(this.textBox2);
            this.xtraTabPage2.Controls.Add(this.textBox1);
            this.xtraTabPage2.Controls.Add(this.label6);
            this.xtraTabPage2.Controls.Add(this.label5);
            this.xtraTabPage2.Controls.Add(this.label4);
            this.xtraTabPage2.Controls.Add(this.label3);
            this.xtraTabPage2.Controls.Add(this.label2);
            this.xtraTabPage2.Controls.Add(this.label1);
            this.xtraTabPage2.Controls.Add(this.treeList2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1294, 502);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // treeList3
            // 
            this.treeList3.DataSource = null;
            this.treeList3.Location = new System.Drawing.Point(3, 181);
            this.treeList3.Name = "treeList3";
            this.treeList3.Size = new System.Drawing.Size(1288, 318);
            this.treeList3.TabIndex = 51;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(400, 36);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 22);
            this.textBox7.TabIndex = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 49;
            this.label7.Text = "需求日期";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(233, 8);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 48;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(233, 36);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 22);
            this.textBox5.TabIndex = 47;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(400, 8);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 22);
            this.textBox6.TabIndex = 46;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(64, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(436, 22);
            this.textBox3.TabIndex = 45;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(64, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 44;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 42;
            this.label6.Text = "備註";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 41;
            this.label5.Text = "請購部門";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 40;
            this.label4.Text = "請購人員";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 39;
            this.label3.Text = "單據日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 38;
            this.label2.Text = "請購單號";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 37;
            this.label1.Text = "請購單別";
            // 
            // treeList2
            // 
            this.treeList2.DataSource = null;
            this.treeList2.Location = new System.Drawing.Point(3, 92);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(1288, 83);
            this.treeList2.TabIndex = 5;
            // 
            // frmErpCreatePR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 614);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmErpCreatePR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmErpCreatePR";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmErpCreatePR_FormClosed);
            this.Load += new System.EventHandler(this.frmErpCreatePR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton sbCreateNote;
        private DevExpress.XtraEditors.SimpleButton sbOpenFile;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboErpNotePr;
        private DevExpress.XtraTreeList.TreeList treeList3;
    }
}