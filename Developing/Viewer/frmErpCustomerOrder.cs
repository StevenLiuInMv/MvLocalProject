using DevExpress.XtraGrid.Views.Grid;
using MvLocalProject.Controller;
using MvSharedLib.Controller;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmERPCustomerOrder : Form
    {
        public frmERPCustomerOrder()
        {
            InitializeComponent();
        }

        DataTable _MajorDt = null;
        string[] _CustomerOrderTypes = { "A226", "A22A" };
        //DateTime _RedDate = DateTime.Today.AddDays(-3);
        DateTime _YellowDate = DateTime.Today.AddDays(+2);
        DateTime _TempGridRowDate = DateTime.Now;

        private void sbQuery_Click(object sender, EventArgs e)
        {
            _MajorDt = null;

            // 判斷日期
            if (deStart.Text.Length == 0 || deEnd.Text.Length == 0)
            {
                MessageBox.Show("請先選取日期區間");
                return;
            }

            // 判斷start date 不能比end date晚
            DateTime dStart = DateTime.Parse(deStart.Text);
            DateTime dEnd = DateTime.Parse(deEnd.Text);
            if (DateTime.Compare(dStart, dEnd) > 0)
            {
                MessageBox.Show("起始日不可大於結束日");
                return;
            }

            // 判斷區間不要超過一個月
            TimeSpan ts = dEnd - dStart;
            int differenceInDays = ts.Days;
            if (differenceInDays > 180)
            {
                MessageBox.Show("日期區間請不要超過180天");
                return;
            }

            try
            {
                using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
                {
                    connection.Open();
                    //_majorDt = MvDbDao.CollectData_CustomerOrder(connection, _customerOrderTypes, "20200125", "20200523");
                    _MajorDt = MvDbDao.CollectData_CustomerOrder(connection, _CustomerOrderTypes, deStart.DateTime.ToString("yyyyMMdd"), deEnd.DateTime.ToString("yyyyMMdd"));
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Cant fetch data from ERP, Please inform MIS to check this issue");
                return;
            }

            gridControl1.DataSource = _MajorDt;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.RefreshData();

        }

        private void frmERPCustomerOrder_Load(object sender, EventArgs e)
        {
            deStart.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deEnd.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deEnd.DateTime = DateTime.Today;
            deStart.DateTime = DateTime.Parse(DateTime.Today.AddMonths(-4).ToShortDateString());
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string ExpectedDate = View.GetRowCellDisplayText(e.RowHandle, View.Columns["預交日"]);
                _TempGridRowDate = DateTime.ParseExact(ExpectedDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                if (DateTime.Compare(DateTime.Today, _TempGridRowDate) >= 0)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
                else if (DateTime.Compare(_YellowDate, _TempGridRowDate) > 0)
                {
                    e.Appearance.BackColor = Color.YellowGreen;
                    e.Appearance.BackColor2 = Color.Yellow;
                }
            }
        }

        private void frmERPCustomerOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void sbExpandAll_Click(object sender, EventArgs e)
        {
            gridView1.ExpandAllGroups();
        }

        private void sbCollapseAll_Click(object sender, EventArgs e)
        {
            gridView1.CollapseAllGroups();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView View = sender as GridView;
            if (View == null) return;
            try
            {
                Clipboard.SetText(View.GetFocusedDisplayText());
                textBox1.Text = Clipboard.GetText();
            }
            catch (ArgumentNullException)
            {
                Clipboard.Clear();
            }
        }
    }
}
