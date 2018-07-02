namespace MvLocalProject.Viewer
{
    partial class frmTestMvSecurityScanBo
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
            this.btnTestScanAllFiles = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnGetSummaryReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestScanAllFiles
            // 
            this.btnTestScanAllFiles.Location = new System.Drawing.Point(12, 12);
            this.btnTestScanAllFiles.Name = "btnTestScanAllFiles";
            this.btnTestScanAllFiles.Size = new System.Drawing.Size(76, 39);
            this.btnTestScanAllFiles.TabIndex = 0;
            this.btnTestScanAllFiles.Text = "ScanAllFiles";
            this.btnTestScanAllFiles.UseVisualStyleBackColor = true;
            this.btnTestScanAllFiles.Click += new System.EventHandler(this.btnTestScanAllFiles_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 57);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(581, 259);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // btnGetSummaryReport
            // 
            this.btnGetSummaryReport.Location = new System.Drawing.Point(94, 12);
            this.btnGetSummaryReport.Name = "btnGetSummaryReport";
            this.btnGetSummaryReport.Size = new System.Drawing.Size(76, 39);
            this.btnGetSummaryReport.TabIndex = 2;
            this.btnGetSummaryReport.Text = "GetSummaryReport";
            this.btnGetSummaryReport.UseVisualStyleBackColor = true;
            this.btnGetSummaryReport.Click += new System.EventHandler(this.btnGetSummaryReport_Click);
            // 
            // frmTestMvSecurityScanBo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 328);
            this.Controls.Add(this.btnGetSummaryReport);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnTestScanAllFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestMvSecurityScanBo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestMvSecurityScanBo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestMvSecurityScanBo_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestScanAllFiles;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnGetSummaryReport;
    }
}