namespace MvLocalProject.Viewer
{
    partial class frmMocP07
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboRpt = new System.Windows.Forms.ComboBox();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLoadERPData = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonQueryTest = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "機型篩選";
            // 
            // cboRpt
            // 
            this.cboRpt.FormattingEnabled = true;
            this.cboRpt.Location = new System.Drawing.Point(90, 32);
            this.cboRpt.Name = "cboRpt";
            this.cboRpt.Size = new System.Drawing.Size(152, 20);
            this.cboRpt.TabIndex = 2;
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(248, 32);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(66, 20);
            this.buttonQuery.TabIndex = 3;
            this.buttonQuery.Text = "查詢";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "母製令機號";
            // 
            // buttonLoadERPData
            // 
            this.buttonLoadERPData.Location = new System.Drawing.Point(329, 30);
            this.buttonLoadERPData.Name = "buttonLoadERPData";
            this.buttonLoadERPData.Size = new System.Drawing.Size(101, 23);
            this.buttonLoadERPData.TabIndex = 9;
            this.buttonLoadERPData.Text = "Load ERP Data";
            this.buttonLoadERPData.UseVisualStyleBackColor = true;
            this.buttonLoadERPData.Click += new System.EventHandler(this.buttonLoadERPData_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(436, 29);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Load SOP Data";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // buttonQueryTest
            // 
            this.buttonQueryTest.Location = new System.Drawing.Point(248, 6);
            this.buttonQueryTest.Name = "buttonQueryTest";
            this.buttonQueryTest.Size = new System.Drawing.Size(66, 20);
            this.buttonQueryTest.TabIndex = 11;
            this.buttonQueryTest.Text = "Test";
            this.buttonQueryTest.UseVisualStyleBackColor = true;
            this.buttonQueryTest.Click += new System.EventHandler(this.buttonQueryTest_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(634, 306);
            this.dataGridView1.TabIndex = 12;
            // 
            // frmMocP07
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 393);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonQueryTest);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.buttonLoadERPData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.cboRpt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMocP07";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMocP07";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMocP07_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboRpt;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLoadERPData;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonQueryTest;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

