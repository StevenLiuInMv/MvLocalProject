using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvLocalProject.Controller;
using MvLocalProject.Model;
using System.Net;

namespace MvLocalProject.Viewer
{
    public partial class frmMainDev : Form
    {
        public frmMainDev()
        {
            InitializeComponent();
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            string s = e.Link.ItemName;
            getMappingFormAndShowDialog(s);
        }


        private void getMappingFormAndShowDialog(string name)
        {
            Form form = null;
            switch (name)
            {
                case "reportBomP09":
                case "reportMocP10":
                    form = new frmReport();
                    break;
                case "reportMocP07":
                    form = new frmMocP07();
                    break;
                case "reportPurP17":
                    form = new frmPurP17();
                    break;
                case "toolTestMvSecurityScanBo":
                    form = new frmTestMvSecurityScanBo();
                    break;
                case "toolTestExtract":
                    form = new frmTestExtract();
                    break;
                case "toolTestProcessBar":
                    form = new frmTestProcessBar();
                    break;
                case "toolTestDiskScan":
                    form = new frmDiskScan();
                    break;
                case "toolTestAD":
                    form = new frmTestAD();
                    break;
                case "formBomCompare":
                    form = new frmBomCompare();
                    break;
                case "formBom":
                    form = new frmBom();
                    break;
                case "formBomCompareDev":
                    form = new frmBomCompareDev();
                    break;
                case "formMoc":
                    form = new frmMoc();
                    break;
                case "toolItCisco":
                    form = new frmItCisco();
                    break;
                case "toolItMxMail":
                    form = new frmItMxMail();
                    break;
                default:
                    break;
            }

            if (form == null) { return; }

            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void tileBarItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void tileBar1_Click(object sender, EventArgs e)
        {

        }

        private void frmMainDev_Load(object sender, EventArgs e)
        {
            string localHost = Dns.GetHostName().Split('.')[0];
            bool isFindRole = false;

            // 如果是Admin 全開
            foreach (string misHost in DefinedParameter.MvAdminPcHostName)
            {
                if (misHost.Equals(localHost))
                {
                    enableMenuForAdmin();
                    isFindRole = true;
                }
            }

            // MvMis權限
            if (isFindRole == true) { return; }
            foreach (string misHost in DefinedParameter.MvMisPcHostName)
            {
                if (misHost.Equals(localHost))
                {
                    enableMenuForMis();
                    isFindRole = true;
                }
            }

            // RD權限
            if (isFindRole == true) { return; }
            foreach (string misHost in DefinedParameter.MvRdPcHostName)
            {
                if (misHost.Equals(localHost))
                {
                    enableMenuForRd();
                    isFindRole = true;
                }
            }

            // Normal User權限
            if (isFindRole == false)
            {
                enableMenuForNormalUser();
            }
        }

        private void enableMenuForAdmin()
        {
            nbpErpReport.Visible = true;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = true;
            nbgTesting.Visible = true;
            formBomCompare.Visible = true;
        }

        private void enableMenuForMis()
        {
            nbpErpReport.Visible = false;
            nbgErpFoms.Visible = false;
            nbgItTools.Visible = true;
            nbgTesting.Visible = false;
        }

        private void enableMenuForNormalUser()
        {
            nbpErpReport.Visible = false;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
            formBom.Visible = false;
            formBomCompareDev.Visible = false;
        }

        private void enableMenuForRd()
        {
            nbpErpReport.Visible = true;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
        }

        private void frmMainDev_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
