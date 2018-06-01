namespace MvLocalProject.Viewer
{
    partial class frmMoc
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
            this.label1 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.sbtnGet = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "母製令單號";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(83, 6);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(214, 20);
            this.textEdit1.TabIndex = 1;
            // 
            // sbtnGet
            // 
            this.sbtnGet.Location = new System.Drawing.Point(303, 5);
            this.sbtnGet.Name = "sbtnGet";
            this.sbtnGet.Size = new System.Drawing.Size(54, 21);
            this.sbtnGet.TabIndex = 2;
            this.sbtnGet.Text = "取得";
            this.sbtnGet.Click += new System.EventHandler(this.sbtnGet_Click);
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(12, 44);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(1286, 538);
            this.treeList1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "輸入格式 : A511-20180500001";
            // 
            // frmMoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 594);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.sbtnGet);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.label1);
            this.Name = "frmMoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMoc_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton sbtnGet;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.Label label2;
    }
}