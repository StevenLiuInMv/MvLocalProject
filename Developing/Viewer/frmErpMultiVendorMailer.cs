using DevExpress.Spreadsheet;
using DevExpress.XtraSplashScreen;
using MvLocalProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmErpMultiVendorMailer : Form
    {
        public frmErpMultiVendorMailer()
        {
            InitializeComponent();
        }

        ArrayList VendorList = new ArrayList();

        private void sbOpenFile_Click(object sender, EventArgs e)
        {
            // 開檔, Parsing 差異料清單
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open an Excel File";
            dialog.Filter = "Excel Files|*.xls;*.xlsx";
            // If the file name is not an empty string open it for saving.  

            string filePathAndName = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName != null)
            { filePathAndName = dialog.FileName; }
            else
            { return; }

            // 讀取檔案內容, 並判斷是否有不合法的料號在內
            SplashScreenManager.ShowDefaultWaitForm();

            // 清空原始 treelist元件
            //clearAllCacheData();
            // initial hash tables

            bool isSuccess = false;
            Workbook workbook = new Workbook();
            ArrayList itemList = new ArrayList();
            DataTable excelToDt = null;

            // 讀取xlsx or xls
            string extensionName = Path.GetExtension(filePathAndName);
            using (FileStream stream = new FileStream(filePathAndName, FileMode.Open))
            {
                if (".xlsx".Equals(extensionName))
                {
                    isSuccess = workbook.LoadDocument(stream, DocumentFormat.Xlsx);
                }
                else
                {
                    isSuccess = workbook.LoadDocument(stream, DocumentFormat.Xls);
                }
            }

            if (isSuccess == false)
            {

                MessageBox.Show("請確認EXCEL檔案是否已解密");
                return;
            }

            // 判斷Sheet只能有一個, 等要release再打開
            if (workbook.Sheets.Count != 1)
            {
                MessageBox.Show(string.Format("請確認EXCEL檔案內只有一個sheet{0}該檔現有個sheets : {1}", Environment.NewLine, workbook.Sheets.Count));
                return;
            }

            // 取得sheet 內容
            Worksheet sheet = workbook.Worksheets[workbook.Sheets.Count - 1];
            Range range = sheet.GetUsedRange();


            // 取得欄位標題資訊
            int j = 4;
            string[] excelHeader = new string[13];
            for (int i = 0; i < excelHeader.Length; i++)
            {
                excelHeader[i] = sheet.GetCellValue(i, j).ToString();
            }

            // 定義column
            excelToDt = CustomStructure.cloneExcelErpVendorMailerDtColumn();
            for (int i = 1; i < range.RowCount; i++)
            {
                j = 0;
                DataRow dr = excelToDt.NewRow();
                dr["MachineID"] = sheet.GetCellValue(j, i);
                dr["ItemNumber"] = sheet.GetCellValue(++j, i);
                dr["ItemName"] = sheet.GetCellValue(++j, i);
                dr["ItemSpec"] = sheet.GetCellValue(++j, i);
                dr["Amount"] = sheet.GetCellValue(++j, i);
                dr["VendorID"] = sheet.GetCellValue(++j, i);
                dr["VendorShortName"] = sheet.GetCellValue(++j, i);
                dr["UnitPrice"] = sheet.GetCellValue(++j, i);
                dr["Price"] = sheet.GetCellValue(++j, i);
                dr["Discount"] = sheet.GetCellValue(++j, i);
                dr["Remark"] = sheet.GetCellValue(++j, i);
                excelToDt.Rows.Add(dr);
            }

            // 取得Distinct的Vendor清單

            var distinctRows = (from DataRow dRow in excelToDt.Rows
                                select new {col1 = dRow["VendorID"]}).Distinct();

            foreach (var item in distinctRows)
            {
                VendorList.Add(item.col1);
            }

            VendorList.Add(distinctRows);

            // 設定各Sheet 權限
            // 不開放編輯功能, 或隱藏欄位等
            treeList1.DataSource = excelToDt;
            //setColumnsCaption(ref excelHeader, ref treeList1);

            //treeList1.BestFitColumns(false);
            treeList1.OptionsView.AutoWidth = false; 
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList1.OptionsBehavior.Editable = false;

            // 命名每個Pages
            xtraTabControl1.TabPages[0].Text = "EXCEL_Vendor";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];

            SplashScreenManager.CloseForm(false);
        }

        private void frmErpMultiVendorMailer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void sbConvert_Click(object sender, EventArgs e)
        {

        }
        // 開發目的
        // 使用mail一次發送指定的供應商, 發送的內容by廠商為一致的內容, 指定料號
        // 1.乙次性發送mail模組
        // 2.繁轉簡的轉碼功能
        // 3.使用的工

    }
}
