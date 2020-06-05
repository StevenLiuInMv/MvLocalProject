using DevExpress.XtraSplashScreen;
using DevExpress.Spreadsheet;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using MvLocalProject.Controller;
using MvLocalProject.Bo;
using MvLocalProject.Model;
using DevExpress.XtraTreeList;
using System.Data.SqlClient;
using MvSharedLib.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmErpCreatePR : Form
    {
        public frmErpCreatePR()
        {
            InitializeComponent();
        }

        DataTable cachePrDt = null;
        Hashtable[] hashTreeListBackColor = new Hashtable[3];

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
            clearAllCacheData();
            // initial hash tables

            bool isSuccess = false;
            Workbook workbook = new Workbook();
            ArrayList itemList = new ArrayList();
            DataTable excelToDt = null;
            DataTable tempItemDt = null;

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
            excelToDt = CustomStructure.cloneExcelPrDtColumn();

            // 取得欄位內容
            // 判斷工號必需要完全相同
            string empNo = string.Empty;
            for (int i = 5; i <= range.RowCount; i++)
            {
                j = 0;
                DataRow dr = excelToDt.NewRow();
                dr["TB043"] = sheet.GetCellValue(j, i);
                dr["TA012"] = sheet.GetCellValue(++j, i);
                if (i == 5)
                {
                    empNo = dr["TA012"].ToString();
                }
                if (empNo.Length == 0)
                {
                    MessageBox.Show("請購人員不允許為空值");
                    return;
                }
                dr["TB004"] = sheet.GetCellValue(++j, i);
                // 判斷品號為空值時, 停止再向下parsing
                if (sheet.GetCellValue(j, i).IsEmpty == true)
                {
                    break;
                }
                // 當判斷品號不為空值時, 加判斷工號是否相同
                // 如不相同, 也不允許再往下執行
                if (empNo.Equals(dr["TA012"].ToString()) == false)
                {
                    MessageBox.Show("該Excel內含2組以上的工號, 不允許匯入作業");
                    return;
                }
                itemList.Add(dr["TB004"]);
                dr["TB005"] = sheet.GetCellValue(++j, i);
                dr["TB006"] = sheet.GetCellValue(++j, i);
                dr["TB009"] = sheet.GetCellValue(++j, i);
                dr["TB008"] = sheet.GetCellValue(++j, i);
                dr["TB011"] = sheet.GetCellValue(++j, i);
                dr["TB010"] = sheet.GetCellValue(++j, i);
                dr["TB012"] = sheet.GetCellValue(++j, i);
                dr["TB029"] = sheet.GetCellValue(++j, i);
                dr["TB201"] = sheet.GetCellValue(++j, i);
                dr["TA202"] = sheet.GetCellValue(++j, i);
                excelToDt.Rows.Add(dr);
            }

            // 判斷料號是否有不存在DB的資料
            // 如果有, 存入cacheillegalItem, 後續顯示訊息判斷使用
            tempItemDt = MvDbDao.checkData_hasIllegalItemListInInvmb((string[])itemList.ToArray(typeof(string)));
            // 依判斷是否有非合法的Item執行後續判斷
            // 即便有不合法的Item, 介面還是要顯示出來, 因為要提供使用者檢查使用
            // 判斷excel轉成data table後的欄位正確性查檢
            // 只有有問題的時候, 才需要取得BackColorList
            bool isIllegal = checkDataValidAndSetBackColor(excelToDt, tempItemDt, ref hashTreeListBackColor[0]);
            if (tempItemDt != null && tempItemDt.Rows.Count >= 1 || isIllegal == true)
            {
                // 判斷excel轉成data table後的欄位正確性查檢
                // 只有有問題的時候, 才需要取得BackColorList
                // checkDataValidAndSetBackColor(dt, tempItemDt, ref hashTreeListBackColor[0]);
                MessageBox.Show("部份料號不存在DB, 或Excel內容有不正確欄位");
            }
            else
            {
                cachePrDt = excelToDt.Copy();
            }
            // 設定各Sheet 權限
            // 不開放編輯功能, 或隱藏欄位等
            treeList1.DataSource = excelToDt;
            setColumnsCaption(ref excelHeader, ref treeList1);

            //treeList1.Columns["MB004"].Visible = false;

            treeList1.BestFitColumns();
            treeList1.OptionsView.AutoWidth = false;
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList1.OptionsBehavior.Editable = false;

            // 命名每個Pages
            xtraTabControl1.TabPages[0].Text = "EXCEL_PR";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];

            SplashScreenManager.CloseForm(false);
        }

        private void clearAllCacheData()
        {
            cachePrDt = null;
            treeList1.DataSource = null;
            treeList1.DataBindings.Clear();
            treeList1.Columns.Clear();
        }

        private void setColumnsCaption(ref string[] header, ref TreeList tl)
        {
            for (int i = 0; i < header.Length; i++)
            {
                tl.Columns[i].Caption = header[i];
            }
        }

        private bool checkDataValidAndSetBackColor(DataTable sourceDt, DataTable illegalDt, ref Hashtable hashTreeListBackColor)
        {

            if (illegalDt == null || illegalDt.Rows.Count == 0)
            {
                return false;
            }

            int changeColorIndex = 0;
            bool hasIllealRow = false;
            foreach (DataRow dr in sourceDt.Rows)
            {
                foreach (DataRow illDr in illegalDt.Rows)
                {
                    if (dr["TB004"].Equals(illDr["MB001"]))
                    {
                        hashTreeListBackColor[changeColorIndex] = true;
                        hasIllealRow = true;
                        break;
                    }
                }
                changeColorIndex++;
            }
            return hasIllealRow;
        }

        private void frmErpCreatePR_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }
            getErpNoteList();
        }


        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[0].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.LightPink;
                e.Appearance.ForeColor = Color.DarkGreen;
            }
        }

        private void sbCreateNote_Click(object sender, EventArgs e)
        {
            if (cachePrDt == null)
            {
                MessageBox.Show("請先確認已有選取正確的請購單匯入檔案");
                return;
            }

            string result = cboErpNotePr.SelectedValue == null ? "" : cboErpNotePr.SelectedValue.ToString();
            if (result.Length == 0)
            {
                MessageBox.Show("請先選取請購單別");
                return;
            }

            // 轉換單別
            ErpNoteHead erpNoteHead = ErpNoteHead.None;
            erpNoteHead = MvErpNoteBo.getEprNoteEnum("H_" + result);
            if (erpNoteHead == ErpNoteHead.None)
            {
                MessageBox.Show("不支援開立此單別, 請找MIS人員提供規格並建置");
                return;
            }

            DataTable tempDt = null;
            // 暫時, debug用
            //GlobalMvVariable.MvAdUserName = "tinalee";
            // 暫時, debug用
            string erpUserGroup = string.Empty;
            DataSet tempDs = null;

            // 匯入PR
            // 測試用
            GlobalMvVariable.MvAdUserName = "rainyluo";
            string noteNumber = string.Empty;
            using (SqlConnection conn = MvDbConnector.Connection_ERPDB2_Dot_MVTEST)
            {
                conn.Open();
                noteNumber = MvErpNoteBo.getMaxErpNote(conn, erpNoteHead);
            }
            // 測試用

            SplashScreenManager.ShowDefaultWaitForm();
            using (SqlConnection conn = MvDbConnector.getErpDbConnection(GlobalMvVariable.MvAdCompany))
            {
                conn.Open();
                // 取得ad帳號及user group
                // 這裡取的其實是deptId, 但因為William哥的VB程式匯入已經都是匯入deptId了
                // 就先依照舊規則匯入, 之後如果要再調整, 再改取別的欄位即可
                string tempSql = string.Format("SELECT A.MF004,A.MF005,B.MG006,B.MG004 FROM ADMMF A, ADMMG B WHERE A.MF001=B.MG001 AND MF001='{0}' AND B.MG002='PURA01K' ORDER BY MG002 ", GlobalMvVariable.MvAdUserName);

                tempDt = MvDbConnector.queryDataBySql(conn, tempSql);
                if (tempDt == null || tempDt.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("你沒有操作開立PR的權限{0}UserName : {1}", Environment.NewLine, GlobalMvVariable.MvAdUserName));
                    return;
                }

                erpUserGroup = tempDt.Rows[0]["MF004"].ToString();

                //tempDs = MvErpNoteBo.createErpNote_PR(conn, erpNoteHead, cachePrDt, DateTime.Today, GlobalMvVariable.MvAdUserName, erpUserGroup);
                // 測試用
                tempDs = MvErpNoteBo.createErpNote_PR(GlobalMvVariable.MvAdCompany, conn, erpNoteHead, cachePrDt, DateTime.Today, GlobalMvVariable.MvAdUserName, erpUserGroup, noteNumber);
                // 測試用
            }
            // 取出來比對內容
            if (tempDs.Tables.Count < 2)
            {
                MessageBox.Show("產生PR Fail, 請重新操作");
                clearAllCacheData();
                return;
            }

            // 確認沒有問題, 再重新至DB撈出來並顯示在Grid上, 確保資料已進DB
            DataRow tempDr = tempDs.Tables["PURTA"].Rows[0];

            textBox1.Text = tempDr["TA001"].ToString();
            textBox2.Text = tempDr["TA002"].ToString();
            textBox3.Text = tempDr["TA006"].ToString();
            textBox4.Text = tempDr["TA004"].ToString();
            textBox5.Text = tempDr["TA012"].ToString();
            textBox6.Text = tempDr["TA013"].ToString();
            textBox7.Text = tempDr["TA200"].ToString();

            // 不開放編輯功能, 或隱藏欄位等
            treeList2.DataSource = tempDs.Tables["PURTA"];
            hidePurtaColumns(ref treeList2);
            treeList2.OptionsView.AutoWidth = false;
            treeList2.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.Editable = false;

            treeList3.DataSource = tempDs.Tables["PURTB"];
            hidePurtbColumns(ref treeList3);
            treeList3.OptionsView.AutoWidth = false;
            treeList3.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.Editable = false;

            // 命名每個Pages
            xtraTabControl1.TabPages[1].Text = "Imported PR";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[1];

            treeList2.BestFitColumns();
            treeList3.BestFitColumns();
            SplashScreenManager.CloseForm(false);
        }

        private void getErpNoteList()
        {
            using (SqlConnection conn =  MvDbConnector.getErpDbConnection(GlobalMvVariable.MvAdCompany))
            {
                DataTable tempDt = null;
                conn.Open();
                string tempSql = "SELECT MQ001, MQ001+' '+MQ002 NOTE FROM CMSMQ WHERE MQ003='31' ORDER BY MQ001";

                tempDt = MvDbConnector.queryDataBySql(conn, tempSql);
                if (tempDt == null || tempDt.Rows.Count == 0)
                {
                    MessageBox.Show("取得表單資料有問題");
                    return;
                }

                cboErpNotePr.DataSource = tempDt;
                cboErpNotePr.ValueMember = "MQ001";
                cboErpNotePr.DisplayMember = "NOTE";
                cboErpNotePr.Text = "";
                cboErpNotePr.SelectedValue = "";
            }
        }
        private void hidePurtaColumns(ref TreeList treelist)
        {
            treelist.Columns["MODIFIER"].Visible = false;
            treelist.Columns["MODI_DATE"].Visible = false;
            treelist.Columns["FLAG"].Visible = false;
            treelist.Columns["TA005"].Visible = false;
            treelist.Columns["TA007"].Visible = false;
            treelist.Columns["TA007"].Visible = false;
            treelist.Columns["TA008"].Visible = false;
            treelist.Columns["TA009"].Visible = false;
            treelist.Columns["TA010"].Visible = false;
            treelist.Columns["TA014"].Visible = false;
            treelist.Columns["TA015"].Visible = false;
            treelist.Columns["TA016"].Visible = false;
            treelist.Columns["TA017"].Visible = false;
            treelist.Columns["TA018"].Visible = false;
            treelist.Columns["TA019"].Visible = false;
            treelist.Columns["TA021"].Visible = false;
            treelist.Columns["TA022"].Visible = false;
            treelist.Columns["TA023"].Visible = false;
            treelist.Columns["TA024"].Visible = false;
            treelist.Columns["TA025"].Visible = false;
            treelist.Columns["TA026"].Visible = false;
            treelist.Columns["TA027"].Visible = false;
            treelist.Columns["TA028"].Visible = false;
            treelist.Columns["TA200"].Visible = false;
            treelist.Columns["TA201"].Visible = false;
            treelist.Columns["TA202"].Visible = false;
            treelist.Columns["TA203"].Visible = false;
            treelist.Columns["TA550"].Visible = false;
            treelist.Columns["UDF01"].Visible = false;
            treelist.Columns["UDF02"].Visible = false;
            treelist.Columns["UDF03"].Visible = false;
            treelist.Columns["UDF04"].Visible = false;
            treelist.Columns["UDF05"].Visible = false;
            treelist.Columns["UDF06"].Visible = false;
            treelist.Columns["UDF07"].Visible = false;
            treelist.Columns["UDF08"].Visible = false;
            treelist.Columns["UDF09"].Visible = false;
            treelist.Columns["UDF10"].Visible = false;
        }

        private void hidePurtbColumns(ref TreeList treelist)
        {
            treelist.Columns["MODIFIER"].Visible = false;
            treelist.Columns["MODI_DATE"].Visible = false;
            treelist.Columns["FLAG"].Visible = false;
            treelist.Columns["TB006"].Visible = false;
            treelist.Columns["TB010"].Visible = false;
            treelist.Columns["TB013"].Visible = false;
            treelist.Columns["TB014"].Visible = false;
            treelist.Columns["TB019"].Visible = false;
            treelist.Columns["TB020"].Visible = false;
            treelist.Columns["TB021"].Visible = false;
            treelist.Columns["TB022"].Visible = false;
            treelist.Columns["TB023"].Visible = false;
            treelist.Columns["TB024"].Visible = false;
            treelist.Columns["TB025"].Visible = false;
            treelist.Columns["TB026"].Visible = false;
            treelist.Columns["TB027"].Visible = false;
            treelist.Columns["TB028"].Visible = false;
            treelist.Columns["TB029"].Visible = false;
            treelist.Columns["TB030"].Visible = false;
            treelist.Columns["TB031"].Visible = false;
            treelist.Columns["TB032"].Visible = false;
            treelist.Columns["TB033"].Visible = false;
            treelist.Columns["TB034"].Visible = false;
            treelist.Columns["TB035"].Visible = false;
            treelist.Columns["TB036"].Visible = false;
            treelist.Columns["TB037"].Visible = false;
            treelist.Columns["TB038"].Visible = false;
            treelist.Columns["TB039"].Visible = false;
            treelist.Columns["TB040"].Visible = false;
            treelist.Columns["TB041"].Visible = false;
            treelist.Columns["TB042"].Visible = false;
            treelist.Columns["TB043"].Visible = false;
            treelist.Columns["TB044"].Visible = false;
            treelist.Columns["TB045"].Visible = false;
            treelist.Columns["TB046"].Visible = false;
            treelist.Columns["TB047"].Visible = false;
            treelist.Columns["TB048"].Visible = false;
            treelist.Columns["TB049"].Visible = false;
            treelist.Columns["TB050"].Visible = false;
            treelist.Columns["TB051"].Visible = false;
            treelist.Columns["TB052"].Visible = false;
            treelist.Columns["TB053"].Visible = false;
            treelist.Columns["TB054"].Visible = false;
            treelist.Columns["TB055"].Visible = false;
            treelist.Columns["TB056"].Visible = false;
            treelist.Columns["TB058"].Visible = false;
            treelist.Columns["TB059"].Visible = false;
            treelist.Columns["TB060"].Visible = false;
            treelist.Columns["TB061"].Visible = false;
            treelist.Columns["TB062"].Visible = false;
            treelist.Columns["TB063"].Visible = false;
            treelist.Columns["TB500"].Visible = false;
            treelist.Columns["TB501"].Visible = false;
            treelist.Columns["TB502"].Visible = false;
            treelist.Columns["TB503"].Visible = false;
            treelist.Columns["TB550"].Visible = false;
            treelist.Columns["TB551"].Visible = false;
            treelist.Columns["UDF01"].Visible = false;
            treelist.Columns["UDF02"].Visible = false;
            treelist.Columns["UDF03"].Visible = false;
            treelist.Columns["UDF04"].Visible = false;
            treelist.Columns["UDF05"].Visible = false;
            treelist.Columns["UDF06"].Visible = false;
            treelist.Columns["UDF07"].Visible = false;
            treelist.Columns["UDF08"].Visible = false;
            treelist.Columns["UDF09"].Visible = false;
            treelist.Columns["UDF10"].Visible = false;
        }

        private void frmErpCreatePR_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}