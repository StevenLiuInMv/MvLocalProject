namespace MvLocalProject.Viewer
{
    partial class frmItCisco
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnGetPorts = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.teIp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tePort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnGetPortInfo = new System.Windows.Forms.Button();
            this.btnDisablePort = new System.Windows.Forms.Button();
            this.btnEnablePort = new System.Windows.Forms.Button();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teStatus = new DevExpress.XtraEditors.TextEdit();
            this.btnDebugGetPorts = new System.Windows.Forms.Button();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.groupMacEdit = new DevExpress.XtraEditors.GroupControl();
            this.btnDelMac = new System.Windows.Forms.Button();
            this.btnAddMac = new System.Windows.Forms.Button();
            this.teMacAddress = new DevExpress.XtraEditors.TextEdit();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupMacNumberEdit = new DevExpress.XtraEditors.GroupControl();
            this.btnSetMacNumber = new System.Windows.Forms.Button();
            this.teMacNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnGetPortsIncludeMac = new System.Windows.Forms.Button();
            this.btnGetPortsFromDB = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMacEdit)).BeginInit();
            this.groupMacEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teMacAddress.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupMacNumberEdit)).BeginInit();
            this.groupMacNumberEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teMacNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(557, 29);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 113);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // btnGetPorts
            // 
            this.btnGetPorts.Location = new System.Drawing.Point(1035, 1);
            this.btnGetPorts.Name = "btnGetPorts";
            this.btnGetPorts.Size = new System.Drawing.Size(65, 32);
            this.btnGetPorts.TabIndex = 4;
            this.btnGetPorts.Text = "GetPorts";
            this.btnGetPorts.UseVisualStyleBackColor = true;
            this.btnGetPorts.Visible = false;
            this.btnGetPorts.Click += new System.EventHandler(this.btnGetPorts_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 144);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1162, 498);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.cardView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // cardView1
            // 
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.gridControl1;
            this.cardView1.Name = "cardView1";
            // 
            // teIp
            // 
            this.teIp.Location = new System.Drawing.Point(51, 25);
            this.teIp.Name = "teIp";
            this.teIp.Size = new System.Drawing.Size(153, 20);
            this.teIp.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(15, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "IP:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Port:";
            // 
            // tePort
            // 
            this.tePort.Location = new System.Drawing.Point(51, 45);
            this.tePort.Name = "tePort";
            this.tePort.Size = new System.Drawing.Size(153, 20);
            this.tePort.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Name:";
            // 
            // teName
            // 
            this.teName.Location = new System.Drawing.Point(51, 65);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(153, 20);
            this.teName.TabIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnGetPortInfo);
            this.groupControl1.Controls.Add(this.btnDisablePort);
            this.groupControl1.Controls.Add(this.btnEnablePort);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.teStatus);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.teName);
            this.groupControl1.Controls.Add(this.teIp);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.tePort);
            this.groupControl1.Location = new System.Drawing.Point(12, 29);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(270, 113);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Cisco Information";
            // 
            // btnGetPortInfo
            // 
            this.btnGetPortInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnGetPortInfo.Location = new System.Drawing.Point(210, 81);
            this.btnGetPortInfo.Name = "btnGetPortInfo";
            this.btnGetPortInfo.Size = new System.Drawing.Size(52, 28);
            this.btnGetPortInfo.TabIndex = 16;
            this.btnGetPortInfo.Text = "Info";
            this.btnGetPortInfo.UseVisualStyleBackColor = false;
            this.btnGetPortInfo.Click += new System.EventHandler(this.btnGetMac_Click);
            // 
            // btnDisablePort
            // 
            this.btnDisablePort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnDisablePort.Location = new System.Drawing.Point(210, 52);
            this.btnDisablePort.Name = "btnDisablePort";
            this.btnDisablePort.Size = new System.Drawing.Size(52, 29);
            this.btnDisablePort.TabIndex = 15;
            this.btnDisablePort.Text = "Disable";
            this.btnDisablePort.UseVisualStyleBackColor = false;
            this.btnDisablePort.Click += new System.EventHandler(this.btnDisablePort_Click);
            // 
            // btnEnablePort
            // 
            this.btnEnablePort.Location = new System.Drawing.Point(210, 23);
            this.btnEnablePort.Name = "btnEnablePort";
            this.btnEnablePort.Size = new System.Drawing.Size(52, 29);
            this.btnEnablePort.TabIndex = 14;
            this.btnEnablePort.Text = "Enable";
            this.btnEnablePort.UseVisualStyleBackColor = true;
            this.btnEnablePort.Click += new System.EventHandler(this.btnEnablePort_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 14);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Status:";
            // 
            // teStatus
            // 
            this.teStatus.Location = new System.Drawing.Point(51, 85);
            this.teStatus.Name = "teStatus";
            this.teStatus.Size = new System.Drawing.Size(153, 20);
            this.teStatus.TabIndex = 12;
            // 
            // btnDebugGetPorts
            // 
            this.btnDebugGetPorts.Location = new System.Drawing.Point(1109, -2);
            this.btnDebugGetPorts.Name = "btnDebugGetPorts";
            this.btnDebugGetPorts.Size = new System.Drawing.Size(65, 35);
            this.btnDebugGetPorts.TabIndex = 13;
            this.btnDebugGetPorts.Text = "DebugGetPorts";
            this.btnDebugGetPorts.UseVisualStyleBackColor = true;
            this.btnDebugGetPorts.Visible = false;
            this.btnDebugGetPorts.Click += new System.EventHandler(this.btnDebugGetPorts_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(557, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(84, 14);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Debug Output:";
            // 
            // groupMacEdit
            // 
            this.groupMacEdit.Controls.Add(this.btnDelMac);
            this.groupMacEdit.Controls.Add(this.btnAddMac);
            this.groupMacEdit.Controls.Add(this.teMacAddress);
            this.groupMacEdit.Location = new System.Drawing.Point(288, 29);
            this.groupMacEdit.Name = "groupMacEdit";
            this.groupMacEdit.Size = new System.Drawing.Size(263, 62);
            this.groupMacEdit.TabIndex = 17;
            this.groupMacEdit.Text = "Edit Mac address";
            // 
            // btnDelMac
            // 
            this.btnDelMac.Location = new System.Drawing.Point(214, 31);
            this.btnDelMac.Name = "btnDelMac";
            this.btnDelMac.Size = new System.Drawing.Size(44, 22);
            this.btnDelMac.TabIndex = 18;
            this.btnDelMac.Text = "Del";
            this.btnDelMac.UseVisualStyleBackColor = true;
            this.btnDelMac.Click += new System.EventHandler(this.btnDelMac_Click);
            // 
            // btnAddMac
            // 
            this.btnAddMac.Location = new System.Drawing.Point(164, 31);
            this.btnAddMac.Name = "btnAddMac";
            this.btnAddMac.Size = new System.Drawing.Size(44, 22);
            this.btnAddMac.TabIndex = 17;
            this.btnAddMac.Text = "Add";
            this.btnAddMac.UseVisualStyleBackColor = true;
            this.btnAddMac.Click += new System.EventHandler(this.btnAddMac_Click);
            // 
            // teMacAddress
            // 
            this.teMacAddress.Location = new System.Drawing.Point(5, 32);
            this.teMacAddress.Name = "teMacAddress";
            this.teMacAddress.Size = new System.Drawing.Size(153, 20);
            this.teMacAddress.TabIndex = 17;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1183, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(398, 17);
            this.toolStripStatusLabel1.Text = "因使用非同步寫法, 在操作Enable及Disable的動作, 請等後2秒後再執行 !!!";
            // 
            // groupMacNumberEdit
            // 
            this.groupMacNumberEdit.Controls.Add(this.btnSetMacNumber);
            this.groupMacNumberEdit.Controls.Add(this.teMacNumber);
            this.groupMacNumberEdit.Location = new System.Drawing.Point(288, 90);
            this.groupMacNumberEdit.Name = "groupMacNumberEdit";
            this.groupMacNumberEdit.Size = new System.Drawing.Size(263, 52);
            this.groupMacNumberEdit.TabIndex = 19;
            this.groupMacNumberEdit.Text = "Edit mac number by Port";
            // 
            // btnSetMacNumber
            // 
            this.btnSetMacNumber.Location = new System.Drawing.Point(164, 23);
            this.btnSetMacNumber.Name = "btnSetMacNumber";
            this.btnSetMacNumber.Size = new System.Drawing.Size(44, 22);
            this.btnSetMacNumber.TabIndex = 18;
            this.btnSetMacNumber.Text = "Set";
            this.btnSetMacNumber.UseVisualStyleBackColor = true;
            this.btnSetMacNumber.Click += new System.EventHandler(this.btnSetMacNumber_Click);
            // 
            // teMacNumber
            // 
            this.teMacNumber.Location = new System.Drawing.Point(5, 24);
            this.teMacNumber.Name = "teMacNumber";
            this.teMacNumber.Size = new System.Drawing.Size(153, 20);
            this.teMacNumber.TabIndex = 17;
            // 
            // btnGetPortsIncludeMac
            // 
            this.btnGetPortsIncludeMac.Location = new System.Drawing.Point(964, 1);
            this.btnGetPortsIncludeMac.Name = "btnGetPortsIncludeMac";
            this.btnGetPortsIncludeMac.Size = new System.Drawing.Size(65, 32);
            this.btnGetPortsIncludeMac.TabIndex = 20;
            this.btnGetPortsIncludeMac.Text = "GetPortsIncludeMac";
            this.btnGetPortsIncludeMac.UseVisualStyleBackColor = true;
            this.btnGetPortsIncludeMac.Visible = false;
            this.btnGetPortsIncludeMac.Click += new System.EventHandler(this.btnGetPortsIncludeMac_Click);
            // 
            // btnGetPortsFromDB
            // 
            this.btnGetPortsFromDB.Location = new System.Drawing.Point(893, 1);
            this.btnGetPortsFromDB.Name = "btnGetPortsFromDB";
            this.btnGetPortsFromDB.Size = new System.Drawing.Size(65, 32);
            this.btnGetPortsFromDB.TabIndex = 21;
            this.btnGetPortsFromDB.Text = "GetPortsFromDB";
            this.btnGetPortsFromDB.UseVisualStyleBackColor = true;
            this.btnGetPortsFromDB.Visible = false;
            this.btnGetPortsFromDB.Click += new System.EventHandler(this.btnGetPortsFromDB_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(822, 1);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(65, 32);
            this.btnExportToExcel.TabIndex = 22;
            this.btnExportToExcel.Text = "ExportToExcel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Visible = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // frmItCisco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 667);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.btnGetPortsFromDB);
            this.Controls.Add(this.btnGetPortsIncludeMac);
            this.Controls.Add(this.groupMacNumberEdit);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupMacEdit);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnDebugGetPorts);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnGetPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmItCisco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmItCisco";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmItCisco_FormClosed);
            this.Load += new System.EventHandler(this.frmItCisco_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMacEdit)).EndInit();
            this.groupMacEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teMacAddress.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupMacNumberEdit)).EndInit();
            this.groupMacNumberEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teMacNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnGetPorts;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.TextEdit teIp;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tePort;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Button btnDisablePort;
        private System.Windows.Forms.Button btnEnablePort;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teStatus;
        private System.Windows.Forms.Button btnDebugGetPorts;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.Button btnGetPortInfo;
        private DevExpress.XtraEditors.GroupControl groupMacEdit;
        private System.Windows.Forms.Button btnDelMac;
        private System.Windows.Forms.Button btnAddMac;
        private DevExpress.XtraEditors.TextEdit teMacAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraEditors.GroupControl groupMacNumberEdit;
        private System.Windows.Forms.Button btnSetMacNumber;
        private DevExpress.XtraEditors.TextEdit teMacNumber;
        private System.Windows.Forms.Button btnGetPortsIncludeMac;
        private System.Windows.Forms.Button btnGetPortsFromDB;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btnExportToExcel;
    }
}