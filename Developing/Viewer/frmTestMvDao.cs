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
using DevExpress.XtraSplashScreen;

namespace MvLocalProject.Viewer
{
    public partial class frmTestMvDao : Form
    {
        public frmTestMvDao()
        {
            InitializeComponent();
        }

        private void sbBomP07_Click(object sender, EventArgs e)
        {
            string bomId = "21506066V01";
            //bomId = "21511015V02";

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            DataTable dt = new DataTable();

            //dt = MvDbDao.collectData_BomP07_VB6(bomId, false);
            dt = MvDbDao.collectData_BomP07_Thin(bomId);

            gridControl1.DataSource = dt;

            //Close Wait Form
            SplashScreenManager.CloseForm(false);
        }

        private void frmTestMvDao_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
