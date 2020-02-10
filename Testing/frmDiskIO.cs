using MvSharedLib.Checker;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Testing
{
    public partial class frmDiskIO : Form
    {
        public frmDiskIO()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string diskPath = @"D:";
            rtb_ListDriveInfo.Text = "";
            rtb_ListDriveInfo.Text = Utility.ShowSubDirectoriesSizeInfo(diskPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            StringBuilder sb = new StringBuilder();

            rtb_ListDriveInfo.Text = "";

            foreach (DriveInfo d in allDrives)
            {
                sb.AppendLine(string.Format("Drive {0}", d.Name));
                sb.AppendLine(string.Format("Drive type: {0}", d.DriveType));
                if (d.IsReady == true)
                {
                    sb.AppendLine(Utility.ShowSubDirectoriesSizeInfo(d.Name));
                    sb.AppendLine("======================================");
                }
            }
            rtb_ListDriveInfo.Text = sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CollectOutlookPSTPathStatus status = CollectOutlookPSTPathStatus.StartGetVersion;
            OfficeCheck officeCheck = new OfficeCheck();

            status = officeCheck.ExceuteCollectOutlookPSTPath();
            MessageBox.Show("status = " + status);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int version = OfficeCheck.GetMSOutlookVersion();
            MessageBox.Show("Version = " + version);
        }
    }
}
