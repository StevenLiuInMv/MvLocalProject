using System;
using System.Windows.Forms;
using MvLocalProject.Controller;
using MvLocalProject.Viewer;
using System.Data;

namespace MvLocalProject.Viewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mocP07ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void mocP10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }
        private void bomP09ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void testExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void testProcessBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void bomCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void testMvSecurityScanBoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }



        private void diskScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void testADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }
        private void searchBomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMappingFormAndShowDialog(sender.ToString());
        }

        private void getMappingFormAndShowDialog(string name)
        {
            Form form = null;
            switch (name)
            {
                case "BomP09":
                case "MocP10":
                    form = new frmReport();
                    break;
                case "MocP07":
                    form = new frmReportMocP07();
                    break;
                case "TestMvSecurityScanBo":
                    form = new frmTestMvSecurityScanBo();
                    break;
                case "TestExtract":
                    form = new frmTestExtract();
                    break;
                case "TestProcessBar":
                    form = new frmTestProcessBar();
                    break;
                case "BomCompare":
                    form = new frmBomCompare();
                    break;
                case "DiskScan":
                    form = new frmDiskScan();
                    break;
                case "TestAD":
                    form = new frmTestAD();
                    break;
                case "SearchBom":
                    form = new frmBom();
                    break;
                default:
                    break;
            }

            if (form == null) { return; }
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

    }
}
