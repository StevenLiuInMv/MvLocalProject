using MvLocalProject.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmReportPurP17 : Form
    {
        public frmReportPurP17()
        {
            InitializeComponent();
        }

        private void sbtnGet_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = MvDbDao.collectData_PurP17_VB6("20180508");
            treeList1.DataSource = dt;

        }

        private void frmPurP17_Load(object sender, EventArgs e)
        {

        }

        private void frmPurP17_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
