namespace MvLocalProject.Viewer
{
    partial class frmTestMisGetBom
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.lblBom = new DevExpress.XtraEditors.LabelControl();
            this.sbtnGetBomList = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.sbDebug = new DevExpress.XtraEditors.SimpleButton();
            this.sbConvertToMoc = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sbCopyCell = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboBomType);
            this.groupBox1.Controls.Add(this.lblBom);
            this.groupBox1.Controls.Add(this.sbtnGetBomList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 52);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bom專區";
            // 
            // cboBomType
            // 
            this.cboBomType.FormattingEnabled = true;
            this.cboBomType.Location = new System.Drawing.Point(60, 21);
            this.cboBomType.Name = "cboBomType";
            this.cboBomType.Size = new System.Drawing.Size(490, 20);
            this.cboBomType.TabIndex = 3;
            // 
            // lblBom
            // 
            this.lblBom.Location = new System.Drawing.Point(6, 23);
            this.lblBom.Name = "lblBom";
            this.lblBom.Size = new System.Drawing.Size(48, 14);
            this.lblBom.TabIndex = 0;
            this.lblBom.Text = "Bom編號";
            // 
            // sbtnGetBomList
            // 
            this.sbtnGetBomList.Location = new System.Drawing.Point(556, 7);
            this.sbtnGetBomList.Name = "sbtnGetBomList";
            this.sbtnGetBomList.Size = new System.Drawing.Size(41, 45);
            this.sbtnGetBomList.TabIndex = 2;
            this.sbtnGetBomList.Text = "Get";
            this.sbtnGetBomList.Click += new System.EventHandler(this.sbtnGetBomList_Click);
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(12, 70);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(725, 564);
            this.treeList1.TabIndex = 5;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            // 
            // treeList2
            // 
            this.treeList2.DataSource = null;
            this.treeList2.Location = new System.Drawing.Point(744, 70);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(725, 564);
            this.treeList2.TabIndex = 6;
            this.treeList2.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList2_NodeCellStyle);
            // 
            // sbDebug
            // 
            this.sbDebug.Location = new System.Drawing.Point(1414, 23);
            this.sbDebug.Name = "sbDebug";
            this.sbDebug.Size = new System.Drawing.Size(51, 36);
            this.sbDebug.TabIndex = 7;
            this.sbDebug.Text = "Debug";
            this.sbDebug.Click += new System.EventHandler(this.sbDebug_Click);
            // 
            // sbConvertToMoc
            // 
            this.sbConvertToMoc.Location = new System.Drawing.Point(615, 19);
            this.sbConvertToMoc.Name = "sbConvertToMoc";
            this.sbConvertToMoc.Size = new System.Drawing.Size(95, 45);
            this.sbConvertToMoc.TabIndex = 4;
            this.sbConvertToMoc.Text = "DebugCheck";
            this.sbConvertToMoc.Click += new System.EventHandler(this.sbConvertToMoc_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(768, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 22);
            this.textBox1.TabIndex = 8;
            // 
            // sbCopyCell
            // 
            this.sbCopyCell.Location = new System.Drawing.Point(934, 27);
            this.sbCopyCell.Name = "sbCopyCell";
            this.sbCopyCell.Size = new System.Drawing.Size(61, 32);
            this.sbCopyCell.TabIndex = 4;
            this.sbCopyCell.Text = "CopyCell";
            this.sbCopyCell.Click += new System.EventHandler(this.sbCopyCell_Click);
            // 
            // frmTestMisGetBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 635);
            this.Controls.Add(this.sbCopyCell);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sbConvertToMoc);
            this.Controls.Add(this.sbDebug);
            this.Controls.Add(this.treeList2);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestMisGetBom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestMisGetBom";
            this.Load += new System.EventHandler(this.frmTestMisGetBom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboBomType;
        private DevExpress.XtraEditors.LabelControl lblBom;
        private DevExpress.XtraEditors.SimpleButton sbtnGetBomList;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraEditors.SimpleButton sbDebug;
        private DevExpress.XtraEditors.SimpleButton sbConvertToMoc;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton sbCopyCell;
    }
}