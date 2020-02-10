namespace Testing
{
    partial class frmDiskIO
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
            this.rtb_ListDriveInfo = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_ListDriveInfo
            // 
            this.rtb_ListDriveInfo.Location = new System.Drawing.Point(24, 63);
            this.rtb_ListDriveInfo.Name = "rtb_ListDriveInfo";
            this.rtb_ListDriveInfo.Size = new System.Drawing.Size(508, 260);
            this.rtb_ListDriveInfo.TabIndex = 2;
            this.rtb_ListDriveInfo.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "單磁碟";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "單主機";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 37);
            this.button3.TabIndex = 5;
            this.button3.Text = "GetOutlookPst";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(231, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 37);
            this.button4.TabIndex = 6;
            this.button4.Text = "GetOutlookVersion";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmDiskIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 340);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtb_ListDriveInfo);
            this.Name = "frmDiskIO";
            this.Text = "frmDiskIO";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_ListDriveInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}