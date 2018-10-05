using DevExpress.Spreadsheet;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using MvLocalProject.Controller;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MvLocalProject.Bo;
using MvLocalProject.Model;

namespace MvLocalProject.Viewer
{
    public partial class frmMcBatchJob : Form
    {
        public frmMcBatchJob()
        {
            InitializeComponent();
        }

        // 開單
        // 調撥單 A121
        // 調撥單 A12E
        // 退料單 A561
        // 請購單 3102
        // 採購單 3302
        // 製令變更單
        // 託外製令 A512

        Hashtable[] hashTreeListBackColor = new Hashtable[3];
        DataTable cacheNoteDt = null;
        string cacheMocNo = string.Empty;
        DataSet cacheSortDs = null;                 // 依add, delete, change 分類的DataTable
        DataTable cacheMocDt = null;

        private void frmMcBatchJob_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }
        }

        private void sbOpenFile_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];
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
            hashTreeListBackColor[0].Clear();

            bool isSuccess = false;
            Workbook workbook = new Workbook();
            ArrayList itemList = new ArrayList();
            DataTable excelToDt = new DataTable();
            DataTable tempItemDt = null;

            using (FileStream stream = new FileStream(filePathAndName, FileMode.Open))
            {
                isSuccess = workbook.LoadDocument(stream, DocumentFormat.Xlsx);
            }
            if (isSuccess == false)
            {
                MessageBox.Show("請確認EXCEL檔案是否已解密");
                return;
            }

            // 判斷Sheet只能有一個, 等要release再打開
            //if(workbook.Sheets.Count != 1)
            //{
            //    MessageBox.Show(string.Format("請確認EXCEL檔案內只有一個sheet{0}該檔現有個sheets : {1}", Environment.NewLine, workbook.Sheets.Count));
            //    return;
            //}

            // 取得sheet 內容
            Worksheet sheet = workbook.Worksheets[workbook.Sheets.Count - 1];
            Range range = sheet.GetUsedRange();

            // 取得欄位標題資訊
            int j = 1;
            // 產品編號
            textBox1.Text = sheet.GetCellValue(1, 1).ToString();
            // 機台編號
            textBox2.Text = sheet.GetCellValue(1, 2).ToString();
            textBox3.Text = sheet.GetCellValue(j, 3).ToString();
            textBox4.Text = sheet.GetCellValue(j, 4).ToString();
            textBox5.Text = sheet.GetCellValue(j, 5).ToString();
            textBox6.Text = sheet.GetCellValue(j, 6).ToString();

            // 判斷是否可以從縱貫線取得製令編號
            string bomNo = textBox1.Text;
            string machineNo = textBox2.Text;
            bomNo = bomNo.Substring(bomNo.LastIndexOf("：") + 1);
            machineNo = machineNo.Substring(machineNo.LastIndexOf("：") + 1);

            txtBom.Text = bomNo;
            txtMachine.Text = machineNo;
            tempItemDt = MvDbDao.collectData_MachineFromMVPlan(machineNo, true);
            if (tempItemDt == null || tempItemDt.Rows.Count != 1)
            {
                MessageBox.Show(string.Format("無法由縱貫線系統取得未出貨的機台清單或機台清單數量大於1筆{0}{1}", Environment.NewLine, textBox2.Text));
                return;
            }
            // 可取得製令編號的, 要判斷執行時間是否在出貨時間允許的範圍內
            string mocNo = tempItemDt.Rows[0]["ManufactureOrderNo"].ToString();
            string shippedDate = tempItemDt.Rows[0]["ConfirmShipDate"].ToString();

            txtMocNo.Text = mocNo;
            cacheMocNo = mocNo;
            // 判斷執行時間start date 不能比預計出貨日end date晚
            DateTime dStart = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            DateTime dEnd = DateTime.Parse(shippedDate);
            if (DateTime.Compare(dStart, dEnd) > 0)
            {
                //MessageBox.Show(string.Format("執行時間已超過預計出貨日{0}執行時間: {1}{0}預計出貨日: {2}{0}母製令單號: {3}", Environment.NewLine, dStart.ToString("yyyy/MM/dd"), dEnd.ToString("yyyy/MM/dd"), mocNo));
                //return;
            }

            // 判斷執行時間start date 不能在預計出貨日end date 前一週內
            TimeSpan ts = dEnd - dStart;
            int differenceInDays = ts.Days;
            if (differenceInDays > 90)
            {
                MessageBox.Show(string.Format("執行時間距離預計出貨日小於7天, 不可再執行操作{0}執行時間: {1}{0}預計出貨日: {2}{0}母製令單號: {3}", Environment.NewLine, dStart.ToString("yyyy/MM/dd"), dEnd.ToString("yyyy/MM/dd"), mocNo));
                return;
            }

            // 定義column
            excelToDt = new DataTable();
            excelToDt.Columns.Add("Item");
            excelToDt.Columns.Add("Add");
            excelToDt.Columns.Add("Delete");
            excelToDt.Columns.Add("Change");
            excelToDt.Columns.Add("OrgA8");
            excelToDt.Columns.Add("NewA8");
            excelToDt.Columns.Add("A8Name");
            excelToDt.Columns.Add("OrgAmount");
            excelToDt.Columns.Add("NewAmount");
            excelToDt.Columns.Add("IsNew");
            excelToDt.Columns.Add("IsRDConfirm");
            excelToDt.Columns.Add("Owner");
            excelToDt.Columns.Add("Module");
            excelToDt.Columns.Add("Remark");
            excelToDt.Columns.Add("AmountForMC");
            excelToDt.Columns.Add("InventoryAll");
            excelToDt.Columns.Add("MB001");
            excelToDt.Columns.Add("MB002");
            excelToDt.Columns.Add("MB003");
            excelToDt.Columns.Add("MB004");
            // 以下欄位是為了從製令取資料, 以利開PR時帶入
            excelToDt.Columns.Add("MOCTA_TA201");
            excelToDt.Columns.Add("MOCTA_TA200");
            excelToDt.Columns.Add("MOCTB_TB201");
            excelToDt.Columns.Add("MOCTB_TB017");

            // 取得欄位修改內容
            j = 2;
            for (int i = 9; i <= range.RowCount; i++)
            {
                if (sheet.GetCellValue(j, i).IsEmpty == true)
                {
                    break;
                }
                j = 1;
                DataRow dr = excelToDt.NewRow();
                dr["Item"] = sheet.GetCellValue(j, i);
                dr["Add"] = sheet.GetCellValue(++j, i);
                dr["Delete"] = sheet.GetCellValue(++j, i);
                dr["Change"] = sheet.GetCellValue(++j, i);
                dr["OrgA8"] = sheet.GetCellValue(++j, i);

                if (dr["OrgA8"].ToString().Equals("N/A") == false)
                {
                    itemList.Add(dr["OrgA8"].ToString());
                }

                dr["NewA8"] = sheet.GetCellValue(++j, i);
                if (dr["NewA8"].ToString().Equals("N/A") == false)
                {
                    itemList.Add(dr["NewA8"].ToString());
                }

                dr["A8Name"] = sheet.GetCellValue(++j, i);
                dr["OrgAmount"] = sheet.GetCellValue(++j, i);
                dr["NewAmount"] = sheet.GetCellValue(++j, i);
                dr["IsNew"] = sheet.GetCellValue(++j, i);
                dr["IsRDConfirm"] = sheet.GetCellValue(++j, i);
                dr["Owner"] = sheet.GetCellValue(++j, i);
                dr["Module"] = sheet.GetCellValue(++j, i);
                dr["Remark"] = sheet.GetCellValue(++j, i);
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
                cacheNoteDt = null;
                // 判斷excel轉成data table後的欄位正確性查檢
                // 只有有問題的時候, 才需要取得BackColorList
                // checkDataValidAndSetBackColor(dt, tempItemDt, ref hashTreeListBackColor[0]);
                MessageBox.Show("部份料號不存在DB, 或Excel內容有不正確欄位");
            }
            else
            {
                // 將料件從INVMB的參數取得, 目前取得的參數 MB001, MB002, MB003, MB004
                string columnPattern = "RTRIM(MB001) MB001, RTRIM(MB002) MB002, RTRIM(MB003) MB003, RTRIM(MB004) MB004";

                tempItemDt = MvDbDao.collectData_InvmbItems((string[])itemList.ToArray(typeof(string)), columnPattern);
                foreach (DataRow dr in excelToDt.Rows)
                {
                    foreach(DataRow tempDr in tempItemDt.Rows)
                    {
                        if (dr["NewA8"].Equals(tempDr["MB001"]) || dr["OrgA8"].Equals(tempDr["MB001"]))
                        {
                            dr["MB001"] = tempDr["MB001"];
                            dr["MB002"] = tempDr["MB002"];
                            dr["MB003"] = tempDr["MB003"];
                            dr["MB004"] = tempDr["MB004"];
                            break;
                        }
                    }
                }
                // 保留至cache
                cacheNoteDt = excelToDt.Copy();
            }

            // 設定各Sheet 權限
            // 不開放編輯功能, 或隱藏欄位等
            treeList1.DataSource = excelToDt;
            setColumnsCaption(ref treeList1);

            treeList1.Columns["AmountForMC"].Visible = false;
            treeList1.Columns["InventoryAll"].Visible = false;
            treeList1.Columns["MB001"].Visible = false;
            treeList1.Columns["MB002"].Visible = false;
            treeList1.Columns["MB003"].Visible = false;
            treeList1.Columns["MB004"].Visible = false;
            treeList1.Columns["MOCTA_TA201"].Visible = false;
            treeList1.Columns["MOCTA_TA200"].Visible = false;
            treeList1.Columns["MOCTB_TB201"].Visible = false;
            treeList1.Columns["MOCTB_TB017"].Visible = false;

            treeList1.BestFitColumns();
            treeList1.OptionsView.AutoWidth = false;
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList1.OptionsBehavior.Editable = false;

            // 命名每個Pages
            xtraTabControl1.TabPages[0].Text = "差異料清單表";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];


            // 如果資料均正確, 顯示完後, 直接進入parsing流程
            if (isIllegal == false)
            {
                sbParsing_Click(sender, e);
            }

            SplashScreenManager.CloseForm(false);
        }
        private void clearAllCacheData()
        {
            cacheMocNo = string.Empty;
            treeList1.DataSource = null;
            treeList1.DataBindings.Clear();
            treeList1.Columns.Clear();

            treeList2.DataSource = null;
            treeList2.DataBindings.Clear();
            treeList2.Columns.Clear();

            treeList3.DataSource = null;
            treeList3.DataBindings.Clear();
            treeList3.Columns.Clear();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            txtItem.Clear();

        }


        private void setColumnsCaption(ref TreeList tl)
        {
            tl.Columns[0].Caption = "項目";
            tl.Columns[1].Caption = "新增";
            tl.Columns[2].Caption = "刪除";
            tl.Columns[3].Caption = "變更";
            tl.Columns[4].Caption = "原料號";
            tl.Columns[5].Caption = "變更料號";
            tl.Columns[6].Caption = "品名";
            tl.Columns[7].Caption = "原數量";
            tl.Columns[8].Caption = "變更需求";
            tl.Columns[9].Caption = "新建";
            tl.Columns[10].Caption = "文件上傳";
            tl.Columns[11].Caption = "負責人員";
            tl.Columns[12].Caption = "模組";
            tl.Columns[13].Caption = "備註";
            tl.Columns[14].Caption = "料控倉";
            tl.Columns[15].Caption = "其他倉";
        }

        private void setColumnsCaptionForInvetory(ref TreeList tl)
        {
            tl.Columns[7].Caption = "料號";
            tl.Columns[8].Caption = "倉別";
            tl.Columns[9].Caption = "儲位";
            tl.Columns[11].Caption = "數量";
        }

        private void setColumnsCaptionForNote(ref TreeList tl)
        {
            for(int i =0;i< CustomStructure.ExcelPrDtCaption.Length; i++)
            {
                tl.Columns[i].Caption = CustomStructure.ExcelPrDtCaption[i];
            }
        }


        private void frmMcBatchJob_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void sbCheckItemValidity_Click(object sender, EventArgs e)
        {
            string[] itemList = new string[1];
            itemList[0] = txtItem.Text;
            bool hasRow = MvDbDao.checkData_hasIllegalItemInInvmb(itemList);
            if (hasRow == true)
            {
                txtItem.BackColor = Color.Red;
            }
            else
            {
                txtItem.BackColor = Color.White;
            }
        }

        private void barCopyCell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeList treeList = null;

            if (treeList1.Focused == true)
            {
                treeList = treeList1;
            }
            else if (treeList2.Focused == true)
            {
                treeList = treeList2;
            }
            else if (treeList3.Focused == true)
            {
                treeList = treeList3;
            }
            else if (treeList4.Focused == true)
            {
                treeList = treeList4;
            }
            else if (treeList5.Focused == true)
            {
                treeList = treeList5;
            }
            else if (treeList6.Focused == true)
            {
                treeList = treeList6;
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
            catch (NullReferenceException)
            {
                Clipboard.Clear();
            }

            txtItem.Text = Clipboard.GetText();
            txtItem.BackColor = Color.White;
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

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[0].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.LightPink;
                e.Appearance.ForeColor = Color.DarkGreen;
            }
        }

        private bool checkDataValidAndSetBackColor(DataTable sourceDt, DataTable illegalDt, ref Hashtable hashTreeListBackColor)
        {

            int changeColorIndex = 0;
            bool isNoteRowValid = false;
            bool hasIllealRow = false;
            foreach (DataRow dr in sourceDt.Rows)
            {

                isNoteRowValid = checkNoteRowValid(dr);
                if (isNoteRowValid == false)
                {
                    hashTreeListBackColor[changeColorIndex] = true;
                    hasIllealRow = true;
                    changeColorIndex++;
                    continue;
                }

                if (illegalDt == null)
                {
                    changeColorIndex++;
                    continue;
                }

                foreach (DataRow illDr in illegalDt.Rows)
                {
                    if (dr["NewA8"].Equals(illDr["MB001"]) || dr["OrgA8"].Equals(illDr["MB001"]))
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

        private void barCopyAndCheckValid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barCopyCell_ItemClick(sender, e);
            sbCheckItemValidity_Click(sender, e);
        }


        private bool checkNoteRowValid(DataRow dr)
        {
            int orgAmount = 0;
            int newAmount = 0;
            string newA8 = string.Empty;
            string orgA8 = string.Empty;

            try
            {
                orgA8 = dr["OrgA8"].ToString();
                newA8 = dr["NewA8"].ToString();
                orgAmount = int.Parse(dr["OrgAmount"].ToString());
                newAmount = int.Parse(dr["NewAmount"].ToString());
            }
            catch (ArgumentNullException)
            {
                return false;
            }

            if (dr["Add"].ToString().Equals("V"))
            {
                // 新增, OrgA8 為N/A, 原數量為0
                if (orgA8.Equals("N/A") == false || orgAmount != 0)
                {
                    return false;
                }
            }
            else if (dr["Change"].ToString().Equals("V"))
            {
                // 變更, OrgA8, NewA8 都要有值, 就算N/A也得有值
                if (orgA8.Length == 0 || newA8.Length == 0)
                {
                    return false;
                }
            }
            else if (dr["Delete"].ToString().Equals("V"))
            {
                // 刪除
                if (newA8.Equals("N/A") == false || newAmount != 0)
                {
                    return false;
                }
            }
            else
            {
                // 異常狀態
                return false;
            }
            return true;
        }

        private void sbParsing_Click(object sender, EventArgs e)
        {
            if (cacheNoteDt == null)
            {
                MessageBox.Show("請先確認已有選取正確的差異料清單檔案");
                return;
            }

            DataSet ds = new DataSet();
            DataTable addDt = cacheNoteDt.Clone();
            DataTable deleteDt = cacheNoteDt.Clone();
            DataTable changeDt = cacheNoteDt.Clone();
            DataTable illegalDt = cacheNoteDt.Clone();
            DataTable tempDt = null;
            DataRow tempDr = null;
            DataRow[] tempDrList = null;
            DataTable noteDtA121 = null;
            DataTable noteDtA12E = null;
            DataTable noteDtPr = null;

            ArrayList addItemList = new ArrayList();
            ArrayList deleteItemList = new ArrayList();
            ArrayList changeItemList = new ArrayList();

            addDt.TableName = "Add";
            deleteDt.TableName = "Delete";
            changeDt.TableName = "Change";
            

            string condition = string.Empty;
            // 從製令找出備註的資訊
            // 如果料號不在, 則自動生成相關資訊
            SqlConnection connection = MvDbConnector.getErpDbConnection(GlobalMvVariable.MvAdCompany);
            try
            {
                tempDt = MvDbDao.collectData_Moc(connection, cacheMocNo);
            }
            catch (SqlException se)
            {
                throw se;
            }
            finally
            {
                MvDbConnector.closeSqlConnection(ref connection);
            }

            for (int i = 0; i < cacheNoteDt.Rows.Count; i++)
            {
                condition = string.Format("TA006 = '{0}' AND TB003 = '{1}'", cacheNoteDt.Rows[i]["Module"], cacheNoteDt.Rows[i]["OrgA8"].ToString().Equals("N/A") == false ? cacheNoteDt.Rows[i]["OrgA8"] : cacheNoteDt.Rows[i]["NewA8"]);
                tempDrList = tempDt.Select(condition);
                if (tempDrList.Length != 0)
                {
                    cacheNoteDt.Rows[i]["MOCTA_TA201"] = tempDrList[0]["TA201"];
                    cacheNoteDt.Rows[i]["MOCTA_TA200"] = tempDrList[0]["TA200"];
                    cacheNoteDt.Rows[i]["MOCTB_TB201"] = tempDrList[0]["TB201"];
                    cacheNoteDt.Rows[i]["MOCTB_TB017"] = tempDrList[0]["TB017"];
                }
            }


            // 開始執行庫存檢查作業
            int orgAmount = 0;
            int newAmount = 0;
            string newA8 = string.Empty;
            string orgA8 = string.Empty;

            // 先將新增, 刪除, 變更分類
            foreach (DataRow dr in cacheNoteDt.Rows)
            {
                orgA8 = dr["OrgA8"].ToString();
                newA8 = dr["NewA8"].ToString();
                orgAmount = int.Parse(dr["OrgAmount"].ToString());
                newAmount = int.Parse(dr["NewAmount"].ToString());
                // 新增
                if (dr["Add"].ToString().Equals("V"))
                {
                    // 檢查是否有庫存
                    tempDr = addDt.NewRow();
                    tempDr.ItemArray = dr.ItemArray.Clone() as object[];
                    addDt.Rows.Add(tempDr);
                    addItemList.Add(newA8);
                    Console.WriteLine("Add : " + dr["MB001"]);
                }
                // 刪除
                else if (dr["Delete"].ToString().Equals("V"))
                {
                    tempDr = deleteDt.NewRow();
                    tempDr.ItemArray = dr.ItemArray.Clone() as object[];
                    deleteDt.Rows.Add(tempDr);
                    deleteItemList.Add(orgA8);
                    Console.WriteLine("Delete : " + dr["MB001"]);
                }
                // 變更
                else if (dr["Change"].ToString().Equals("V"))
                {
                    tempDr = changeDt.NewRow();
                    tempDr.ItemArray = dr.ItemArray.Clone() as object[];
                    changeDt.Rows.Add(tempDr);
                    if (orgA8.Equals("N/A") == true) { changeItemList.Add(orgA8); }
                    if (newA8.Equals("N/A") == true) { changeItemList.Add(newA8); }
                    Console.WriteLine("Change : " + dr["MB001"]);
                }
                // 異常
                else
                {
                    tempDr = illegalDt.NewRow();
                    tempDr.ItemArray = dr.ItemArray.Clone() as object[];
                    illegalDt.Rows.Add(tempDr);
                    Console.WriteLine("illegal : " + dr["MB001"]);
                }
            }

            if (illegalDt.Rows.Count != 0)
            {
                MessageBox.Show("Parsing 資料有問題");
                cacheNoteDt = null;
                return;
            }
             
            // 1. 判斷是否還有庫存
            // 2. 判斷是否有經過儲位規劃
            // 3. 依現有數量整理後, 再乙次開單
            // 4. 

            // 判斷是否有儲位設定

            connection = MvDbConnector.getErpDbConnection(GlobalMvVariable.MvAdCompany);
            try
            {
                connection.Open();
                tempDt = MvDbDao.collectData_HasStorageLocation(connection, GlobalMvVariableForMc.OwnWarehouses);
                if (tempDt == null || tempDt.Rows.Count == 0)
                {
                    MessageBox.Show("Can't find the storage location information");
                    return;
                }

                foreach (DataRow tempDr1 in tempDt.Rows)
                {
                    if (tempDr1["MC009"].ToString().Equals("N"))
                    {
                        MessageBox.Show(string.Format("The warehouse has no storage location setting, please create the setting first{0}warehouse : {1}", Environment.NewLine, tempDr1["MC001"]));
                        return;
                    }
                }

                // 判斷是否有庫存量    
                //tempDt = MvDbDao.collectData_InventoryWithStorageLocation(connection, (string[])addItemList.ToArray(typeof(string)), warehousesForMc);

                // 先建立各單別的資料結構
                // 調撥單, 先暫時同PR
                noteDtPr = CustomStructure.cloneExcelPrDtColumn();
                noteDtA121 = CustomStructure.cloneExcelPrDtColumn();
                noteDtA12E = CustomStructure.cloneExcelPrDtColumn();

                tempDt = MvDbDao.collectData_Inventory(connection, (string[])addItemList.ToArray(typeof(string)), null);
                if (tempDt == null || tempDt.Rows.Count == 0)
                {
                    for (int i = 0; i < addDt.Rows.Count; i++)
                    {
                        addDt.Rows[i]["AmountForMC"] = 0;
                        addDt.Rows[i]["InventoryAll"] = 0;
                    }
                }
                else
                {
                    float amountForMc = 0;

                    string amountForOthers = string.Empty;
                    string warehouse = string.Empty;
                    for (int i = 0; i < addDt.Rows.Count; i++)
                    {
                        amountForMc = 0;
                        amountForOthers = string.Empty;
                        condition = string.Format("MC001 = '{0}'", addDt.Rows[i]["NewA8"]);
                        tempDrList = tempDt.Select(condition);
                        if (tempDrList.Length == 0)
                        {
                            addDt.Rows[i]["AmountForMC"] = 0;
                            addDt.Rows[i]["InventoryAll"] = 0;
                        }
                        else
                        {
                            for (int j = 0; j < tempDrList.Length; j++)
                            {
                                warehouse = tempDrList[j]["MC002"].ToString().TrimEnd();
                                if (Array.IndexOf(GlobalMvVariableForMc.OwnWarehouses, warehouse) >= 0)
                                {
                                    amountForMc += float.Parse(tempDrList[j]["MC007"].ToString());
                                }
                                else
                                {
                                    amountForOthers += string.Format("{0} {1:0}; ", warehouse, tempDrList[j]["MC007"]);
                                }
                            }
                            addDt.Rows[i]["AmountForMC"] = amountForMc;
                            addDt.Rows[i]["InventoryAll"] = amountForOthers.TrimEnd();
                        }
                    }

                    // 依庫別取得各儲位資訊
                    tempDt = MvDbDao.collectData_InventoryWithStorageLocation(connection, (string[])addItemList.ToArray(typeof(string)), GlobalMvVariableForMc.OwnWarehouses);
                    if (tempDt == null || tempDt.Rows.Count == 0)
                    {
                        // 只有開PR
                        Console.WriteLine("Only PR");
                        //MvErpNoteBo.createErpNote_PR(GlobalMvVariable.MvAdCompany, connection, ErpNoteHead.H_3109, null, DateTime.Now, GlobalMvVariable.MvAdUserName, "");

                        GlobalMvVariable.MvEmpNo = "M657";
                        for (int i = 0; i < addDt.Rows.Count; i++)
                        {

                            Console.WriteLine("create PR");
                            // 先轉成匯入的excel格式的DataTable
                            tempDr = noteDtPr.NewRow();
                            tempDr["TA012"] = GlobalMvVariable.MvEmpNo;
                            tempDr["TB004"] = addDt.Rows[i]["MB001"];
                            tempDr["TB005"] = addDt.Rows[i]["MB002"];
                            tempDr["TB006"] = addDt.Rows[i]["MB003"];
                            tempDr["TB009"] = addDt.Rows[i]["NewAmount"];
                            tempDr["TB008"] = "303";
                            tempDr["TB011"] = DateTime.Today.ToString("yyyy/MM/dd");
                            tempDr["TB012"] = "差異料清單" + addDt.Rows[i]["MOCTA_TA201"].ToString();
                            tempDr["TB201"] = addDt.Rows[i]["MOCTA_TA200"].ToString();
                            tempDr["TA202"] = "Y";
                            noteDtPr.Rows.Add(tempDr);
                        }
                    }
                    else
                    {
                        // 依庫存數先開調撥單(A121, A12E), 剩下不夠的, 再開PR
                        // 扣除的順序 310, 301, 472, 701
                        Console.WriteLine("Has A121, A12E, PR");

                        // 暫時使用
                        GlobalMvVariable.MvEmpNo = "M657";
                        float needAmount = 0;
                        float existAmount = 0;

                        DataRow[] tempDrList310 = null;
                        DataRow[] tempDrList472 = null;
                        DataRow[] tempDrList701 = null;
                        DataRow[] tempDrList301 = null;

                        for (int i = 0; i < addDt.Rows.Count; i++)
                        {
                            needAmount = int.Parse(addDt.Rows[i]["NewAmount"].ToString());
                            condition = string.Format("MM001 = '{0}'", addDt.Rows[i]["NewA8"]);
                            tempDrList = tempDt.Select(condition);

                            if (tempDrList.Length == 0)
                            {
                                Console.WriteLine("create PR");
                                // 先轉成匯入的excel格式的DataTable
                                addDataRowToNoteDt(ref noteDtPr, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", float.MinValue, DateTime.Today.ToString("yyyy/MM/dd"));
                            }
                            else
                            {
                                Console.WriteLine("create MO : " + addDt.Rows[i]["NewA8"]);
                                // 先用倉別來分, 依序是310, 472, 701, 301
                                condition = string.Format("MM001 = '{0}' AND MM002 = '{1}'", addDt.Rows[i]["NewA8"], "310");
                                tempDrList310 = tempDt.Select(condition);

                                condition = string.Format("MM001 = '{0}' AND MM002 = '{1}'", addDt.Rows[i]["NewA8"], "472");
                                tempDrList472 = tempDt.Select(condition);

                                condition = string.Format("MM001 = '{0}' AND MM002 = '{1}'", addDt.Rows[i]["NewA8"], "701");
                                tempDrList701 = tempDt.Select(condition);

                                condition = string.Format("MM001 = '{0}' AND MM002 = '{1}'", addDt.Rows[i]["NewA8"], "301");
                                tempDrList301 = tempDt.Select(condition);

                                // 如310倉有, 先減掉, 這裡還要改, 因為tempDrList不一定只會有一筆
                                // 310倉 -> A12E調撥
                                existAmount = 0;
                                if (needAmount > 0 && tempDrList310.Length > 0)
                                {
                                    foreach (DataRow dr in tempDrList310)
                                    {
                                        existAmount = float.Parse(dr["MM005"].ToString());
                                        if (needAmount >= existAmount)
                                        {
                                            addDataRowToNoteDt(ref noteDtA12E, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", existAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount -= existAmount;
                                        }
                                        else
                                        {
                                            addDataRowToNoteDt(ref noteDtA12E, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", needAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount = 0;
                                        }
                                    }
                                }
                                // 472倉 -> A121調撥
                                existAmount = 0;
                                if (needAmount > 0 && tempDrList472.Length > 0)
                                {
                                    foreach (DataRow dr in tempDrList472)
                                    {
                                        existAmount = float.Parse(dr["MM005"].ToString());
                                        if (needAmount >= existAmount)
                                        {
                                            addDataRowToNoteDt(ref noteDtA121, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", existAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount -= existAmount;
                                        }
                                        else
                                        {
                                            addDataRowToNoteDt(ref noteDtA121, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", needAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount = 0;
                                        }
                                    }
                                }
                                // 701倉 -> A121調撥
                                existAmount = 0;
                                if (needAmount > 0 && tempDrList701.Length > 0)
                                {
                                    foreach(DataRow dr in tempDrList701)
                                    {
                                        existAmount = float.Parse(dr["MM005"].ToString());
                                        if (needAmount >= existAmount)
                                        {
                                            addDataRowToNoteDt(ref noteDtA121, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", existAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount -= existAmount;
                                        }
                                        else
                                        {
                                            addDataRowToNoteDt(ref noteDtA121, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", needAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount = 0;
                                        }
                                    }
                                }
                                // 301倉 -> A12E調撥
                                existAmount = 0;
                                if (needAmount > 0 && tempDrList301.Length > 0)
                                {
                                    foreach (DataRow dr in tempDrList301)
                                    {
                                        existAmount = float.Parse(dr["MM005"].ToString());
                                        if (needAmount >= existAmount)
                                        {
                                            addDataRowToNoteDt(ref noteDtA12E, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", existAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount -= existAmount;
                                        }
                                        else
                                        {
                                            addDataRowToNoteDt(ref noteDtA12E, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", needAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                            needAmount = 0;
                                        }
                                    }
                                }
                                if (needAmount > 0)
                                {
                                    // 以上倉別都調撥完了, 還是不夠, 需再加入PR
                                    addDataRowToNoteDt(ref noteDtPr, addDt.Rows[i], GlobalMvVariable.MvEmpNo, "303", needAmount, DateTime.Today.ToString("yyyy/MM/dd"));
                                }
                            }
                        }
                    }
                }

                // 存入cacheSortDs
                cacheSortDs = new DataSet();
                cacheSortDs.Tables.Add(addDt);
                cacheSortDs.Tables.Add(deleteDt);
                cacheSortDs.Tables.Add(changeDt);
            }
            catch (SqlException se)
            {
                throw se;
            }
            finally
            {
                MvDbConnector.closeSqlConnection(ref connection);
            }

            // 只顯示並處理Add流程
            treeList2.DataSource = addDt;
            treeList3.DataSource = tempDt;

            // 命名每個Pages
            setColumnsCaption(ref treeList2);
            treeList2.Columns["Delete"].Visible = false;
            treeList2.Columns["Change"].Visible = false;
            treeList2.Columns["OrgA8"].Visible = false;
            treeList2.Columns["OrgAmount"].Visible = false;
            treeList2.Columns["IsNew"].Visible = false;
            treeList2.Columns["IsRDConfirm"].Visible = false;
            treeList2.Columns["Owner"].Visible = false;
            treeList2.Columns["Module"].Visible = false;
            treeList2.Columns["Remark"].Visible = false;
            treeList2.Columns["MB001"].Visible = false;
            treeList2.Columns["MB002"].Visible = false;
            treeList2.Columns["MB003"].Visible = false;
            treeList2.Columns["MB004"].Visible = false;
            treeList2.OptionsView.AutoWidth = false;
            treeList2.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.Editable = false;

            setColumnsCaptionForInvetory(ref treeList3);
            treeList3.Columns["COMPANY"].Visible = false;
            treeList3.Columns["USR_GROUP"].Visible = false;
            treeList3.Columns["MODIFIER"].Visible = false;
            treeList3.Columns["MODI_DATE"].Visible = false;
            treeList3.Columns["FLAG"].Visible = false;
            treeList3.Columns["MM004"].Visible = false;
            treeList3.Columns["MM006"].Visible = false;
            treeList3.Columns["MM007"].Visible = false;
            treeList3.Columns["MM008"].Visible = false;
            treeList3.Columns["MM009"].Visible = false;
            treeList3.Columns["MM010"].Visible = false;
            treeList3.Columns["MM011"].Visible = false;
            treeList3.Columns["MM012"].Visible = false;
            treeList3.Columns["MM013"].Visible = false;
            treeList3.Columns["MM014"].Visible = false;
            treeList3.Columns["MM015"].Visible = false;
            treeList3.Columns["MM016"].Visible = false;
            treeList3.Columns["MM017"].Visible = false;
            treeList3.Columns["UDF01"].Visible = false;
            treeList3.Columns["UDF02"].Visible = false;
            treeList3.Columns["UDF03"].Visible = false;
            treeList3.Columns["UDF04"].Visible = false;
            treeList3.Columns["UDF05"].Visible = false;
            treeList3.Columns["UDF06"].Visible = false;
            treeList3.Columns["UDF07"].Visible = false;
            treeList3.Columns["UDF08"].Visible = false;
            treeList3.Columns["UDF09"].Visible = false;
            treeList3.Columns["UDF10"].Visible = false;
            treeList3.OptionsView.AutoWidth = false;
            treeList3.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.Editable = false;

            xtraTabControl1.TabPages[1].Text = "差異料庫存資訊";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[1];
            treeList2.BestFitColumns();
            treeList3.BestFitColumns();


            treeList4.DataSource = noteDtPr;
            setColumnsCaptionForNote(ref treeList4);
            treeList4.OptionsView.AutoWidth = false;
            treeList4.OptionsBehavior.ReadOnly = true;
            treeList4.OptionsBehavior.Editable = false;

            treeList5.DataSource = noteDtA121;
            setColumnsCaptionForNote(ref treeList5);
            treeList5.OptionsView.AutoWidth = false;
            treeList5.OptionsBehavior.ReadOnly = true;
            treeList5.OptionsBehavior.Editable = false;

            treeList6.DataSource = noteDtA12E;
            setColumnsCaptionForNote(ref treeList6);
            treeList6.OptionsView.AutoWidth = false;
            treeList6.OptionsBehavior.ReadOnly = true;
            treeList6.OptionsBehavior.Editable = false;
            xtraTabControl1.TabPages[2].Text = "預計開單資訊";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[2];
            treeList4.BestFitColumns();
            treeList5.BestFitColumns();
            treeList6.BestFitColumns();
        }

        private void treeList2_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList2.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void treeList3_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList3.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void treeList4_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList4.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void treeList5_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList5.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void treeList6_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList6.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType != HitInfoType.Column)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }


        private void sbResort_Click(object sender, EventArgs e)
        {
      
        }

        private void addDataRowToNoteDt(ref DataTable noteDt, DataRow itemDr, string ta012, string tb008, float tb009, string createDate)
        {
            DataRow tempDr = noteDt.NewRow();
            tempDr["TA012"] = ta012;
            tempDr["TB004"] = itemDr["MB001"];
            tempDr["TB005"] = itemDr["MB002"];
            tempDr["TB006"] = itemDr["MB003"];
            tempDr["TB008"] = tb008;
            // tb009如果是最小數, 直接帶入NewAmount
            tempDr["TB009"] = float.MinValue == tb009 ? itemDr["NewAmount"] : tb009;
            tempDr["TB011"] = createDate;
            tempDr["TB012"] = "差異料清單" + itemDr["MOCTA_TA201"].ToString();
            tempDr["TB201"] = itemDr["MOCTA_TA200"].ToString();
            tempDr["TA202"] = "Y";
            noteDt.Rows.Add(tempDr);
        }

    }
}
