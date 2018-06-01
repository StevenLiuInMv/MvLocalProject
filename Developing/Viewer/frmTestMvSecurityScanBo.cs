using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvLocalProject.Bo;

namespace MvLocalProject.Viewer
{
    public partial class frmTestMvSecurityScanBo : Form
    {
        public frmTestMvSecurityScanBo()
        {
            InitializeComponent();
        }

        private void btnTestScanAllFiles_Click(object sender, EventArgs e)
        {
            MvSecurityScanBo mssb = new MvSecurityScanBo();
            mssb.executeAllProcess();
            //Environment.Exit(Environment.ExitCode);
        }

        private void btnGetSummaryReport_Click(object sender, EventArgs e)
        {
            MvSecurityScanBo mssb = new MvSecurityScanBo();
            mssb.executeSummaryReport();
        }

        private void frmTestMvSecurityScanBo_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
