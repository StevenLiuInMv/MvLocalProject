using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using MvLocalProject.Controller;
using MvSharedLib.Controller;
using MvSharedLib.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmReportFinance : Form
    {
        public frmReportFinance()
        {
            InitializeComponent();
        }

        private void frmReportFinance_Load(object sender, EventArgs e)
        {
            deEnd.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deEnd.DateTime = DateTime.Today;
            xtraTabControl1.TabPages[0].Text = "AcpP01";
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (deEnd.Text.Length == 0)
            {
                MessageBox.Show("請先選取付款截止日");
                return;
            }

            string aa = deEnd.DateTime.ToString("yyyyMMdd");
            // 取得內容
            DataTable tempDt = null;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection conn = MvDbConnector.getErpDbConnection(MvCompanySite.MACHVISION))
            {
                conn.Open();
                // 取得excel 內容
                sb.Clear();
                sb.Append(" SELECT C.TC004 AS TA004, A.MA003, A.MA027, B.MO006, A.MA028, CAST(Sum(D.TD015) AS numeric(21,0)) AS TA037, C.TC023 AS TA036 ")
                    .Append(" FROM PURMA A LEFT JOIN DSCSYS.dbo.CMSMO B ON A.MA027=B.MO001, ACPTC C, ACPTD D ")
                    .Append("WHERE C.TC001=D.TD001 ")
                    .Append("  AND C.TC002=D.TD002 ")
                    .Append("  AND C.TC004=A.MA001 ")
                    .Append(string.Format("AND C.TC003='{0}' ", deEnd.DateTime.ToString("yyyyMMdd")))
                    .Append("  AND D.TD004=-1 ")
                    .Append("  AND C.TC008='Y' ")
                    .Append("  AND C.TC009='N' ")
                    .Append("  AND (TD008 LIKE '1103-0620%') ")
                    .Append("GROUP BY C.TC004,A.MA003, A.MA027, B.MO006, A.MA028, D.TD008, C.TC023 ")
                    .AppendLine("ORDER BY C.TC004");

                tempDt = MvDbConnector.queryDataBySql(conn, sb.ToString());
            }

            // 重新調整TA036內容
            string matchHead = "008";       // 華南銀行代碼為008開頭
            string bankId = string.Empty;
            long amount = 0;
            if (tempDt != null && tempDt.Rows.Count > 0)
            {
                long div = 0;
                foreach (DataRow dr in tempDt.Rows)
                {
                    bankId = dr["MA027"].ToString();
                    amount = Convert.ToInt64(dr["TA037"]);
                    if (bankId.StartsWith(matchHead) == true)
                    {
                        dr["TA036"] = 0;
                    }
                    else
                    {
                        div = amount / 1000000;
                        dr["TA036"] = (div >= 2) ? (div + 1) * 5 : 10;
                    }
                }
            }

            // 不開放編輯功能, 或隱藏欄位等
            treeList1.DataSource = tempDt;
            treeList1.OptionsView.AutoWidth = false;
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList1.OptionsBehavior.Editable = false;
            setColumnsCaption(ref treeList1);

            // 命名每個Pages
            xtraTabControl1.TabPages[0].Text = "AcpP01";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];

            treeList1.BestFitColumns();
        }

        private void sbExportExcelFile_Click(object sender, EventArgs e)
        {
            exportExcelAction(ref treeList1);
        }


        private void exportExcelAction(ref TreeList treeList)
        {
            // 如果focusd 不是treelist, 就直接跳離
            if (treeList == null) return;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel 活頁簿 |*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName == "") { return; }
            string filePathAndName = saveFileDialog1.FileName;

            try
            {
                treeList.ExportToXlsx(filePathAndName);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, filePathAndName));
                return;
            }
            MessageBox.Show(string.Format("Save as path {0}{1}", Environment.NewLine, filePathAndName));
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void barCopyCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeList treeList = null;

            if (treeList1.Focused == true)
            {
                treeList = treeList1;
            }

            if (treeList == null) return;
            try
            {
                Clipboard.SetText(treeList.FocusedNode.GetDisplayText(treeList.FocusedColumn));
            }
            catch (ArgumentNullException)
            {
                Clipboard.Clear();
            }

            textBox1.Text = Clipboard.GetText();
        }

        private void barExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeList treeList = null;

            if (treeList1.Focused == true)
            {
                treeList = treeList1;
            }

            if (treeList == null) return;
            exportExcelAction(ref treeList);
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠左鍵, 及不在Column上才執行copy cell
            if (e.Button != MouseButtons.Left) { return; }
            TreeListHitInfo hitInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.HitInfoType == HitInfoType.Column) { return; }
            if (treeList1.FocusedColumn == null) { return; }
            barCopyCell_ItemClick(sender, null);
        }

        private void barExportText_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("此功能尚未實作");
        }

        private void setColumnsCaption(ref TreeList tl)
        {
            tl.Columns[0].Caption = "代碼";
            tl.Columns[1].Caption = "公司名稱";
            tl.Columns[2].Caption = "銀行行號";
            tl.Columns[3].Caption = "銀行名稱";
            tl.Columns[4].Caption = "銀行帳號";
            tl.Columns[5].Caption = "金額";
            tl.Columns[6].Caption = "手續費";
        }

        private void frmReportFinance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
