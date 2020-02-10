namespace MvLocalProject.Viewer
{
    partial class frmDiskScan
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
            this.btnCheckDisk = new System.Windows.Forms.Button();
            this.rtb_ListDriveInfo = new System.Windows.Forms.RichTextBox();
            this.btnScanFiles = new System.Windows.Forms.Button();
            this.btnScanFilesForAllDisk = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckDisk
            // 
            this.btnCheckDisk.Location = new System.Drawing.Point(31, 27);
            this.btnCheckDisk.Name = "btnCheckDisk";
            this.btnCheckDisk.Size = new System.Drawing.Size(75, 36);
            this.btnCheckDisk.TabIndex = 0;
            this.btnCheckDisk.Text = "CheckDisk";
            this.btnCheckDisk.UseVisualStyleBackColor = true;
            this.btnCheckDisk.Click += new System.EventHandler(this.btnCheckDisk_Click);
            // 
            // rtb_ListDriveInfo
            // 
            this.rtb_ListDriveInfo.Location = new System.Drawing.Point(31, 69);
            this.rtb_ListDriveInfo.Name = "rtb_ListDriveInfo";
            this.rtb_ListDriveInfo.Size = new System.Drawing.Size(588, 260);
            this.rtb_ListDriveInfo.TabIndex = 1;
            this.rtb_ListDriveInfo.Text = "";
            // 
            // btnScanFiles
            // 
            this.btnScanFiles.Location = new System.Drawing.Point(121, 28);
            this.btnScanFiles.Name = "btnScanFiles";
            this.btnScanFiles.Size = new System.Drawing.Size(71, 35);
            this.btnScanFiles.TabIndex = 2;
            this.btnScanFiles.Text = "ScanFiles";
            this.btnScanFiles.UseVisualStyleBackColor = true;
            this.btnScanFiles.Click += new System.EventHandler(this.btnScanFiles_Click);
            // 
            // btnScanFilesForAllDisk
            // 
            this.btnScanFilesForAllDisk.Location = new System.Drawing.Point(198, 28);
            this.btnScanFilesForAllDisk.Name = "btnScanFilesForAllDisk";
            this.btnScanFilesForAllDisk.Size = new System.Drawing.Size(71, 35);
            this.btnScanFilesForAllDisk.TabIndex = 3;
            this.btnScanFilesForAllDisk.Text = "ScanFileForAll";
            this.btnScanFilesForAllDisk.UseVisualStyleBackColor = true;
            this.btnScanFilesForAllDisk.Click += new System.EventHandler(this.btnScanFilesForAllDisk_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(284, 28);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(71, 35);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(375, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(465, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "單磁碟";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(545, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 33);
            this.button3.TabIndex = 7;
            this.button3.Text = "單主機";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmDiskScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 366);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnScanFilesForAllDisk);
            this.Controls.Add(this.btnScanFiles);
            this.Controls.Add(this.rtb_ListDriveInfo);
            this.Controls.Add(this.btnCheckDisk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDiskScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDiskScan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDiskScan_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckDisk;
        private System.Windows.Forms.RichTextBox rtb_ListDriveInfo;
        private System.Windows.Forms.Button btnScanFiles;
        private System.Windows.Forms.Button btnScanFilesForAllDisk;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}