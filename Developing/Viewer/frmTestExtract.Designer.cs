namespace MvLocalProject.Viewer
{
    partial class frmTestExtract
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
            this.btnTestList = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn7Zip = new System.Windows.Forms.Button();
            this.btnBuildSummaryData = new System.Windows.Forms.Button();
            this.btnGetExcel = new System.Windows.Forms.Button();
            this.btnGetSummaryExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestList
            // 
            this.btnTestList.Location = new System.Drawing.Point(12, 28);
            this.btnTestList.Name = "btnTestList";
            this.btnTestList.Size = new System.Drawing.Size(80, 42);
            this.btnTestList.TabIndex = 0;
            this.btnTestList.Text = "Uncompress";
            this.btnTestList.UseVisualStyleBackColor = true;
            this.btnTestList.Click += new System.EventHandler(this.btnTestList_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 76);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(467, 476);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // btn7Zip
            // 
            this.btn7Zip.Location = new System.Drawing.Point(108, 28);
            this.btn7Zip.Name = "btn7Zip";
            this.btn7Zip.Size = new System.Drawing.Size(80, 42);
            this.btn7Zip.TabIndex = 2;
            this.btn7Zip.Text = "7-zip Uncompress";
            this.btn7Zip.UseVisualStyleBackColor = true;
            this.btn7Zip.Click += new System.EventHandler(this.btn7Zip_Click);
            // 
            // btnBuildSummaryData
            // 
            this.btnBuildSummaryData.Location = new System.Drawing.Point(204, 28);
            this.btnBuildSummaryData.Name = "btnBuildSummaryData";
            this.btnBuildSummaryData.Size = new System.Drawing.Size(80, 42);
            this.btnBuildSummaryData.TabIndex = 3;
            this.btnBuildSummaryData.Text = "BuildSummaryData";
            this.btnBuildSummaryData.UseVisualStyleBackColor = true;
            this.btnBuildSummaryData.Click += new System.EventHandler(this.btnBuildSummaryData_Click);
            // 
            // btnGetExcel
            // 
            this.btnGetExcel.Location = new System.Drawing.Point(301, 28);
            this.btnGetExcel.Name = "btnGetExcel";
            this.btnGetExcel.Size = new System.Drawing.Size(80, 42);
            this.btnGetExcel.TabIndex = 4;
            this.btnGetExcel.Text = "GetExcel";
            this.btnGetExcel.UseVisualStyleBackColor = true;
            this.btnGetExcel.Click += new System.EventHandler(this.btnGetExcel_Click);
            // 
            // btnGetSummaryExcel
            // 
            this.btnGetSummaryExcel.Location = new System.Drawing.Point(399, 28);
            this.btnGetSummaryExcel.Name = "btnGetSummaryExcel";
            this.btnGetSummaryExcel.Size = new System.Drawing.Size(80, 42);
            this.btnGetSummaryExcel.TabIndex = 5;
            this.btnGetSummaryExcel.Text = "GetSummaryExcel";
            this.btnGetSummaryExcel.UseVisualStyleBackColor = true;
            this.btnGetSummaryExcel.Click += new System.EventHandler(this.btnGetSummaryExcel_Click);
            // 
            // frmTestExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 578);
            this.Controls.Add(this.btnGetSummaryExcel);
            this.Controls.Add(this.btnGetExcel);
            this.Controls.Add(this.btnBuildSummaryData);
            this.Controls.Add(this.btn7Zip);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnTestList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTestExtract";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestExtract";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestExtract_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestList;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn7Zip;
        private System.Windows.Forms.Button btnBuildSummaryData;
        private System.Windows.Forms.Button btnGetExcel;
        private System.Windows.Forms.Button btnGetSummaryExcel;
    }
}