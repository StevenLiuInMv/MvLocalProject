using MvLocalProject.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MvLocalProject.Bo;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;
using MvLocalProject.Model;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Columns;
using System.Collections.Generic;
using System.Linq;

namespace MvLocalProject.Viewer
{
    public partial class frmBomToMocCompare : Form
    {
        bool isInitialBomList = true;
        DataTable cacheBomDt = null;
        DataTable cacheMergedBomDt = null;
        DataTable cacheMergedBomToVirtualMocDt = null;
        DataTable cacheMocDt = null;
        Hashtable[] hashTreeListBackColor = new Hashtable[4];

        public frmBomToMocCompare()
        {
            InitializeComponent();
        }


        private void sbtnGetBomList_Click(object sender, EventArgs e)
        {
            if (isInitialBomList == true)
            {
                // 第一次取得BomList
                DataTable tmpDt = null;
                tmpDt = MvDbDao.collectData_BomList();
                cboBomType.DataSource = tmpDt;
                cboBomType.ValueMember = "MC001";
                cboBomType.DisplayMember = "MB0012";
                cboBomType.Text = "";
                cboBomType.SelectedValue = "";
                isInitialBomList = false;
                return;
            }

            string result = cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
            if (result.Length == 0)
            {
                MessageBox.Show("Please choice the bom");
                return;
            }
            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            clearAllBomCacheDtAndTreeList();

            MvBomCompareBo bo = new MvBomCompareBo();
            DataSet sourceDs = null;

            // get source data set
            sourceDs = bo.GetDevDataSet_BomP09_Thin(result, true).Copy();
            DataTable sourceDt = sourceDs.Tables[result + "_Filter"].Copy();
            DataTable filterDt = sourceDs.Tables[result + "_Filter"].Copy();

            DataTable tempDt = null;

            // 找出選配
            tempDt = bo.getOptionalItem(filterDt);

            // set cache datatable
            if (cacheBomDt != null) { cacheBomDt.Clear(); }
            cacheBomDt = filterDt.Copy();

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            // 第1個Table的特別處理
            sourceDt.Columns.Remove("Column9");
            sourceDt.Columns.Remove("NameSpaceNoVer");
            sourceDt.Columns.Remove("AmountSpace");
            sourceDt.Columns["ModuleLv1"].SetOrdinal(6);

            treeList1.DataSource = sourceDt.Clone();
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, sourceDt, ref hashTreeListBackColor[0], false, false);
            setColumnsCaption(ref treeList1);

            // 第2個Table的特別處理
            tempDt.Columns.Remove("AmountSpace");
            tempDt.Columns.Remove("NameSpaceNoVer");
            tempDt.Columns.Remove("Column9");
            tempDt.Columns["ModuleLv1"].SetOrdinal(6);
            treeList2.DataSource = tempDt.Clone();
            showTreeListByLevel(treeList2, tempDt, ref hashTreeListBackColor[1], false, true);
            setColumnsCaption(ref treeList2);


            // 設定各Sheet的Sheet權限
            // 不開放編輯功能, 或隱藏欄位等
            treeList1.Columns["RowId"].Visible = false;
            treeList1.Columns["NameSpace"].Visible = false;
            treeList1.Columns["MD013"].Visible = false;
            treeList2.Columns["RowId"].Visible = false;
            treeList2.Columns["NameSpace"].Visible = false;
            treeList2.Columns["MD013"].Visible = false;
            treeList2.Columns["OrgLV"].Visible = false;

            treeList1.BestFitColumns();
            treeList2.BestFitColumns();

            treeList1.OptionsView.AutoWidth = false;
            treeList2.OptionsView.AutoWidth = false;

            treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;

            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;

            treeList2.ExpandAll();
            treeList2.OptionsView.ShowCheckBoxes = true;
            treeList2.OptionsBehavior.AllowRecursiveNodeChecking = true;

            // 命名每個Pages
            xtraTabControl1.TabPages[0].Text = "Original_Bom";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];
            //Close Wait Form
            SplashScreenManager.CloseForm(false);
        }

        private void showTreeListByLevel(TreeList tl, DataTable dt, ref Hashtable hashTreeListBackColor, bool isChangeLv, bool isChangeOrgLv)
        {
            TreeListNode[] parentNodeList = new TreeListNode[8];
            TreeListNode parentNode = null;
            TreeListNode newNode = null;
            int preLV = 0;
            int nowLv = 1;
            int changeColorIndex = 0;
            string strNowLv = string.Empty;
            string buyType = string.Empty;
            string A8 = string.Empty;
            string isDefaultOptional = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                strNowLv = dr["LV"].ToString().Trim().Replace(".", "");
                nowLv = int.Parse(strNowLv);
                buyType = dr["MB025X"].ToString().Trim();
                A8 = dr["A8"].ToString().Trim();
                isDefaultOptional = dr["MD013"].ToString();

                // ==============以下 Debug使用, 可移除 ============
                if (A8.Equals("21502121"))
                {
                    Console.WriteLine("test");
                }
                // ==============以上 Debug使用, 可移除 ============

                if (isChangeLv == true)
                {
                    dr["LV"] = "L" + strNowLv.ToString();
                }

                // 記錄是否要改變顏色
                if (buyType.Equals("模組"))
                {
                    hashTreeListBackColor[changeColorIndex] = true;
                }

                // 設定parentNode
                if (preLV < nowLv)          // 1 -> 2
                {
                    parentNode = parentNodeList[preLV];
                }
                else if (preLV == nowLv)    // 2 -> 2
                {
                    // parentNode 要設定上一層
                    parentNode = parentNodeList[nowLv - 1];
                }
                else if (preLV > nowLv)     // 2 -> 1
                {
                    // parentNode 要設定Parent的上一層, 並清空該層
                    parentNode = parentNodeList[nowLv - 1];
                    parentNodeList[preLV] = null;
                }

                // 取得新Node
                if(isChangeOrgLv == true)
                {
                    if (dr["OrgLV"].ToString().Length > 0)
                    {
                        dr["LV"] = dr["OrgLV"];
                    }
                }
                newNode = tl.AppendNode(dr.ItemArray, parentNode);
                // update node's CheckState
                if (isDefaultOptional.Equals("Y") == true)
                {
                    newNode.CheckState = CheckState.Checked;
                }
                // update parent node's CheckState
                if (parentNode != null)
                {
                    parentNode.CheckState = getParentNodeCheckState(parentNode);
                }
                parentNodeList[nowLv] = newNode;

                preLV = nowLv;
                changeColorIndex++;
            }
        }

        private void frmBomToMocCompare_Load(object sender, EventArgs e)
        {
            isInitialBomList = true;
            // initial Bar

            BarManager barManager = new BarManager();
            barManager.Form = this;
            barManager.BeginUpdate();
            Bar bar1 = new Bar(barManager, "My MainMenu");
            bar1.DockStyle = BarDockStyle.Top;
            barManager.MainMenu = bar1;

            // Create bar items for the bar1 and bar2
            BarSubItem subMenuFile = new BarSubItem(barManager, "File");
            BarSubItem subMenuView = new BarSubItem(barManager, "View");

            BarButtonItem buttonSaveOrgGrid = new BarButtonItem(barManager, "Save Orginal Bom as Excel ...");
            BarButtonItem buttonSaveFilterGrid = new BarButtonItem(barManager, "Save Filter Bom as Excel ...");
            BarButtonItem buttonSaveAll = new BarButtonItem(barManager, "Save All as Excel ...");

            BarButtonItem buttonExpandAll = new BarButtonItem(barManager, "ExpandAll");
            BarButtonItem buttonCollapseAll = new BarButtonItem(barManager, "CollapseAll");
            BarButtonItem buttonShowRowId = new BarButtonItem(barManager, "ShowRowId");

            subMenuFile.AddItems(new BarItem[] { buttonSaveOrgGrid, buttonSaveFilterGrid });
            subMenuView.AddItems(new BarItem[] { buttonExpandAll, buttonCollapseAll, buttonShowRowId });

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subMenuFile, subMenuView });

            // A handler to process clicks on bar items
            barManager.ItemClick += new ItemClickEventHandler(barManager_ItemClick);

            barManager.EndUpdate();

            // initial BomList
            sbtnGetBomList_Click(sender, e);

        }

        void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarSubItem subMenu = e.Item as BarSubItem;
            if (subMenu != null) { return; }

            TreeList tmpTl = null;
            if (e.Item.Caption.Equals("Save Orginal Bom as Excel ..."))
            {
                tmpTl = treeList1;
            }
            else if (e.Item.Caption.Equals("Save Filter Bom as Excel ..."))
            {
                tmpTl = treeList2;
            }
            else if (e.Item.Caption.Equals("Save All as Excel ..."))
            {
                // unfinished
            }
            else if (e.Item.Caption.Equals("ExpandAll"))
            {
                treeList1.ExpandAll();
                treeList2.ExpandAll();
                treeList3.ExpandAll();
                treeList7.ExpandAll();
                return;
            }
            else if (e.Item.Caption.Equals("CollapseAll"))
            {
                treeList1.CollapseAll();
                treeList2.CollapseAll();
                treeList3.CollapseAll();
                treeList7.CollapseAll();
                return;
            }
            else if(e.Item.Caption.Equals("ShowRowId"))
            {
                TreeListColumn col = null;
                try
                {
                    col = treeList1.Columns.First(c => c.FieldName == "RowId");
                }
                catch (InvalidOperationException)
                {
                    // do nothing
                    return;
                }

                if (col == null)
                {
                    return;
                }
                else
                {
                    col.Visible = true;
                    col.VisibleIndex = 0;
                }
            }


            // 往下跑, 目前只有存檔功能, 用tmpTreeList看有沒有指定來判斷
            if (tmpTl != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel 活頁簿 |*.xlsx";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName == "") { return; }
                string filePathAndName = saveFileDialog1.FileName;

                try
                {
                    tmpTl.ExportToXlsx(filePathAndName);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, filePathAndName));
                    return;
                }
                MessageBox.Show(string.Format("Save as path {0}{1}", Environment.NewLine, filePathAndName));
            }
        }

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[0].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void treeList2_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[1].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void treeList3_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[2].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void treeList7_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[3].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }


        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            string workingDirectory = @"D:\99_TempArea\";
            string fileNameAndPath = string.Format("{0}{1}_{2}.xlsx", workingDirectory, DefinedReport.ErpReportType.BomP09_RD.ToString(), DateTime.Now.ToString("yyyyMMdd"));
            try
            {
                treeList1.ExportToXlsx(fileNameAndPath);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, fileNameAndPath));
                return;
            }
            MessageBox.Show(string.Format("Save the file in below path {0}{1}", Environment.NewLine, fileNameAndPath));
        }

        private void setColumnsCaption(ref TreeList tl)
        {
            tl.Columns[0].Caption = "階次";
            tl.Columns[1].Caption = "元件品號";
            tl.Columns[2].Caption = "品號屬性";
            tl.Columns[3].Caption = "品名";
            tl.Columns[4].Caption = "單位";
            tl.Columns[5].Caption = "數量";
            //tl.Columns[6].Caption = "SOP圖號";
            //if (tl.Columns.Count > 7)
            //{
            //    tl.Columns[6].Caption = "模組";
            //}
        }

        private void cboBomType_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboBomType.SelectedIndex < 0) { return; }
            if (e.KeyCode == Keys.Enter) { sbtnGetBomList_Click(sender, e); }
        }

        private void frmBomToMocCompare_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

        private void treeList7_MouseUp(object sender, MouseEventArgs e)
        {
            // 只有在滑鼠右鍵, 及不在Column上才彈出 PopupMenu
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeList7.CalcHitInfo(new Point(e.X, e.Y));
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
            else if (treeList7.Focused == true)
            {
                treeList = treeList7;
            }

            if (treeList == null) return;
            Clipboard.SetText(treeList.FocusedNode.GetDisplayText(treeList.FocusedColumn));
            textBox1.Text = Clipboard.GetText();
        }

        private void sbGetMoc_Click(object sender, EventArgs e)
        {
            string mocNo = textEdit1.Text;
            if (textEdit1.Text.IndexOf("-") < 0)
            {
                MessageBox.Show("請輸入正確的製令單號" + Environment.NewLine + "Ex : A511-20180500001");
                return;
            }

            SplashScreenManager.ShowDefaultWaitForm();
            DataTable sourceDt = MvDbDao.collectData_Moc(mocNo);
            // copy to cacheDt
            if (cacheMocDt != null) { cacheMocDt.Clear(); }
            cacheMocDt = sourceDt.Copy();
            treeList4.DataSource = sourceDt;

            // 不開放編輯功能
            treeList4.OptionsBehavior.ReadOnly = true;
            treeList4.OptionsBehavior.Editable = false;

            xtraTabControl1.TabPages[3].Text = "製令_" + mocNo;
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[3];
            SplashScreenManager.CloseForm(false);
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textEdit1.Text.Length <= 0) { return; }
            if (e.KeyCode == Keys.Enter) { sbGetMoc_Click(sender, e); }
        }

        private void sbMocCompare_Click(object sender, EventArgs e)
        {
            if (cacheMocDt == null || cacheMergedBomToVirtualMocDt == null || cacheMocDt.Rows.Count == 0 || cacheMergedBomToVirtualMocDt.Rows.Count == 0)
            {
                MessageBox.Show("請先操作取得Bom與製令");
                return;
            }

            // 開始執行比對的程式
            // 比對的邏輯, 先判斷第一層模組是不是合理
            SplashScreenManager.ShowDefaultWaitForm();
            DataSet resultDs = null;
            MvBomCompareBo bo = new MvBomCompareBo();
            resultDs = bo.compareProcessByVirtualMoc(cacheMergedBomToVirtualMocDt, cacheMocDt);
            SplashScreenManager.CloseForm(false);

        }

        private void barExpandAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (treeList1.Focused == true)
            {
                treeList1.ExpandAll();
            }
            else if (treeList2.Focused == true)
            {
                treeList2.ExpandAll();
            }
            else if (treeList3.Focused == true)
            {
                treeList3.ExpandAll();
            }
            else if (treeList7.Focused == true)
            {
                treeList7.ExpandAll();
            }

        }

        private void barCollapseAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (treeList1.Focused == true)
            {
                treeList1.CollapseAll();
            }
            else if (treeList2.Focused == true)
            {
                treeList2.CollapseAll();
            }
            else if (treeList3.Focused == true)
            {
                treeList3.CollapseAll();
            }
            else if (treeList7.Focused == true)
            {
                treeList7.CollapseAll();
            }
        }

        private void barMappingNodes_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 此功能只開放給TreeList2
            TreeList treeList = null;

            if (treeList2.Focused != true)
            {
                MessageBox.Show("只在Original_Bom的選配欄才可使用");
                return;
            }

            //if (treeList2 == null) { return; }

            treeList = treeList2;
            // 選到的node, 找出Source對應
            string focusNameSpace = treeList.FocusedNode.GetDisplayText("NameSpace");
            if (focusNameSpace.Length == 0)
            {
                return;
            }
            treeList1.FocusedNode = treeList1.FindNodeByFieldValue("NameSpace", focusNameSpace);
            treeList1.Select();
        }

        private CheckState getParentNodeCheckState(TreeListNode parentNode)
        {
            int checkedCount = 0;
            if (parentNode == null) { return CheckState.Unchecked; }
            foreach(TreeListNode childNode in parentNode.Nodes)
            {
                if (childNode.Checked == true)
                {
                    checkedCount += 1;
                }
            }

            if (checkedCount == parentNode.Nodes.Count)
            {
                return CheckState.Checked;
            }
            else if (checkedCount == 0)
            {
                return CheckState.Unchecked;
            } else
            {
                return CheckState.Indeterminate;
            }
        }

        private void sbMergeBom_Click(object sender, EventArgs e)
        {

            if (cacheBomDt == null)
            {
                MessageBox.Show("請先操作取得Bom後再執行Merge作業");
                return;
            }
            // 開始執行Filter作業

            SplashScreenManager.ShowDefaultWaitForm();

            DataTable removeNodesDt = new DataTable();

            foreach (TreeListColumn tlc in treeList2.Columns)
            {
                removeNodesDt.Columns.Add(tlc.FieldName);
            }

            DataRow treeListDr = null;
            DataRow tempDr = null;
            TreeListNode node = null;
            DataTable mergeDt = cacheBomDt.Copy();

            for (int nodeIndex = 0; nodeIndex < treeList2.AllNodesCount; nodeIndex++)
            {
                tempDr = removeNodesDt.NewRow();
                node = treeList2.GetNodeByVisibleIndex(nodeIndex);
                // 找出Unchecked node, 準備從bom datatable 中移除
                if (node.CheckState != CheckState.Unchecked)
                {
                    continue;
                }

                treeListDr = treeList2.GetDataRow(nodeIndex);
                tempDr.ItemArray = treeListDr.ItemArray.Clone() as object[];
                removeNodesDt.Rows.Add(tempDr);
            }

            // 取完後, 再來的部份就是要從原來的bom移除row
            // 下午可能要調整前面的程式, 之後要用hide的, 不可以用remove datacolumn
            // 可能會導致後續的資料無法回填

            // 要由後往前Remove
            // LV > 1, 先移除name space.*, 再移除name space
            // LV = 1, 只移除name space, 因為子階在之前就被移除了
            string tempNameSpace = string.Empty;
            string condition = string.Empty;
            int tempLv = 0;
            //DataRow[] removeDataRowList = null;

            //// 先將child移除後, 再移除parent
            //for (int index = removeNodesDt.Rows.Count; index > 0; index--)
            //{
            //    tempDr = removeNodesDt.Rows[index - 1];
            //    tempNameSpace = tempDr["NameSpace"].ToString();
            //    tempLv = int.Parse(tempDr["LV"].ToString());
            //    DataRow dr1 = null;

            //    if (tempLv > 1)
            //    {
            //        condition = string.Format("NameSpace like '{0}.%'", tempNameSpace);
            //        removeDataRowList = mergeDt.Select(condition);

            //        try
            //        {
            //            for(int index1 = 0;index1< removeDataRowList.Length; index1++)
            //            {
            //                dr1 = removeDataRowList[index1];
            //                mergeDt.Rows.Remove(dr1);
            //            }
            //        }
            //        catch (IndexOutOfRangeException)
            //        {
            //            // 發生此exception 不跳離, 有可能該row在之前已被移除
            //            Console.WriteLine("Remove Child Exception, RowId=" + dr1["RowId"] + ", " + dr1["LV"].ToString() + " " + dr1["A8"].ToString() + " " + dr1["ModuleLv1"].ToString());
            //        }
            //    }
            //    // 先將child移除後, 再移除parent
            //    // 不能改用List 接Array, 有可能會把parent給移除
            //    condition = string.Format("NameSpace = '{0}'", tempNameSpace);
            //    removeDataRowList = mergeDt.Select(condition);
            //    try
            //    {
            //        for (int index1 = 0; index1 < removeDataRowList.Length; index1++)
            //        {
            //            dr1 = removeDataRowList[index1];
            //            mergeDt.Rows.Remove(dr1);
            //        }
            //        //mergeDt.Rows.Remove(removeDataRowList);
            //    }
            //    catch (IndexOutOfRangeException)
            //    {
            //        // 發生此exception 不跳離, 有可能該row在之前已被移除
            //        Console.WriteLine("Remove Parent Exception, RowId=" + tempDr["RowId"] + ", " + tempDr["LV"].ToString() + " " + tempDr["A8"].ToString() + " " + tempDr["ModuleLv1"].ToString());
            //    }
            //}





            // 先將child移除後, 再移除parent
            List<DataRow> removeDataRowList = new List<DataRow>();
            DataRow dr1 = null;
            for (int index = removeNodesDt.Rows.Count; index > 0; index--)
            {
                removeDataRowList.Clear();
                tempDr = removeNodesDt.Rows[index - 1];
                tempNameSpace = tempDr["NameSpace"].ToString();
                tempLv = int.Parse(tempDr["LV"].ToString());

                // 先找child nodes
                if (tempLv > 1)
                {
                    condition = string.Format("NameSpace like '{0}.%'", tempNameSpace);
                    removeDataRowList.AddRange(mergeDt.Select(condition));
                }
                // 再找parent nodes
                condition = string.Format("NameSpace = '{0}'", tempNameSpace);
                removeDataRowList.AddRange(mergeDt.Select(condition));
                try
                {
                    for (int index1 = 0; index1 < removeDataRowList.Count; index1++)
                    {
                        dr1 = removeDataRowList[index1];
                        mergeDt.Rows.Remove(dr1);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // 發生此exception 不跳離, 有可能該row在之前已被移除
                    Console.WriteLine("Remove Exception, RowId=" + dr1["RowId"] + ", " + dr1["LV"].ToString() + " " + dr1["A8"].ToString() + " " + dr1["ModuleLv1"].ToString());
                }
            }


            // copy to cacheDt
            if (cacheMergedBomDt != null) { cacheMergedBomDt.Clear(); }
            cacheMergedBomDt = mergeDt.Copy();

            treeList7.DataSource = mergeDt.Clone();
            hashTreeListBackColor[3].Clear();
            showTreeListByLevel(treeList7, mergeDt, ref hashTreeListBackColor[3], false, false);
            setColumnsCaption(ref treeList7);

            treeList7.Columns["Column9"].Visible = false;
            treeList7.Columns["MD013"].Visible = false;
            treeList7.Columns["NameSpace"].Visible = false;
            treeList7.Columns["NameSpaceNoVer"].Visible = false;
            treeList7.Columns["AmountSpace"].Visible = false;
            treeList7.Columns["RowId"].Visible = false;
            treeList7.BestFitColumns();
            treeList7.OptionsView.AutoWidth = false;
            treeList7.OptionsBehavior.ReadOnly = true;
            treeList7.OptionsBehavior.Editable = false;

            xtraTabControl1.TabPages[1].Text = "Merged_Bom";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[1];

            SplashScreenManager.CloseForm(false);
        }

        private void sbConvertVirtualMoc_Click(object sender, EventArgs e)
        {
            if (cacheMergedBomDt == null)
            {
                MessageBox.Show("請先操作Merge取得MergeBom後再執行Convert作業");
                return;
            }

            SplashScreenManager.ShowDefaultWaitForm();

            MvBomCompareBo bo = new MvBomCompareBo();
            DataTable mocDt = null;

            // convert bom to moc and copy to cacheDt
            //mocDt = bo.convertBomToMoc(filterDt);
            mocDt = bo.convertBomToVirturlMoc(cacheMergedBomDt);

            if (cacheMergedBomToVirtualMocDt != null) { cacheMergedBomToVirtualMocDt.Clear(); }
            cacheMergedBomToVirtualMocDt = mocDt.Copy();

            // 第3個Table的特別處理
            //mocDt.Columns.Remove("MD006");
            //mocDt.Columns.Remove("NameSpace");
            //mocDt.Columns.Remove("NameSpaceNoVer");
            //mocDt.Columns.Remove("Column9");

            mocDt.Columns["RealAmount"].SetOrdinal(5);
            mocDt.Columns["ModuleLv1"].SetOrdinal(7);
            treeList3.DataSource = mocDt.Copy();
            //hashTreeListBackColor[2].Clear();
            //showTreeListByLevel(treeList3, mocDt, ref hashTreeListBackColor[2], false, false);
            setColumnsCaption(ref treeList3);


            // 設定Sheet3的權限
            // 不開放編輯功能, 或隱藏欄位等
            treeList3.Columns["MD006"].Visible = false;
            treeList3.Columns["NameSpace"].Visible = false;
            treeList3.Columns["NameSpaceNoVer"].Visible = false;
            treeList3.Columns["Column9"].Visible = false;
            treeList3.Columns["RowId"].Visible = false;

            treeList3.OptionsView.AutoWidth = false;
            treeList3.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.Editable = false;
            //treeList3.BestFitColumns();

            // 命名每個Pages
            xtraTabControl1.TabPages[2].Text = "MergedBom_VirtualMoc";
            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[2];

            SplashScreenManager.CloseForm(false);
        }

        private void clearAllBomCacheDtAndTreeList()
        {
            cacheBomDt = null;
            cacheMergedBomDt = null;
            cacheMergedBomToVirtualMocDt = null;
            cacheMocDt = null;

            treeList1.DataSource = null;
            treeList1.DataBindings.Clear();
            treeList1.Columns.Clear();

            treeList2.DataSource = null;
            treeList2.DataBindings.Clear();
            treeList2.Columns.Clear();

            treeList3.DataSource = null;
            treeList3.DataBindings.Clear();
            treeList3.Columns.Clear();

            treeList7.DataSource = null;
            treeList7.DataBindings.Clear();
            treeList7.Columns.Clear();
        }

        private void barShowRowId_ItemClick(object sender, ItemClickEventArgs e)
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
            else if (treeList7.Focused == true)
            {
                treeList = treeList7;
            }

            if (treeList == null) return;

            TreeListColumn col = null;
            try
            {
                col = treeList.Columns.First(c => c.FieldName == "RowId");
            }
            catch (InvalidOperationException)
            {
                // do nothing
                return;
            }

            if (col == null)
            {
                return;
            }
            else
            {
                // 因為treeList2 有checkBox, 所以顯示在第2行
                if (treeList == treeList2)
                {
                    col.VisibleIndex = 1;
                }
                else
                {
                    col.VisibleIndex = 0;
                }
                col.Visible = true;
            }
        }

        private void barHideRowId_ItemClick(object sender, ItemClickEventArgs e)
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
            else if (treeList7.Focused == true)
            {
                treeList = treeList7;
            }

            if (treeList == null) return;

            TreeListColumn col = null;
            try
            {
                col = treeList.Columns.First(c => c.FieldName == "RowId");
            }
            catch (InvalidOperationException)
            {
                // do nothing
                return;
            }

            if (col == null)
            {
                return;
            }
            else
            {
                col.Visible =false;
            }
        }
    }
}
