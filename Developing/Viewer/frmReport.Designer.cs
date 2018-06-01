namespace MvLocalProject.Viewer
{
    partial class frmReport
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupDate = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboReportType = new System.Windows.Forms.ComboBox();
            this.groupBom = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.chkBomListBox = new System.Windows.Forms.CheckedListBox();
            this.groupDate.SuspendLayout();
            this.groupBom.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(65, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(102, 22);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "起始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "結束日期";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(258, 21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(102, 22);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(316, 35);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 34);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "產生報表";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupDate
            // 
            this.groupDate.Controls.Add(this.dateTimePicker1);
            this.groupDate.Controls.Add(this.label1);
            this.groupDate.Controls.Add(this.label2);
            this.groupDate.Controls.Add(this.dateTimePicker2);
            this.groupDate.Location = new System.Drawing.Point(12, 75);
            this.groupDate.Name = "groupDate";
            this.groupDate.Size = new System.Drawing.Size(371, 52);
            this.groupDate.TabIndex = 6;
            this.groupDate.TabStop = false;
            this.groupDate.Text = "選擇日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "報表選項";
            // 
            // cboReportType
            // 
            this.cboReportType.FormattingEnabled = true;
            this.cboReportType.Location = new System.Drawing.Point(12, 40);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(298, 20);
            this.cboReportType.TabIndex = 8;
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
            // 
            // groupBom
            // 
            this.groupBom.Controls.Add(this.label4);
            this.groupBom.Controls.Add(this.cboBomType);
            this.groupBom.Location = new System.Drawing.Point(12, 142);
            this.groupBom.Name = "groupBom";
            this.groupBom.Size = new System.Drawing.Size(371, 60);
            this.groupBom.TabIndex = 9;
            this.groupBom.TabStop = false;
            this.groupBom.Text = "Bom專用";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "產品編號";
            // 
            // cboBomType
            // 
            this.cboBomType.FormattingEnabled = true;
            this.cboBomType.Location = new System.Drawing.Point(65, 21);
            this.cboBomType.Name = "cboBomType";
            this.cboBomType.Size = new System.Drawing.Size(295, 20);
            this.cboBomType.TabIndex = 10;
            // 
            // chkBomListBox
            // 
            this.chkBomListBox.FormattingEnabled = true;
            this.chkBomListBox.Location = new System.Drawing.Point(12, 208);
            this.chkBomListBox.Name = "chkBomListBox";
            this.chkBomListBox.Size = new System.Drawing.Size(371, 157);
            this.chkBomListBox.TabIndex = 10;
            this.chkBomListBox.SelectedIndexChanged += new System.EventHandler(this.chkBomListBox_SelectedIndexChanged);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 381);
            this.Controls.Add(this.chkBomListBox);
            this.Controls.Add(this.groupBom);
            this.Controls.Add(this.cboReportType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupDate);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReport_FormClosed);
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.groupDate.ResumeLayout(false);
            this.groupDate.PerformLayout();
            this.groupBom.ResumeLayout(false);
            this.groupBom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboReportType;
        private System.Windows.Forms.GroupBox groupBom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboBomType;
        private System.Windows.Forms.CheckedListBox chkBomListBox;
    }
}