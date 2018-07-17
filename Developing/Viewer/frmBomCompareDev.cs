using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MvLocalProject.Bo;
using MvLocalProject.Controller;
using MvLocalProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmBomCompareDev : Form
    {
        bool isInitialBomList = false;
        Hashtable[] hashTreeListBackColor = new Hashtable[2];

        public frmBomCompareDev()
        {
            InitializeComponent();
        }

        private void frmBomCompareDev_Load(object sender, EventArgs e)
        {
            BarManager barManager = new BarManager();
            barManager.Form = this;
            barManager.BeginUpdate();
            Bar bar1 = new Bar(barManager, "My MainMenu");
            bar1.DockStyle = BarDockStyle.Top;
            barManager.MainMenu = bar1;

            // Create bar items for the bar1 and bar2
            BarSubItem subMenuFile = new BarSubItem(barManager, "File");
            BarSubItem subMenuView = new BarSubItem(barManager, "View");

            BarButtonItem buttonSaveOrgGrid = new BarButtonItem(barManager, "Save Orginal Grid as Excel ...");
            BarButtonItem buttonSaveNewGrid = new BarButtonItem(barManager, "Save New Grid as Excel ...");
            BarButtonItem buttonSaveCompareGrid = new BarButtonItem(barManager, "Save CompareDetail sheet as Excel ...");
            BarButtonItem buttonSaveSummayDetailGrid = new BarButtonItem(barManager, "Save SummaryDetail sheet as Excel ...");
            BarButtonItem buttonSaveSummay = new BarButtonItem(barManager, "Save Summary as Excel ...");
            BarButtonItem buttonSaveAll = new BarButtonItem(barManager, "Save All as Excel ...");

            BarButtonItem buttonExpandAll = new BarButtonItem(barManager, "ExpandAll");
            BarButtonItem buttonCollapseAll = new BarButtonItem(barManager, "CollapseAll");

            subMenuFile.AddItems(new BarItem[] { buttonSaveOrgGrid, buttonSaveNewGrid, buttonSaveCompareGrid, buttonSaveSummayDetailGrid, buttonSaveSummay});
            subMenuView.AddItems(new BarItem[] { buttonExpandAll, buttonCollapseAll });

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subMenuFile, subMenuView });

            // A handler to process clicks on bar items
            barManager.ItemClick += new ItemClickEventHandler(barManager_ItemClick);

            barManager.EndUpdate();

            // initial BomList
            btnGetBomList_Click(sender, e);


        }
        void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarSubItem subMenu = e.Item as BarSubItem;
            if (subMenu != null) { return; }

            TreeList tmpTl = null;
            if (e.Item.Caption.Equals("Save Orginal Grid as Excel ..."))
            {
                tmpTl = treeList1;
            }
            else if (e.Item.Caption.Equals("Save New Grid as Excel ..."))
            {
                tmpTl = treeList2;
            }
            else if(e.Item.Caption.Equals("Save CompareDetail sheet as Excel ..."))
            {
                tmpTl = treeList4;
            }
            else if (e.Item.Caption.Equals("Save SummaryDetail sheet as Excel ..."))
            {
                tmpTl = treeList5;
            }
            else if (e.Item.Caption.Equals("Save Summary as Excel ..."))
            {
                tmpTl = treeList6;
            }
            else if (e.Item.Caption.Equals("Save All as Excel ..."))
            {
                // unfinished
            }
            else if (e.Item.Caption.Equals("ExpandAll"))
            {
                treeList1.ExpandAll();
                treeList2.ExpandAll();
                return;
            }
            else if (e.Item.Caption.Equals("CollapseAll"))
            {
                treeList1.CollapseAll();
                treeList2.CollapseAll();
                return;
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

        private void btnGetBomList_Click(object sender, EventArgs e)
        {
            DataTable tmpDt = null;
            tmpDt = MvDbDao.collectData_BomList();
            cboBomType.DataSource = tmpDt;
            cboBomType.ValueMember = "MC001";
            cboBomType.DisplayMember = "MB0012";
            cboBomType.Text = "";
            cboBomType.SelectedValue = "";

            BomView.Columns.Add("BomId");
            BomView.Columns.Add("BomName");
            BomView.View = View.Details;

            enableObject(true, false);
            btnGetBomList.Visible = false;
        }


        private void enableObject(bool enableGroup, bool enableTabControl)
        {
            groupBom.Enabled = enableGroup;
            //tabControl1.Enabled = enableTabControl;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (BomView.Items.Count > 1)
            {
                MessageBox.Show("Please remove item first");
                return;
            }

            string result = cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
            if (result.Length == 0) { return; }

            ListViewItem lvi = new ListViewItem(result);
            lvi.SubItems.Add(cboBomType.Text.Remove(0, result.Length + 1));
            BomView.Items.Add(lvi);
            BomView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            BomView.Refresh();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (BomView.Items.Count < 1) { return; }
            for (int i = 0; i < BomView.Items.Count; i++)
            {
                if (BomView.Items[i].Selected)
                {
                    BomView.Items[i].Remove();
                    i--;
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            btnCompare_Click_Thin(sender, e);
        }

        private void btnCompare_Click_VB6(object sender, EventArgs e)
        {
            DataTable tmpDt = new DataTable();
            if (isInitialBomList == true)
            {
                // 第一次取得BomList

                tmpDt = MvDbDao.collectData_BomList();
                cboBomType.DataSource = tmpDt;
                cboBomType.ValueMember = "MC001";
                cboBomType.DisplayMember = "MB0012";
                cboBomType.Text = "";
                cboBomType.SelectedValue = "";
                isInitialBomList = false;
                return;
            }

            if (BomView.Items.Count < 2)
            {
                MessageBox.Show("請選擇2個bom表");
                return;
            }

            // 取得已選取的list
            List<string> selectedList = new List<string>();
            foreach (var item in BomView.Items)
            {
                string row = (item as ListViewItem).Text;
                selectedList.Add(row);
            }

            // 判斷是否選取相同的Bom表
            if (selectedList[0].Equals(selectedList[1]) == true)
            {
                MessageBox.Show("請不要選取相同的bom表比對");
                return;
            }

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();


            DataSet sourceDs = new DataSet();
            MvBomCompareBo bo = new MvBomCompareBo();

            sourceDs = bo.CompareProcessByDev(selectedList.ToArray<string>()).Copy();

            selectedList.Clear();
            selectedList = null;

            // filter data
            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            DataTable sourceDt2 = sourceDs.Tables[1].Copy();

            // show tree list node
            // 設定column
            // 顯示Detail Summary相關資訊
            // 相同的資料不用再呈現差異的比對資訊
            tmpDt = sourceDs.Tables["Same"];
            tmpDt.Columns.Remove("CompareLV");
            tmpDt.Columns.Remove("CompareA8");
            tmpDt.Columns.Remove("CompareMD006");
            tmpDt.Columns.Remove("ModuleLv1");
            treeList3.DataSource = tmpDt.Copy();

            treeList4.DataSource = sourceDs.Tables["Different"].Copy();
            setCompareDetailColumnsCaption(ref treeList3, ref treeList4);

            // 顯示SummaryBom相關資訊 for RD
            treeList5.DataSource = sourceDs.Tables["SummaryRD"].Copy();
            setSummaryDetailColumnsCaption(ref treeList5);

            // 整理SummaryBom For Pur
            treeList6.DataSource = sourceDs.Tables["SummaryPUR"].Copy().Copy();
            setSummaryColumnsCaption(ref treeList6);

            // 整理完後再把name space資訊移除
            sourceDt1.Columns.Remove("NameSpace");
            sourceDt1.Columns.Remove("NameSpaceNoVer");
            sourceDt2.Columns.Remove("NameSpace");
            sourceDt2.Columns.Remove("NameSpaceNoVer");

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            // 注意 ParentFieldName必需在 showTreeListByLevel之前
            // 只要設定過, Level的功能會失效
            treeList1.DataSource = sourceDt1.Clone();
            treeList1.ParentFieldName = sourceDt1.TableName;
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, sourceDt1, ref hashTreeListBackColor[0], true);
            setColumnsCaption(ref treeList1);

            // 注意 ParentFieldName必需在 showTreeListByLevel之前
            // 只要設定過, Level的功能會失效
            treeList2.DataSource = sourceDt2.Clone();
            treeList2.ParentFieldName = sourceDt2.TableName;
            hashTreeListBackColor[1].Clear();
            showTreeListByLevel(treeList2, sourceDt2, ref hashTreeListBackColor[1], true);
            setColumnsCaption(ref treeList2);

            xtraTabControl1.TabPages[0].Text = string.Format("{0} vs {1}", sourceDt1.TableName, sourceDt2.TableName);
            xtraTabControl1.TabPages[1].Text = "CompareDetail";
            xtraTabControl1.TabPages[2].Text = "SummaryDetail";
            xtraTabControl1.TabPages[3].Text = "Summary";

            // Sheet1,2 不開放編輯功能, treeList 1,2,3,4
            // Sheet3,4 開放編輯功能, 這些資料都只能是read only
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.ReadOnly = true;
            treeList4.OptionsBehavior.ReadOnly = true;
            treeList5.OptionsBehavior.ReadOnly = true;
            treeList6.OptionsBehavior.ReadOnly = true;

            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;
            treeList3.OptionsBehavior.Editable = false;
            treeList4.OptionsBehavior.Editable = false;

            //Close Wait Form
            SplashScreenManager.CloseForm(false);
        }

        private void btnCompare_Click_Thin(object sender, EventArgs e)
        {
            DataTable tmpDt = new DataTable();
            if (isInitialBomList == true)
            {
                // 第一次取得BomList

                tmpDt = MvDbDao.collectData_BomList();
                cboBomType.DataSource = tmpDt;
                cboBomType.ValueMember = "MC001";
                cboBomType.DisplayMember = "MB0012";
                cboBomType.Text = "";
                cboBomType.SelectedValue = "";
                isInitialBomList = false;
                return;
            }

            if (BomView.Items.Count < 2)
            {
                MessageBox.Show("請選擇2個bom表");
                return;
            }

            // 取得已選取的list
            List<string> selectedList = new List<string>();
            foreach (var item in BomView.Items)
            {
                string row = (item as ListViewItem).Text;
                selectedList.Add(row);
            }

            // 判斷是否選取相同的Bom表
            if (selectedList[0].Equals(selectedList[1]) == true)
            {
                MessageBox.Show("請不要選取相同的bom表比對");
                return;
            }

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            DataSet sourceDs = new DataSet();
            MvBomCompareBo bo = new MvBomCompareBo();
            sourceDs = bo.CollectSourceDsProcess_BomP09_Thin(selectedList.ToArray<string>()).Copy();

            // filter data
            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            DataTable sourceDt2 = sourceDs.Tables[1].Copy();
            DataSet summaryDs = new DataSet();
            DataSet tmpDs = new DataSet();

            // show tree list node
            // 設定column
            DataTable filterDt1 = bo.filterDataByRdRule(sourceDt1, true);
            DataTable filterDt2 = bo.filterDataByRdRule(sourceDt2, true);

            DataTable dtIncludeNameSpace1;
            DataTable dtIncludeNameSpace2;
            DataTable summaryDt;
            dtIncludeNameSpace1 = bo.extendBomNameSpace(filterDt1);
            dtIncludeNameSpace2 = bo.extendBomNameSpace(filterDt2);

            summaryDs = bo.compareBomByRuleRd_v2(dtIncludeNameSpace1, dtIncludeNameSpace2, false);
            tmpDs = bo.compareBomByRuleRd_v2(dtIncludeNameSpace2, dtIncludeNameSpace1, true);

            // 顯示Detail Summary相關資訊
            // 相同的資料不用再呈現差異的比對資訊
            tmpDt = summaryDs.Tables["Same"];
            tmpDt.Columns.Remove("CompareLV");
            tmpDt.Columns.Remove("CompareA8");
            tmpDt.Columns.Remove("CompareMD006");
            tmpDt.Columns.Remove("ModuleLv1");
            treeList3.DataSource = tmpDt.Copy();
            summaryDt = summaryDs.Tables["Different"];
            summaryDt.Merge(tmpDs.Tables["Different"]);
            treeList4.DataSource = summaryDt.Copy();
            setCompareDetailColumnsCaption(ref treeList3, ref treeList4);

            // 顯示SummaryBom相關資訊 for RD
            tmpDt = new DataTable();
            tmpDt = bo.generateSummaryTableByRd(summaryDt, true);
            treeList5.DataSource = tmpDt.Copy();
            setSummaryDetailColumnsCaption(ref treeList5);

            // 整理SummaryBom For Pur
            tmpDt = bo.generateSummaryTableByPur(summaryDt);
            treeList6.DataSource = tmpDt.Copy();
            setSummaryColumnsCaption(ref treeList6);

            // 整理完後再把name space資訊移除
            dtIncludeNameSpace1.Columns.Remove("NameSpace");
            dtIncludeNameSpace1.Columns.Remove("NameSpaceNoVer");
            dtIncludeNameSpace2.Columns.Remove("NameSpace");
            dtIncludeNameSpace2.Columns.Remove("NameSpaceNoVer");

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            // 注意 ParentFieldName必需在 showTreeListByLevel之前
            // 只要設定過, Level的功能會失效
            treeList1.DataSource = dtIncludeNameSpace1.Clone();
            treeList1.ParentFieldName = dtIncludeNameSpace1.TableName;
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, dtIncludeNameSpace1, ref hashTreeListBackColor[0], false);
            setColumnsCaption(ref treeList1);

            // 注意 ParentFieldName必需在 showTreeListByLevel之前
            // 只要設定過, Level的功能會失效
            treeList2.DataSource = dtIncludeNameSpace2.Clone();
            treeList2.ParentFieldName = dtIncludeNameSpace2.TableName;
            hashTreeListBackColor[1].Clear();
            showTreeListByLevel(treeList2, dtIncludeNameSpace2, ref hashTreeListBackColor[1], false);
            setColumnsCaption(ref treeList2);

            xtraTabControl1.TabPages[0].Text = string.Format("{0} vs {1}", filterDt1.TableName, filterDt2.TableName);
            xtraTabControl1.TabPages[1].Text = "CompareDetail";
            xtraTabControl1.TabPages[2].Text = "SummaryDetail";
            xtraTabControl1.TabPages[3].Text = "Summary";

            // Sheet1,2 不開放編輯功能, treeList 1,2,3,4
            // Sheet3,4 開放編輯功能, 這些資料都只能是read only
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.ReadOnly = true;
            treeList4.OptionsBehavior.ReadOnly = true;
            treeList5.OptionsBehavior.ReadOnly = true;
            treeList6.OptionsBehavior.ReadOnly = true;

            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;
            treeList3.OptionsBehavior.Editable = false;
            treeList4.OptionsBehavior.Editable = false;
            treeList5.OptionsBehavior.Editable = false;
            treeList6.OptionsBehavior.Editable = false;

            //Close Wait Form
            SplashScreenManager.CloseForm(false);
        }

        private void setColumnsCaption(ref TreeList tl)
        {
            tl.Columns[0].Caption = "階次";
            tl.Columns[1].Caption = "元件品號";
            tl.Columns[2].Caption = "品號屬性";
            tl.Columns[3].Caption = "品名";
            tl.Columns[4].Caption = "單位";
            tl.Columns[5].Caption = "數量";
            tl.Columns[6].Caption = "SOP圖號";
        }

        private void setCompareDetailColumnsCaption(ref TreeList sameTl, ref TreeList diffTl)
        {
            int index = 0;
            foreach (string header in DefinedHeader.BomCompareHeaderAliasName)
            {
                diffTl.Columns[index].Caption = header;
                if (index >= 3 && index < DefinedHeader.BomCompareHeaderAliasName.Length - 1)
                {
                    sameTl.Columns[index - 3].Caption = header;
                }
                index++;
            }
        }

        private void setSummaryDetailColumnsCaption(ref TreeList tl)
        {
            int index = 0;
            foreach (string header in DefinedHeader.BomCompareReportHeaderAliasName)
            {
                tl.Columns[index].Caption = header;
                index++;
            }
        }

        private void setSummaryColumnsCaption(ref TreeList tl)
        {
            int index = 0;
            foreach(string header in DefinedHeader.BomCompareReportHeaderAliasNameNoLv)
            {
                tl.Columns[index].Caption = header;
                index++;
            }
            
        }

        private void showTreeListByLevel(TreeList tl, DataTable dt, ref Hashtable hashTreeListBackColor, bool isChangeLv)
        {
            TreeListNode[] parentNodeList = new TreeListNode[8];
            TreeListNode parentNode = null;
            int preLV = 0;
            int nowLv = 1;
            int changeColorIndex = 0;
            string strNowLv = string.Empty;
            string buyType = string.Empty;
            string A8 = string.Empty;


            foreach (DataRow dr in dt.Rows)
            {
                strNowLv = dr["LV"].ToString().Trim().Replace(".", "");
                nowLv = int.Parse(strNowLv);
                buyType = dr["MB025X"].ToString().Trim();
                A8 = dr["A8"].ToString().Trim();

                if (A8.Equals("21502121"))
                {
                    Console.WriteLine("test");
                }

                if (isChangeLv == true)
                {
                    dr["LV"] = "L" + strNowLv;
                }

                // 記錄是否要改變顏色
                if (buyType.Equals("模組"))
                {
                    hashTreeListBackColor[changeColorIndex] = true;
                }

                if (preLV < nowLv) // 1 -> 2
                {
                    // 要加入Node
                    parentNode = parentNodeList[preLV];
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                }
                else if (preLV == nowLv)
                {
                    // parentNode 要設定上一層
                    parentNode = parentNodeList[nowLv - 1];
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                }
                else if (preLV > nowLv) // 2 -> 1
                {
                    // parentNode 要設定Parent的上一層, 並清空該層
                    parentNode = parentNodeList[nowLv - 1];
                    parentNodeList[preLV] = null;
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                }
                preLV = nowLv;
                changeColorIndex++;
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

        private void frmBomCompareDev_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
