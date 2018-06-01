namespace MvLocalProject.Viewer
{
    partial class frmBomCompareDev
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
            this.groupBom = new System.Windows.Forms.GroupBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnGetBomList = new System.Windows.Forms.Button();
            this.BomView = new System.Windows.Forms.ListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboBomType = new System.Windows.Forms.ComboBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList4 = new DevExpress.XtraTreeList.TreeList();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList5 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.treeList6 = new DevExpress.XtraTreeList.TreeList();
            this.groupBom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList5)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList6)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBom
            // 
            this.groupBom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBom.Controls.Add(this.btnCompare);
            this.groupBom.Controls.Add(this.btnGetBomList);
            this.groupBom.Controls.Add(this.BomView);
            this.groupBom.Controls.Add(this.btnRemove);
            this.groupBom.Controls.Add(this.btnAdd);
            this.groupBom.Controls.Add(this.label4);
            this.groupBom.Controls.Add(this.cboBomType);
            this.groupBom.Location = new System.Drawing.Point(12, 32);
            this.groupBom.Name = "groupBom";
            this.groupBom.Size = new System.Drawing.Size(744, 80);
            this.groupBom.TabIndex = 11;
            this.groupBom.TabStop = false;
            this.groupBom.Text = "Bom專用";
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(673, 8);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(65, 33);
            this.btnCompare.TabIndex = 13;
            this.btnCompare.Text = "Start Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnGetBomList
            // 
            this.btnGetBomList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnGetBomList.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGetBomList.Location = new System.Drawing.Point(6, 54);
            this.btnGetBomList.Name = "btnGetBomList";
            this.btnGetBomList.Size = new System.Drawing.Size(57, 23);
            this.btnGetBomList.TabIndex = 14;
            this.btnGetBomList.Text = "Get";
            this.btnGetBomList.UseVisualStyleBackColor = false;
            this.btnGetBomList.Visible = false;
            this.btnGetBomList.Click += new System.EventHandler(this.btnGetBomList_Click);
            // 
            // BomView
            // 
            this.BomView.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BomView.Location = new System.Drawing.Point(309, 8);
            this.BomView.Name = "BomView";
            this.BomView.Size = new System.Drawing.Size(358, 70);
            this.BomView.TabIndex = 12;
            this.BomView.UseCompatibleStateImageBehavior = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRemove.Location = new System.Drawing.Point(264, 45);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(40, 19);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "<--";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdd.Location = new System.Drawing.Point(264, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 19);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "-->";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "產品編號";
            // 
            // cboBomType
            // 
            this.cboBomType.FormattingEnabled = true;
            this.cboBomType.Location = new System.Drawing.Point(6, 31);
            this.cboBomType.Name = "cboBomType";
            this.cboBomType.Size = new System.Drawing.Size(252, 20);
            this.cboBomType.TabIndex = 10;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 115);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1397, 587);
            this.xtraTabControl1.TabIndex = 14;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeList2);
            this.xtraTabPage1.Controls.Add(this.treeList1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1391, 558);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // treeList2
            // 
            this.treeList2.DataSource = null;
            this.treeList2.Location = new System.Drawing.Point(698, 1);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(690, 554);
            this.treeList2.TabIndex = 15;
            this.treeList2.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList2_NodeCellStyle);
            // 
            // treeList1
            // 
            this.treeList1.DataSource = null;
            this.treeList1.Location = new System.Drawing.Point(2, 1);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(690, 554);
            this.treeList1.TabIndex = 14;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.treeList4);
            this.xtraTabPage2.Controls.Add(this.treeList3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1391, 558);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // treeList4
            // 
            this.treeList4.DataSource = null;
            this.treeList4.Location = new System.Drawing.Point(496, 1);
            this.treeList4.Name = "treeList4";
            this.treeList4.Size = new System.Drawing.Size(888, 554);
            this.treeList4.TabIndex = 15;
            // 
            // treeList3
            // 
            this.treeList3.DataSource = null;
            this.treeList3.Location = new System.Drawing.Point(5, 1);
            this.treeList3.Name = "treeList3";
            this.treeList3.Size = new System.Drawing.Size(491, 554);
            this.treeList3.TabIndex = 15;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.treeList5);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1391, 558);
            this.xtraTabPage3.Text = "xtraTabPage3";
            // 
            // treeList5
            // 
            this.treeList5.DataSource = null;
            this.treeList5.Location = new System.Drawing.Point(5, 2);
            this.treeList5.Name = "treeList5";
            this.treeList5.Size = new System.Drawing.Size(1381, 554);
            this.treeList5.TabIndex = 16;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.treeList6);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1391, 558);
            this.xtraTabPage4.Text = "xtraTabPage4";
            // 
            // treeList6
            // 
            this.treeList6.DataSource = null;
            this.treeList6.Location = new System.Drawing.Point(5, 2);
            this.treeList6.Name = "treeList6";
            this.treeList6.Size = new System.Drawing.Size(1381, 554);
            this.treeList6.TabIndex = 17;
            // 
            // frmBomCompareDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 708);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.groupBom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBomCompareDev";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBomCompareDev";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBomCompareDev_FormClosed);
            this.Load += new System.EventHandler(this.frmBomCompareDev_Load);
            this.groupBom.ResumeLayout(false);
            this.groupBom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList5)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBom;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnGetBomList;
        private System.Windows.Forms.ListView BomView;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboBomType;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTreeList.TreeList treeList4;
        private DevExpress.XtraTreeList.TreeList treeList3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTreeList.TreeList treeList5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTreeList.TreeList treeList6;
    }
}