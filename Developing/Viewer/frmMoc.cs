using DevExpress.XtraSplashScreen;
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
    public partial class frmMoc : Form
    {
        public frmMoc()
        {
            InitializeComponent();
        }

        private void sbtnGet_Click(object sender, EventArgs e)
        {
            string mocNo = textEdit1.Text;
            if(textEdit1.Text.IndexOf("-") < 0)
            {
                MessageBox.Show("請輸入正確的製令單號" + Environment.NewLine + "Ex : A511-20180500001");
                return;
            }

            SplashScreenManager.ShowDefaultWaitForm();
            DataTable sourceDt = MvDbDao.collectData_Moc(mocNo);
            treeList1.DataSource = sourceDt;
            // 不開放編輯功能
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList1.OptionsBehavior.Editable = false;
            treeList1.OptionsView.AutoWidth = false;
            // 只要最後一列設定BestFit即可
            treeList1.Columns[treeList1.Columns.Count - 1].BestFit();
            SplashScreenManager.CloseForm(false);
        }

        private void frmMoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
