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
                    form = new frmReportMocP07();
                    break;
                case "reportPurP17":
                    form = new frmReportPurP17();
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
                case "formBomToMocCompare":
                    form = new frmBomToMocCompare();
                    break;
                case "formErpCreatePR":
                    form = new frmErpCreatePR();
                    break;
                case "formMcBatchJob":
                    form = new frmMcBatchJob();
                    break;
                case "formErpCustomerOrder":
                    form = new frmERPCustomerOrder();
                    break;
                default:
                    break;
            }

            if (form == null) { return; }

            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void frmMainDev_Load(object sender, EventArgs e)
        {
            string localHost = Dns.GetHostName().Split('.')[0];
            bool isFindRole = false;

            // 如果是Admin 全開
            foreach (string host in GlobalConstant.MvAdminPcHostName)
            {
                if (host.Equals(localHost))
                {
                    enableMenuForAdmin();
                    isFindRole = true;
                }
            }

            // MvMis權限
            if (isFindRole == true) { return; }
            foreach (string host in GlobalConstant.MvMisPcHostName)
            {
                if (host.Equals(localHost))
                {
                    enableMenuForMis();
                    isFindRole = true;
                }
            }

            // RD權限
            if (isFindRole == true) { return; }
            foreach (string host in GlobalConstant.MvRdPcHostName)
            {
                if (host.Equals(localHost))
                {
                    enableMenuForRd();
                    isFindRole = true;
                }
            }

            // 料控權限
            if (isFindRole == true) { return; }
            foreach (string host in GlobalConstant.MvMcPcHostName)
            {
                if (host.Equals(localHost))
                {
                    enableMenuForMc();
                    enableMenuForMcSpecial(GlobalMvVariable.MvAdUserName);
                    isFindRole = true;
                }
            }

            // 客服權限
            if (isFindRole == true) { return; }
            foreach (string host in GlobalConstant.MvCsrPcHostName)
            {
                if (host.Equals(localHost))
                {
                    enableMenuForCsr();
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
            // 這個Form預設Visible為False, 因為是舊寫功能, 所以才額外開啟
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
            // 各別group disable
            formBom.Visible = false;
            formBomCompareDev.Visible = false;
            formMoc.Visible = false;
            formBomCompare.Visible = false;
            formBomToMocCompare.Visible = false;
            formErpCreatePR.Visible = false;
            formMcBatchJob.Visible = false;
            formErpCustomerOrder.Visible = false;
        }

        private void enableMenuForRd()
        {
            nbpErpReport.Visible = true;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
            // 各別group disable
            formMcBatchJob.Visible = false;
            formErpCustomerOrder.Visible = false;
        }

        private void enableMenuForMc()
        {
            nbpErpReport.Visible = false;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
            // 各別group disable
            formBom.Visible = false;
            formBomCompareDev.Visible = false;
            formMoc.Visible = false;
            formBomCompare.Visible = false;
            formBomToMocCompare.Visible = false;
            formErpCreatePR.Visible = true;
            formMcBatchJob.Visible = true;
            formErpCustomerOrder.Visible = false;
        }

        private void enableMenuForMcSpecial(string adUserName)
        {
            if (adUserName.Equals("rainyluo", StringComparison.OrdinalIgnoreCase) == false)
            {
                return;
            }
            nbpErpReport.Visible = false;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
            // 各別group disable
            formBom.Visible = true;
            formBomCompareDev.Visible = false; 
            formMoc.Visible = true;
            formBomCompare.Visible = false;
            formBomToMocCompare.Visible = false;
            formErpCreatePR.Visible = true;
            formMcBatchJob.Visible = true;
            formErpCustomerOrder.Visible = false;
        }


        private void enableMenuForCsr()
        {
            nbpErpReport.Visible = false;
            nbgErpFoms.Visible = true;
            nbgItTools.Visible = false;
            nbgTesting.Visible = false;
            // 各別group disable
            formBom.Visible = false;
            formBomCompareDev.Visible = false;
            formMoc.Visible = false;
            formBomCompare.Visible = false;
            formBomToMocCompare.Visible = false;
            formErpCreatePR.Visible = false;
            formMcBatchJob.Visible = false;
            formErpCustomerOrder.Visible = true;
        }


        private void frmMainDev_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
